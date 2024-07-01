using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Proyecto__Final.Tablas;

[Table("UsuarioLogin")]
public partial class UsuarioLogin
{
    [Key]
    public int IdUsuarioLogin { get; set; }

    [StringLength(200)]
    public string Nombre { get; set; } = null!;

    [StringLength(200)]
    public string Apellido { get; set; } = null!;

    public int TipoDocumento { get; set; }

    [StringLength(20)]
    public string NroDocumento { get; set; } = null!;

    [StringLength(300)]
    public string Direccion { get; set; } = null!;

    [StringLength(20)]
    public string Telefono { get; set; } = null!;

    [StringLength(20)]
    public string Telefono2 { get; set; } = null!;

    [StringLength(200)]
    public string Correo { get; set; } = null!;

    [StringLength(200)]
    public string Usuario { get; set; } = null!;

    [StringLength(600)]
    public string Clave { get; set; } = null!;

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

    [InverseProperty("IdUsuarioLoginNavigation")]
    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    [InverseProperty("IdUsuarioLoginNavigation")]
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
