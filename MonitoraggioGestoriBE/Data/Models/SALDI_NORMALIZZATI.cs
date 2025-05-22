using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MonitoraggioGestoriBE.Data.Models;

[Table("SALDI_NORMALIZZATI")]
public partial class SALDI_NORMALIZZATI
{
    [Key]
    [Precision(10)]
    public int ID_SALDO_NORMALIZZATO { get; set; }

    [Column(TypeName = "NUMBER")]
    public decimal ID_GESTORE { get; set; }

    /// <summary>
    /// progressivo che raggruppa l&apos;importazione di un gruppo di saldi
    /// </summary>
    [Precision(10)]
    public int ID_IMPORTAZIONE { get; set; }

    /// <summary>
    /// &apos;TIT&apos; = saldo titolo , &apos;CC&apos; = saldo conto corrente
    /// </summary>
    [StringLength(5)]
    [Unicode(false)]
    public string? TIPOLOGIA_SALDO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CODICE_OPERAZIONE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CODICE_RAPPORTO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? INTESTATARIO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CONTO_CORRENTE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CATEGORIA_TITOLO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CODICE_INTERNO_TITOLO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ISIN { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? MERCATO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? TICKER { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? DESCRIZIONE_TITOLO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? DIVISA_TITOLO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? DIVISA_RIFERIMENTO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CAMBIO { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? DATA_SALDO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? QUANTITA { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? PREZZO_TITOLO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? RATEO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CTV_TITOLO { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime DATA_IMPORTAZIONE { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? DATA_MODIFICA { get; set; }

    [Precision(10)]
    public int? UTENTE_MODIFICA { get; set; }

    [Unicode(false)]
    public string? NOTE { get; set; }

    /// <summary>
    /// &apos;NE&apos;=normalizzato errore, &apos;N&apos;=normalizzato, &apos;IE&apos;=importazione errore &apos;IV&apos;=importazione valido, &apos;I&apos;=importato
    /// </summary>
    [StringLength(5)]
    [Unicode(false)]
    public string STATO { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? PREZZO_RIFERIMENTO { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CTV_RIFERIMENTO { get; set; }

    /// <summary>
    /// id del tracciato associato a questo saldo (AN_TRACCIATI)
    /// </summary>
    [Precision(10)]
    public int? ID_TRACCIATO { get; set; }

    [ForeignKey("ID_GESTORE")]
    [InverseProperty("SALDI_NORMALIZZATIs")]
    public virtual AN_GESTORI ID_GESTORENavigation { get; set; } = null!;
}
