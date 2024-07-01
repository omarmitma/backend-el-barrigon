using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Proyecto__Final.Tablas;

[Table("DetallePedido")]
public partial class DetallePedido
{
    [Key]
    public int IdDetallePedido { get; set; }

    public int IdPedido { get; set; }

    public int IdPlato { get; set; }

    [StringLength(10)]
    public string TiempoEspera { get; set; } = null!;

    [Column(TypeName = "decimal(10, 3)")]
    public decimal? Precio { get; set; }

    public int Cantidad { get; set; }

    [StringLength(1000)]
    public string? Comentario { get; set; }

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

    [InverseProperty("IdDetallePedidoNavigation")]
    public virtual ICollection<DetallePedidoPersonalizado> DetallePedidoPersonalizados { get; set; } = new List<DetallePedidoPersonalizado>();

    [ForeignKey("IdPedido")]
    [InverseProperty("DetallePedidos")]
    [JsonIgnore]
    public virtual Pedido? IdPedidoNavigation { get; set; } = null!;

    [ForeignKey("IdPlato")]
    [InverseProperty("DetallePedidos")]
    [JsonIgnore]
    public virtual Plato? IdPlatoNavigation { get; set; } = null!;
}
