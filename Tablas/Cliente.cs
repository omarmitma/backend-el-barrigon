using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Proyecto__Final.Tablas;

[Table("Cliente")]
public partial class Cliente
{
    [Key]
    public int IdCliente { get; set; }

    public int IdUsuarioLogin { get; set; }

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

    [ForeignKey("IdUsuarioLogin")]
    [InverseProperty("Clientes")]
    [JsonIgnore]
    public virtual UsuarioLogin? IdUsuarioLoginNavigation { get; set; } = null!;

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<ClienteFuncione> ClienteFunciones { get; set; } = new List<ClienteFuncione>();
}
