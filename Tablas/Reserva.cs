using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Proyecto__Final.Tablas;

public partial class Reserva
{
    [Key]
    public int IdReservas { get; set; }

    public int? IdCliente { get; set; }

    public int? IdMesa { get; set; }

    [Column(TypeName = "date")]
    public DateTime FecReserva { get; set; }

    [StringLength(20)]
    public string? HorReserva { get; set; }

    [StringLength(20)]
    public string? HorMaxima { get; set; }

    public int? CantidadPersonas { get; set; }

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

    [ForeignKey("IdCliente")]
    [InverseProperty("Reservas")]
    [JsonIgnore]
    public virtual Cliente? IdClienteNavigation { get; set; }

    [ForeignKey("IdMesa")]
    [InverseProperty("Reservas")]
    [JsonIgnore]
    public virtual Mesa? IdMesaNavigation { get; set; }
}
