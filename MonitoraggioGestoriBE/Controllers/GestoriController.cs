using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoraggioGestoriBE.Data;
using MonitoraggioGestoriBE.Data.Models;
using MonitoraggioGestoriBE.Models;


namespace MonitoraggioGestoriBE.Controllers;

[ApiController]
[Route("[controller]/")]
public class GestoriController : ControllerBase
{
    private readonly FlussiFinContext _context;
    private readonly UtilsService _utils;

    public GestoriController(FlussiFinContext context, UtilsService utilsService)
    {
        _context = context;
        _utils = utilsService;
    }
    
    #region  MONITORAGGIO_GESTORI
    
    // GET: /getAnGestoriMonitorati
    [HttpGet("getAnGestoriMonitorati")]
    public async Task<List<GestoreMonitoratoModel>> GetAnGestoriMonitorati()
    {
        // 1. Recupera tutti i gestori monitorati dal DB
        var gestoriMonitorati = await _context.AN_MONITORAGGIO_GESTORIs
            .Include(g => g.ID_GESTORENavigation) // per DsGestore
            .ToListAsync();

        var gestoriIds = gestoriMonitorati.Select(e => e.ID_GESTORE).Distinct().ToList();
        var gestoriMonitoratiIds = gestoriMonitorati.Select(e => e.ID_MONITORAGGIO_GESTORE).Distinct().ToList();

        // 2. Recupera i LastImportMM in un'unica query
        var lastImportByGestoreId = await _context.SALDI_NORMALIZZATIs
            .Where(s => gestoriIds.Contains(s.ID_GESTORE))
            .GroupBy(s => s.ID_GESTORE)
            .Select(g => new
            {
                IdGestore = g.Key,
                MaxDate = g.Max(s => (DateTime?)s.DATA_SALDO)
            })
            .ToDictionaryAsync(x => x.IdGestore, x => x.MaxDate);
        
        // recuper gli import manuali
        var importgestori = await _context.IMPORTAZIONE_GESTORIs
            .Where(w => gestoriMonitoratiIds.Contains(w.ID_MONITORAGGIO_GESTORE))
            .Select(s => new
            {
                IdGestoreMonitorato = s.ID_MONITORAGGIO_GESTORE,
                DtImports = new
                {
                    DtImportazioneMM = s.DT_IMPORT_MM,
                    DtImportazioneSS = s.DT_IMPORT_SS,
                }
            })
            .ToDictionaryAsync(d => d.IdGestoreMonitorato, d => d.DtImports);

        // 3. Costruisci il modello combinando i dati
        var result = gestoriMonitorati
            .Select(g =>
            {
                // Estrai dati da dizionari con nomi distinti
                importgestori.TryGetValue(g.ID_MONITORAGGIO_GESTORE, out var importData);
                lastImportByGestoreId.TryGetValue(g.ID_GESTORE, out var lastMM);

                return new GestoreMonitoratoModel(
                    g,
                    importData?.DtImportazioneMM,
                    lastMM,
                    importData?.DtImportazioneSS
                );
            })
            .ToList();


        return result;
    }

    // - =================================================================================================================================================
    
    // GET: /getGestoreMonitoratoByIdGestore
    [HttpGet("getGestoreMonitoratoByIdGestore/{id}")]
    public async Task<GestoreMonitoratoModel?> GetGestoreMonitoratoByIdGestore(decimal id)
    {
        // 1. Recupera il gestore monitorato
        var monitoraggio = await _context.AN_MONITORAGGIO_GESTORIs
            .Include(g => g.ID_GESTORENavigation)
            .FirstOrDefaultAsync(w => w.ID_GESTORE == id);

        if (monitoraggio == null)
            return null;

        // 2. Recupera il LastImportMM (da SALDI_NORMALIZZATI)
        var lastImportMM = await _context.SALDI_NORMALIZZATIs
            .Where(s => s.ID_GESTORE == monitoraggio.ID_GESTORE)
            .MaxAsync(s => (DateTime?)s.DATA_SALDO);

        // 3. Recupera i dati di importazione (da IMPORTAZIONE_GESTORI)
        var import = await _context.IMPORTAZIONE_GESTORIs
            .Where(i => i.ID_MONITORAGGIO_GESTORE == monitoraggio.ID_MONITORAGGIO_GESTORE)
            .OrderByDescending(i => i.DT_IMPORT_MM)
            .FirstOrDefaultAsync();

        // 4. Costruisci e restituisci il modello
        return new GestoreMonitoratoModel(
            monitoraggio,
            import?.DT_IMPORT_MM,
            lastImportMM,
            import?.DT_IMPORT_SS
        );
    }
    

    #endregion    
}