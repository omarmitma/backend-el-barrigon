using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto__Final.Tablas;

[Table("CabezeraMaestro")]
public partial class CabezeraMaestro
{
    [Key]
    public int IdCabezeraMaestro { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime? FecRegistro { get; set; }

    [StringLength(20)]
    public string? HorRegistro { get; set; }

    public int? UsuRegistro { get; set; }

    [Column(TypeName = "date")]
    public DateTime? FecModifica { get; set; }

    [StringLength(20)]
    public string? HorModifica { get; set; }

    public int? UsuModifica { get; set; }

    public int? Estado { get; set; }

    public int? Accion { get; set; }

    [InverseProperty("IdCabezeraMaestroNavigation")]
    public virtual ICollection<DetalleMaestro> DetalleMaestros { get; set; } = new List<DetalleMaestro>();
}
