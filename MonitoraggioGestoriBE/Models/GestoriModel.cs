using MonitoraggioGestoriBE.Data.Models;

namespace MonitoraggioGestoriBE.Models;

public class GestoreModel
{
    public decimal IdGestore { get; set; }
    public string DsGestore { get; set; }
    public string DtRiferimentoQuadratura { get; set; }
    public int? UtenteCreazione { get; set; }
    public DateTime? DtCreazione { get; set; }
    public int? UtenteModifica { get; set; }
    public DateTime? DtModifica { get; set; }
    public string DsGestoreReport { get; set; }
}

public class GestoreMonitoratoModel
{
    public decimal IdMonitoraggioGestore { get; set; }
    public decimal IdGestore { get; set; }
    public string DsGestore { get; set; }
    public decimal CompravenditaDivisa { get; set; }
    public string? NoteGestore { get; set; }
    public string EmailGestore1 { get; set; }
    public string? EmailGestore2 { get; set; }
    public decimal DtArrivoFlussiMmD1 { get; set; }
    public decimal DtArrivoFlussiMmD2 { get; set; }
    public decimal? DtArrivoFlussiSs { get; set; }
    public decimal FgMonitoring { get; set; }
    public DateTime DtCreazione { get; set; }
    public DateTime? DtLastEdit { get; set; }
    public DateTime? LastImportMM { get; set; }
    public DateTime? DtImportMM { get; set; } //data importazione manuale del mensile
    public DateTime? DtImportSS { get; set; } //data importazione manuale del settimanale
    
    public List<MovimentiNormalizzatiModel> MovimentiBloccati { get; set; }

    public GestoreMonitoratoModel()
    {
    }
    
    public GestoreMonitoratoModel(AN_MONITORAGGIO_GESTORI g)
    {
        IdGestore = g.ID_GESTORE;
        CompravenditaDivisa = g.COMPRAVENDITA_DIVISA;
        NoteGestore = g.NOTE_GESTORE;
        EmailGestore1 = g.EMAIL_GESTORE_1;
        EmailGestore2 = g.EMAIL_GESTORE_2;
        DtArrivoFlussiMmD1 = g.DT_ARRIVO_FLUSSI_MM_D1;
        DtArrivoFlussiMmD2 = g.DT_ARRIVO_FLUSSI_MM_D2;
        DtArrivoFlussiSs = g.DT_ARRIVO_FLUSSI_SS;
        FgMonitoring = g.FG_MONITORING;
        DtCreazione = g.DT_CREAZIONE;
        DtLastEdit = g.DT_LAST_EDIT;
        DsGestore = !String.IsNullOrEmpty(g.ID_GESTORENavigation?.DS_GESTORE) ? g.ID_GESTORENavigation.DS_GESTORE : "";
        LastImportMM = null;
    }
    
    public GestoreMonitoratoModel(AN_MONITORAGGIO_GESTORI g, DateTime? lastImportMM = null)
    {
        IdGestore = g.ID_GESTORE;
        CompravenditaDivisa = g.COMPRAVENDITA_DIVISA;
        NoteGestore = g.NOTE_GESTORE;
        EmailGestore1 = g.EMAIL_GESTORE_1;
        EmailGestore2 = g.EMAIL_GESTORE_2;
        DtArrivoFlussiMmD1 = g.DT_ARRIVO_FLUSSI_MM_D1;
        DtArrivoFlussiMmD2 = g.DT_ARRIVO_FLUSSI_MM_D2;
        DtArrivoFlussiSs = g.DT_ARRIVO_FLUSSI_SS;
        FgMonitoring = g.FG_MONITORING;
        DtCreazione = g.DT_CREAZIONE;
        DtLastEdit = g.DT_LAST_EDIT;
        DsGestore = !String.IsNullOrEmpty(g.ID_GESTORENavigation?.DS_GESTORE) ? g.ID_GESTORENavigation.DS_GESTORE : "";
        DtImportMM = DtImportMM;
        DtImportSS = DtImportSS;
        LastImportMM = lastImportMM;
    }

    public GestoreMonitoratoModel(AN_MONITORAGGIO_GESTORI g, DateTime? DtImportMMP, DateTime? lastImportMM = null, DateTime? DtImportSSP = null, List<MovimentiNormalizzatiModel> MovimentiBloccati = null)
    {
        IdGestore = g.ID_GESTORE;
        CompravenditaDivisa = g.COMPRAVENDITA_DIVISA;
        NoteGestore = g.NOTE_GESTORE;
        EmailGestore1 = g.EMAIL_GESTORE_1;
        EmailGestore2 = g.EMAIL_GESTORE_2;
        DtArrivoFlussiMmD1 = g.DT_ARRIVO_FLUSSI_MM_D1;
        DtArrivoFlussiMmD2 = g.DT_ARRIVO_FLUSSI_MM_D2;
        DtArrivoFlussiSs = g.DT_ARRIVO_FLUSSI_SS;
        FgMonitoring = g.FG_MONITORING;
        DtCreazione = g.DT_CREAZIONE;
        DtLastEdit = g.DT_LAST_EDIT;
        DsGestore = !String.IsNullOrEmpty(g.ID_GESTORENavigation?.DS_GESTORE) ? g.ID_GESTORENavigation.DS_GESTORE : "";
        DtImportMM = DtImportMMP;
        DtImportSS = DtImportSSP;
        LastImportMM = lastImportMM;
        this.MovimentiBloccati = MovimentiBloccati;
    }

}



public class GestoreImportazioneMovimentiChart
{
    public decimal IdGestore { get; set; }
    public DateTime DtImportazione { get; set; }
    public decimal ValueDefinitivi { get; set; }
    public decimal ValueSettimanali { get; set; }
}

public class GestoreUltimoImportazione
{
    public decimal IdGestore { get; set; }
    public decimal IdImportazioneGestore { get; set; }
    public string DsGestore { get; set; }
    public decimal FgImportSS { get; set; }
    public decimal FgImportMM { get; set; }
    public string? Note { get; set; }
    public DateTime DtImportMM { get; set; }    
    public DateTime? DtImportSS { get; set; }    
}