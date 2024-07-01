using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Proyecto__Final.Tablas;

[Table("Pedido")]
public partial class Pedido
{
    [Key]
    public int IdPedido { get; set; }

    public int IdMesa { get; set; }

    public int IdCliente { get; set; }

    public int CantidadPersonas { get; set; }

    public int TipoPago { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    public decimal? TotalPago { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    public decimal? Descuento { get; set; }

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

    [InverseProperty("IdPedidoNavigation")]
    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    [ForeignKey("IdMesa")]
    [InverseProperty("Pedidos")]
    [JsonIgnore]
    public virtual Mesa? IdMesaNavigation { get; set; } = null!;

    [InverseProperty("IdPedidoNavigation")]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
