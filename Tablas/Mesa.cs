using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto__Final.Tablas;

[Table("Mesa")]
public partial class Mesa
{
    [Key]
    public int IdMesa { get; set; }

    [StringLength(20)]
    public string Nombre { get; set; } = null!;

    public int Capacidad { get; set; }

    public int? EstadoMesa { get; set; }

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

    [InverseProperty("IdMesaNavigation")]
    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    [InverseProperty("IdMesaNavigation")]
    public virtual ICollection<MesaFuncione> MesaFunciones { get; set; } = new List<MesaFuncione>();

    [InverseProperty("IdMesaNavigation")]
    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    [InverseProperty("IdMesaNavigation")]
    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
