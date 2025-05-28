using Microsoft.EntityFrameworkCore;
using MonitoraggioGestoriBE.Data;
using MonitoraggioGestoriBE.Data.Models;


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

    public async Task<MONITORAGGIO_GESTORI_IMPORTAZIONE_FLUSSI> GetLastImportManuale(decimal idGestore)
    {
        var res= _context.MONITORAGGIO_GESTORI_IMPORTAZIONE_FLUSSIs.Where(w => w.ID_GESTORE==idGestore)
            .OrderBy(i => i.ID_IMPORTAZIONE_FLUSSO)
            .FirstOrDefault();

        return res;
    }
}