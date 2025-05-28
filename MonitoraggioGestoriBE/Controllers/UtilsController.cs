using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoraggioGestoriBE.Data;
using MonitoraggioGestoriBE.Models;

namespace MonitoraggioGestoriBE.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class UtilsController : ControllerBase
{
    private readonly FlussiFinContext _context;
    
    public UtilsController(FlussiFinContext context)
    {
        _context = context;
    }

    [HttpGet("getSidebar")]
    public async Task<List<SidebarModel>> GetSidebar()
    {
        return await _context.AN_MONITORAGGIO_GESTORIs
            .Select(s => new SidebarModel()
            {
                DsGestore = s.ID_GESTORENavigation.DS_GESTORE,
                IdGestore = s.ID_GESTORE,
            }).ToListAsync();
    }
    
}