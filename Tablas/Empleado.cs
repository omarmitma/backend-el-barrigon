using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Proyecto__Final.Tablas;

public partial class Empleado
{
    [Key]
    public int IdEmpleado { get; set; }

    public int IdUsuarioLogin { get; set; }

    public int? Cargo { get; set; }

    public int? Sucursal { get; set; }

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
    [InverseProperty("Empleados")]
    [JsonIgnore]
    public virtual UsuarioLogin? IdUsuarioLoginNavigation { get; set; } = null!;

}
