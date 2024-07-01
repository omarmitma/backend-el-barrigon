using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Proyecto__Final.Tablas;

[Table("DetalleCarritoPersonalizado")]
public partial class DetalleCarritoPersonalizado
{
    [Key]
    public int IdDetalleCarritoPersonalizado { get; set; }

    public int IdDetalleCarrito { get; set; }

    public int IdProducto { get; set; }

    public int IdMedida { get; set; }

    public int Cantidad { get; set; }

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

    [ForeignKey("IdDetalleCarrito")]
    [InverseProperty("DetalleCarritoPersonalizados")]
    [JsonIgnore]
    public virtual DetalleCarrito? IdDetalleCarritoNavigation { get; set; } = null!;

    [ForeignKey("IdProducto")]
    [InverseProperty("DetalleCarritoPersonalizados")]
    [JsonIgnore]
    public virtual Producto? IdProductoNavigation { get; set; } = null!;
}
