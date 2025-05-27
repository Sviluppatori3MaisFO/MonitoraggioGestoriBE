using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MonitoraggioGestoriBE.Data.Models;

[Keyless]
[Table("MONITORAGGIO_GESTORI_IMPORTAZIONE_FLUSSI")]
public partial class MONITORAGGIO_GESTORI_IMPORTAZIONE_FLUSSI
{
    /// <summary>
    /// Index
    /// </summary>
    [Column(TypeName = "NUMBER")]
    public decimal ID_IMPORTAZIONE_FLUSSO { get; set; }

    /// <summary>
    /// ID GESTORE COLLEGATO AD AN_GESTORI
    /// </summary>
    [Column(TypeName = "NUMBER")]
    public decimal ID_GESTORE { get; set; }

    /// <summary>
    /// FG IMPORTAZIONE MENSILE se limportazione e&apos; effettuata
    /// </summary>
    [Column(TypeName = "NUMBER")]
    public decimal FG_IMPORTAZIONE_MM { get; set; }

    /// <summary>
    /// FG IMPORTAZIONE SETTIMANALE se limportazione e&apos; effettuata
    /// </summary>
    [Column(TypeName = "NUMBER")]
    public decimal? FG_IMPORTAZIONE_SS { get; set; }

    /// <summary>
    /// DATA_IMPORTAZIONE MENSILE
    /// </summary>
    [Column(TypeName = "DATE")]
    public DateTime DT_IMPORT_MM { get; set; }

    /// <summary>
    /// DATA_IMPORTAZIONE SETTIMANALE
    /// </summary>
    [Column(TypeName = "DATE")]
    public DateTime? DT_IMPORT_SS { get; set; }

    /// <summary>
    /// NOTE Sull importazione
    /// </summary>
    [Column(TypeName = "CLOB")]
    public string? NOTE_HTML { get; set; }

    [ForeignKey("ID_GESTORE")]
    public virtual AN_GESTORI ID_GESTORENavigation { get; set; } = null!;

}
