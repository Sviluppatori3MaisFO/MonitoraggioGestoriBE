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

    public GestoriController(FlussiFinContext context)
    {
        _context = context;
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
        return await _context.AN_MONITORAGGIO_GESTORIs
            .Include(i => i.GESTORE)
            .ThenInclude(g => g.SALDI_NORMALIZZATIs)
            .Select(g => new GestoreMonitoratoModel(g))
            .ToListAsync();
    }
    
    // GET: /getGestoreMonitoratoByIdMonitoraggioGestore
    [HttpGet("getGestoreMonitoratoByIdMonitoraggioGestore/{id}")]
    public async Task<GestoreMonitoratoModel?> GetGestoreMonitoratoByIdMonitoraggioGestore(decimal id)
    {
        return await _context.AN_MONITORAGGIO_GESTORIs
                    .Include(i => i.GESTORE)
                    .ThenInclude(g => g.SALDI_NORMALIZZATIs)
                    .Where(w => w.ID_MONITORAGGIO_GESTORE == id)
                    .Select(g => new GestoreMonitoratoModel(g))
                    .FirstOrDefaultAsync();
    }
    
    // GET: /getGestoreMonitoratoByIdGestore
    [HttpGet("getGestoreMonitoratoByIdGestore/{id}")]
    public async Task<GestoreMonitoratoModel?> GetGestoreMonitoratoByIdGestore(decimal id)
    {
        return await _context.AN_MONITORAGGIO_GESTORIs
                    .Include(i => i.GESTORE)
                    .ThenInclude(g => g.SALDI_NORMALIZZATIs)
                    .Where(w => w.ID_GESTORE == id)
                    .Select(g => new GestoreMonitoratoModel(g))
                    .FirstOrDefaultAsync();
    }

    #endregion    
}