using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Proyecto__Final.Tablas;

[Table("DetallePlato")]
public partial class DetallePlato
{
    [Key]
    public int IdDetallePlato { get; set; }

    public int IdPlato { get; set; }

    public int IdProducto { get; set; }

    public int IdMedida { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    public decimal? Cantidad { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    public decimal? CantidadMaxima { get; set; }

    public int FlagRequerido { get; set; }

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

    [ForeignKey("IdPlato")]
    [InverseProperty("DetallePlatos")]
    [JsonIgnore]
    public virtual Plato? IdPlatoNavigation { get; set; } = null!;

    [ForeignKey("IdProducto")]
    [InverseProperty("DetallePlatos")]
    [JsonIgnore]
    public virtual Producto? IdProductoNavigation { get; set; } = null!;
}
