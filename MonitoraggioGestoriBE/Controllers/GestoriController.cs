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

    #region GESTORI

    // GET: /getAnGestori
    [HttpGet("getAnGestori")]
    public async Task<List<AN_GESTORI>> GetAnGestori()
    {
        return await _context.AN_GESTORIs.ToListAsync();
    }
    
    // GET: /getAnGestoreeById
    [HttpGet("getAnGestoreeById/{id}")]
    public async Task<AN_GESTORI?> GetAnGestoreeById(decimal id)
    {
        return await _context.AN_GESTORIs.Where(w => w.ID_GESTORE == id).FirstOrDefaultAsync();
    }

    #endregion

    // - =================================================================================================================================================
    
    #region  MONITORAGGIO_GESTORI
    
    // GET: /getAnGestoriMonitorati
    [HttpGet("getAnGestoriMonitorati")]
    public async Task<List<GestoreMonitoratoModel>> GetAnGestoriMonitorati()
    {
        // 1. Recupera tutti i gestori monitorati dal DB
        var entities = await _context.AN_MONITORAGGIO_GESTORIs
            .Include(g => g.GESTORE) // per DsGestore
            .ToListAsync();

        var gestoriIds = entities.Select(e => e.ID_GESTORE).Distinct().ToList();

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

        // 3. Costruisci il modello combinando i dati
        var result = entities
            .Select(g => new GestoreMonitoratoModel(g,
                lastImportByGestoreId.TryGetValue(g.ID_GESTORE, out var date) ? date : null))
            .ToList();

        return result;
    }

    
    // GET: /getGestoreMonitoratoByIdMonitoraggioGestore
    [HttpGet("getGestoreMonitoratoByIdMonitoraggioGestore/{id}")]
    public async Task<GestoreMonitoratoModel?> GetGestoreMonitoratoByIdMonitoraggioGestore(decimal id)
    {
        var lastImportMm = await _utils.GetLastImportMMByIdMonitoraggio(id);
        
        return await _context.AN_MONITORAGGIO_GESTORIs
            .Where(w => w.ID_GESTORE == id)
            .Select(g => new GestoreMonitoratoModel(g, lastImportMm))
            .FirstOrDefaultAsync();

    }
    

    #endregion    
}