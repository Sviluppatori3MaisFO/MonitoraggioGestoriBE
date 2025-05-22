using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MonitoraggioGestoriBE.Data.Models;

[Table("IMPORTAZIONE_GESTORI")]
public partial class IMPORTAZIONE_GESTORI
{
    [Key]
    [Column(TypeName = "NUMBER")]
    public decimal ID_IMPORTAZIONE_GESTORE { get; set; }

    [Column(TypeName = "NUMBER")]
    public decimal ID_MONITORAGGIO_GESTORE { get; set; }

    [Column(TypeName = "NUMBER")]
    public decimal FG_IMPORT_MM { get; set; }

    [Column(TypeName = "NUMBER")]
    public decimal FG_IMPORT_SS { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? NOTE { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? DT_IMPORT_SS { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime DT_IMPORT_MM { get; set; }

    [ForeignKey("ID_MONITORAGGIO_GESTORE")]
    [InverseProperty("IMPORTAZIONE_GESTORIs")]
    public virtual AN_MONITORAGGIO_GESTORI ID_MONITORAGGIO_GESTORENavigation { get; set; } = null!;
}
