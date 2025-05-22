using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MonitoraggioGestoriBE.Data.Models;

[Table("AN_GESTORI")]
public partial class AN_GESTORI
{
    [Key]
    [Column(TypeName = "NUMBER")]
    public decimal ID_GESTORE { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string DS_GESTORE { get; set; } = null!;

    /// <summary>
    /// indica il campo data da usare nei calcoli per le quadrature: &apos;DT_MOVIMENTO&apos;=contabile  &apos;DT_OPERAZIONE&apos;= disponibile 
    /// </summary>
    [StringLength(20)]
    [Unicode(false)]
    public string? DT_RIFERIMENTO_PER_QUADRATURE { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? DATA_CREAZIONE { get; set; }

    /// <summary>
    /// ID tabella utenti GLOBAUTH
    /// </summary>
    [Precision(10)]
    public int? UTENTE_CREAZIONE { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? DATA_MODIFICA { get; set; }

    /// <summary>
    /// ID tabella utenti GLOBAUTH
    /// </summary>
    [Precision(10)]
    public int? UTENTE_MODIFICA { get; set; }

    /// <summary>
    /// Descrizione breve (usata in reportistica)
    /// </summary>
    [StringLength(255)]
    [Unicode(false)]
    public string DS_GESTORE_REPORT { get; set; } = null!;

    [InverseProperty("ID_GESTORENavigation")]
    public virtual ICollection<AN_MONITORAGGIO_GESTORI> AN_MONITORAGGIO_GESTORIs { get; set; } = new List<AN_MONITORAGGIO_GESTORI>();

    [InverseProperty("ID_GESTORENavigation")]
    public virtual ICollection<MOVIMENTI_NORMALIZZATI> MOVIMENTI_NORMALIZZATIs { get; set; } = new List<MOVIMENTI_NORMALIZZATI>();

    [InverseProperty("ID_GESTORENavigation")]
    public virtual ICollection<SALDI_NORMALIZZATI> SALDI_NORMALIZZATIs { get; set; } = new List<SALDI_NORMALIZZATI>();
}
