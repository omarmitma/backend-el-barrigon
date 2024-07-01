using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Proyecto__Final.Tablas;

public partial class MesaFuncione
{
    [Key]
    public int IdMesaFunciones { get; set; }

    public int IdMesa { get; set; }

    [StringLength(100)]
    public string? Descripcion { get; set; }

    [StringLength(20)]
    public string? HorSolicitada { get; set; }

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
    public int? IdFuncion { get; set; }

    [ForeignKey("IdMesa")]
    [InverseProperty("MesaFunciones")]
    [JsonIgnore]
    public virtual Mesa? IdMesaNavigation { get; set; } = null!;
}
