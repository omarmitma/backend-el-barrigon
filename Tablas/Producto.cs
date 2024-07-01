using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto__Final.Tablas;

[Table("Producto")]
public partial class Producto
{
    [Key]
    public int IdProducto { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column(TypeName = "decimal(10, 3)")]
    public decimal? Precio { get; set; }

    public int IdLinea { get; set; }

    public int IdSubLinea { get; set; }

    public int IdCategoria { get; set; }

    public int IdUnidad { get; set; }
    [StringLength(2000)]
    public string? UrlImagen { get; set; }

    public string? Observacion { get; set; }

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

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetalleCarritoPersonalizado> DetalleCarritoPersonalizados { get; set; } = new List<DetalleCarritoPersonalizado>();

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetallePedidoPersonalizado> DetallePedidoPersonalizados { get; set; } = new List<DetallePedidoPersonalizado>();

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetallePlato> DetallePlatos { get; set; } = new List<DetallePlato>();
}
