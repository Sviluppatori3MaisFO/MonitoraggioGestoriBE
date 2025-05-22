using Microsoft.EntityFrameworkCore;
using MonitoraggioGestoriBE.Data;


public class UtilsService
{
    private readonly FlussiFinContext _context;

    public UtilsService(FlussiFinContext context)
    {
        _context = context;
    }

    public async Task<DateTime?> GetLastImportMMByIdMonitoraggio(decimal idMonitoraggioGestore)
    {
        var idGestore = await _context.AN_MONITORAGGIO_GESTORIs
            .Where(w => w.ID_MONITORAGGIO_GESTORE == idMonitoraggioGestore)
            .Select(s => s.ID_GESTORE)
            .FirstOrDefaultAsync();
        
        return await _context.SALDI_NORMALIZZATIs
            .Where(s => s.ID_GESTORE == idGestore)
            .MaxAsync(s => (DateTime?)s.DATA_SALDO);
    }
}