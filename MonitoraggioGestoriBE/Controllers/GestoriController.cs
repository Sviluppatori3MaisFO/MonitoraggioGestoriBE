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
        var latestImports = await _context.MONITORAGGIO_GESTORI_IMPORTAZIONE_FLUSSIs
            .Where(w => gestoriIds.Contains(w.ID_GESTORE))
            .GroupBy(g => g.ID_GESTORE)
            .Select(g => g.OrderByDescending(x => x.ID_IMPORTAZIONE_FLUSSO).FirstOrDefault())
            .ToListAsync(); // esegui qui la query

        var importgestori = latestImports
            .ToDictionary(
                s => s.ID_GESTORE,
                s => new
                {
                    DtImportazioneMM = s.DT_IMPORT_MM,
                    DtImportazioneSS = s.DT_IMPORT_SS
                });



        // 3. Costruisci il modello combinando i dati
        var result = gestoriMonitorati
            .Select(g =>
            {
                // Estrai dati da dizionari con nomi distinti
                importgestori.TryGetValue(g.ID_GESTORE, out var importData);
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
        var import = await _context.MONITORAGGIO_GESTORI_IMPORTAZIONE_FLUSSIs
            .Where(i => i.ID_GESTORE == monitoraggio.ID_GESTORE)
            .OrderByDescending(i => i.DT_IMPORT_MM)
            .FirstOrDefaultAsync();

        var movimentiBloccati = await GetMovimentiInImportErrore(id);

        // 4. Costruisci e restituisci il modello
        return new GestoreMonitoratoModel(
            monitoraggio,
            import?.DT_IMPORT_MM,
            lastImportMM,
            import?.DT_IMPORT_SS,
            movimentiBloccati
        );
    }
    

    // - =================================================================================================================================================
    
    // GET: /getGestoreMonitoratoByIdGestore
    [HttpGet("getQuantitaGestoriChart/{idGestore}")]
    public async Task<List<GestoreImportazioneMovimentiChart>> GetQuantitaGestoriChart (decimal idGestore)
    {
        
        var seiMesiFa = DateTime.Today.AddMonths(-6);

        var movimenti = await _context.MOVIMENTI_NORMALIZZATIs
            .Where(w => w.ID_GESTORE == idGestore && w.DATA_IMPORTAZIONE >= seiMesiFa)
            .GroupBy(g => new { g.ID_GESTORE, g.DATA_IMPORTAZIONE })
            .Select(s => new GestoreImportazioneMovimentiChart()
            {
                IdGestore = s.Key.ID_GESTORE,
                DtImportazione = s.Key.DATA_IMPORTAZIONE,

                // Conta definitivi: quelli con CODICE_OPERAZIONE che inizia per "D_"
                ValueDefinitivi = s.Count(x => x.CODICE_OPERAZIONE.StartsWith("D_")),

                // Conta settimanali: tutti gli altri
                ValueSettimanali = s.Count(x => !x.CODICE_OPERAZIONE.StartsWith("D_"))
            })
            .ToListAsync();



        return movimenti;
    }
    
    
    // - =================================================================================================================================================
    
    [HttpGet("getMovimentiInImportErrore")]
    public async Task <List<MovimentiNormalizzatiModel>> GetMovimentiInImportErrore(decimal idGestore)
    {
        var res = await _context.MOVIMENTI_NORMALIZZATIs
            .Where(w => w.ID_GESTORE == idGestore && w.STATO != "N" && w.STATO != "I" && w.STATO != "M")
            .Select(s => new MovimentiNormalizzatiModel()
            {
                IdMovimentoNormalizzato = s.ID_MOV_NORMALIZZATO,
                Stato = String.IsNullOrEmpty(s.STATO) ? "" : s.STATO,
                DtOperazione = s.DATA_OPERAZIONE,
                Causale = String.IsNullOrEmpty(s.CAUSALE) ? "" : s.CAUSALE,
                TipoOperazione = String.IsNullOrEmpty(s.TIPO_OPERAZIONE)? "" : s.TIPO_OPERAZIONE,
                
            } )
            
            .ToListAsync();

        return res;
    }
    
    
    
    // - =================================================================================================================================================
    
    // POST: /editMonitoraggioGestore
    [HttpPost("editGestoreMonitorato")]
    public async Task<bool> EditGestoreMonitorato(GestoreMonitoratoModel? model = null)
    {
        if(model == null) return false;
        
        var oldModel = await _context.AN_MONITORAGGIO_GESTORIs
            .FirstOrDefaultAsync(w => w.ID_GESTORE == model.IdGestore);
        if (oldModel == null)
            model = new GestoreMonitoratoModel();
        
        oldModel.COMPRAVENDITA_DIVISA = model.CompravenditaDivisa;
        oldModel.NOTE_GESTORE = model.NoteGestore;
        oldModel.EMAIL_GESTORE_1 = model.EmailGestore1.Trim();
        oldModel.EMAIL_GESTORE_2 = !String.IsNullOrEmpty(model.EmailGestore2) ?model.EmailGestore2.Trim() : null;
        oldModel.DT_ARRIVO_FLUSSI_MM_D1 = model.DtArrivoFlussiMmD1;
        oldModel.DT_ARRIVO_FLUSSI_MM_D2 = model.DtArrivoFlussiMmD2;
        oldModel.DT_ARRIVO_FLUSSI_SS = model.DtArrivoFlussiSs;

        await _context.SaveChangesAsync();
        
        return true;
    }
    
    
    
    

    #endregion    
    
    #region Importazioni
    // - =================================================================================================================================================
    
    [HttpGet("getUltimaImportazione/{idGestore}")]
    public async Task <GestoreUltimoImportazione> GetUltimaImportazione(decimal idGestore)
    {
        var res = await _context.MONITORAGGIO_GESTORI_IMPORTAZIONE_FLUSSIs
            .Where(w => w.ID_GESTORENavigation.ID_GESTORE == idGestore)
            .Select(s => new GestoreUltimoImportazione()
            {
                IdImportazioneGestore = s.ID_IMPORTAZIONE_FLUSSO,
                DsGestore = s.ID_GESTORENavigation.DS_GESTORE,
                FgImportMM = s.FG_IMPORTAZIONE_MM,
                FgImportSS = s.FG_IMPORTAZIONE_SS != null ?s.FG_IMPORTAZIONE_SS.Value:0,
                Note = String.IsNullOrEmpty(s.NOTE_HTML)? "" : s.NOTE_HTML,
                DtImportMM = s.DT_IMPORT_MM,
                DtImportSS = s.DT_IMPORT_SS,
            })
            .OrderByDescending(w => w.IdImportazioneGestore)
            .FirstOrDefaultAsync();
        
        if(res == null)
        {
            res = new GestoreUltimoImportazione();
            res.DsGestore = await _utils.GetGestoreDs(idGestore);
        }

        return res;
    }
    
    
    // - =================================================================================================================================================
    
    // POST: /editMonitoraggioGestore
    [HttpPost("addNewImportazione")]
    public async Task<bool> AddNewImportazione(GestoreUltimoImportazione model)
    {
        MONITORAGGIO_GESTORI_IMPORTAZIONE_FLUSSI addNew = new MONITORAGGIO_GESTORI_IMPORTAZIONE_FLUSSI();

        addNew.FG_IMPORTAZIONE_MM = model.FgImportMM;
        addNew.FG_IMPORTAZIONE_SS = model.FgImportSS;
        addNew.DT_IMPORT_MM = model.DtImportMM;
        addNew.DT_IMPORT_SS = model.DtImportSS;
        addNew.NOTE_HTML = model.Note;
        addNew.ID_GESTORE = model.IdGestore;
        
        await _context.AddAsync(addNew);
        await _context.SaveChangesAsync();
        
        return true;
    }
    #endregion
}





