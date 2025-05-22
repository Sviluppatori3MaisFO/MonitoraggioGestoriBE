using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MonitoraggioGestoriBE.Data.Models;

[Table("AN_MONITORAGGIO_GESTORI")]
public partial class AN_MONITORAGGIO_GESTORI
{
    /// <summary>
    /// ID Progressivo PK tabella
    /// </summary>
    [Key]
    [Column(TypeName = "NUMBER")]
    public decimal ID_MONITORAGGIO_GESTORE { get; set; }

    [Column(TypeName = "NUMBER")]
    public decimal ID_GESTORE { get; set; }

    /// <summary>
    /// 1 se il gestore è compravendita divisa 0 se non lo è
    /// </summary>
    [Column(TypeName = "NUMBER")]
    public decimal COMPRAVENDITA_DIVISA { get; set; }

    /// <summary>
    /// Note del gestore espresse in HTML
    /// </summary>
    [Column(TypeName = "CLOB")]
    public string? NOTE_GESTORE { get; set; }

    /// <summary>
    /// Email da chi arrivano i movimenti del gestore
    /// </summary>
    [StringLength(500)]
    [Unicode(false)]
    public string EMAIL_GESTORE_1 { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? EMAIL_GESTORE_2 { get; set; }

    /// <summary>
    /// Data in cui di solito arrivano i flussi considerare una sett. da quel gg o crea day 1(crea un rage di giorni)
    /// </summary>
    [Column(TypeName = "NUMBER")]
    public decimal DT_ARRIVO_FLUSSI_MM_D1 { get; set; }

    /// <summary>
    /// GG della settimana in cui arrivano i flussi in numero del gg es. Lunedi=1 Martedi=2
    /// </summary>
    [Column(TypeName = "NUMBER")]
    public decimal? DT_ARRIVO_FLUSSI_SS { get; set; }

    /// <summary>
    /// Flag di monitoraggio 1 monitoriamo ancora 2 no (eliminazione logica)
    /// </summary>
    [Column(TypeName = "NUMBER")]
    public decimal FG_MONITORING { get; set; }

    /// <summary>
    /// DT di inizio monitoraggio 
    /// </summary>
    [Column(TypeName = "DATE")]
    public DateTime DT_CREAZIONE { get; set; }

    /// <summary>
    /// DT ultima modifica an
    /// </summary>
    [Column(TypeName = "DATE")]
    public DateTime? DT_LAST_EDIT { get; set; }

    /// <summary>
    /// Data in cui di solito arrivano i flussi considerare una sett. da quel gg  o crea day 2 (crea un rage di giorni)
    /// </summary>
    [Column(TypeName = "NUMBER")]
    public decimal DT_ARRIVO_FLUSSI_MM_D2 { get; set; }

    [ForeignKey("ID_GESTORE")]
    [InverseProperty("AN_MONITORAGGIO_GESTORIs")]
    public virtual AN_GESTORI ID_GESTORENavigation { get; set; } = null!;

    [InverseProperty("ID_MONITORAGGIO_GESTORENavigation")]
    public virtual ICollection<IMPORTAZIONE_GESTORI> IMPORTAZIONE_GESTORIs { get; set; } = new List<IMPORTAZIONE_GESTORI>();
}
