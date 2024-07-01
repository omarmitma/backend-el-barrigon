using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto__Final.Tablas;

[Table("Plato")]
public partial class Plato
{
    [Key]
    public int IdPlato { get; set; }

    public int IdLinea { get; set; }

    public int IdSubLinea { get; set; }

    public int IdCategoria { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [StringLength(50)]
    public string Abreviatura { get; set; } = null!;

    [Column(TypeName = "decimal(10, 3)")]
    public decimal Precio { get; set; }
    [Column(TypeName = "decimal(10, 3)")]
    public decimal PrecioAdicional { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    public decimal PrecioOferta { get; set; }

    [StringLength(2000)]
    public string? UrlImagen { get; set; }

    [StringLength(20)]
    public string? TiempoEspera { get; set; }

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

    [InverseProperty("IdPlatoNavigation")]
    public virtual ICollection<DetalleCarrito> DetalleCarritos { get; set; } = new List<DetalleCarrito>();

    [InverseProperty("IdPlatoNavigation")]
    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    [InverseProperty("IdPlatoNavigation")]
    public virtual ICollection<DetallePlato> DetallePlatos { get; set; } = new List<DetallePlato>();
}
