namespace MonitoraggioGestoriBE.Models;

public class MovimentiModel
{
    
}

public class MovimentiNormalizzatiModel
{
    public decimal IdMovimentoNormalizzato { get; set; }
    public string Stato { get; set; }
    public DateTime? DtOperazione { get; set; }
    public string Causale { get; set; }
    public string TipoOperazione { get; set; }
}