using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Proyecto__Final.Tablas;

[Table("DetalleMaestro")]
public partial class DetalleMaestro
{
    [Key]
    public int IdDetalleMaestro { get; set; }

    public int IdCabezeraMaestro { get; set; }

    [StringLength(100)]
    public string? Nombre { get; set; }

    [StringLength(50)]
    public string? Abreviatura { get; set; }

    [StringLength(200)]
    public string? Campo1Nvarchar { get; set; }

    [StringLength(200)]
    public string? Campo2Nvarchar { get; set; }

    [StringLength(200)]
    public string? Campo3Nvarchar { get; set; }

    public int? Campo1Int { get; set; }

    public int? Campo2Int { get; set; }

    public int? Campo3Int { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    public decimal? Campo1Decimal { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    public decimal? Campo2Decimal { get; set; }

    [Column(TypeName = "decimal(10, 3)")]
    public decimal? Campo3Decimal { get; set; }

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

    [ForeignKey("IdCabezeraMaestro")]
    [InverseProperty("DetalleMaestros")]
    [JsonIgnore]
    public virtual CabezeraMaestro? IdCabezeraMaestroNavigation { get; set; } = null!;
}
