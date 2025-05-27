using Microsoft.EntityFrameworkCore;
using MonitoraggioGestoriBE.Data;


public class UtilsService
{
    private readonly FlussiFinContext _context;

    public UtilsService(FlussiFinContext context)
    {
        _context = context;
    }

    public async Task<string> GetGestoreDs(decimal idGestore)
    {
        var dsGestore =await _context.AN_GESTORIs.Where(w=>w.ID_GESTORE==idGestore).Select(s => s.DS_GESTORE).FirstOrDefaultAsync();
        return String.IsNullOrEmpty(dsGestore) ? "" : dsGestore;
    }
}