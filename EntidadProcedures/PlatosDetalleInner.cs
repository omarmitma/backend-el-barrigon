﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto__Final.EntidadProcedures
{
    public class PlatosDetalleInner
    {
        public int IdDetallePlato { get; set; }

        public int IdPlato { get; set; }

        public int IdProducto { get; set; }

        public int IdMedida { get; set; }

        public decimal? Cantidad { get; set; }

        public decimal? CantidadMaxima { get; set; }

        public int FlagRequerido { get; set; }

        public DateTime? FecRegistro { get; set; }

        public string? HorRegistro { get; set; }

        public int? UsuRegistro { get; set; }

        public DateTime? FecModifica { get; set; }

        public string? HorModifica { get; set; }

        public int? UsuModifica { get; set; }

        public int? Estado { get; set; }

        public int? Accion { get; set; }
        public string? NomProducto { get; set; }
        public string? NomMedida { get; set; }
        public string? AbrMedida { get; set; }
        public decimal? PrecioUnit { get; set; }
        public string? UrlImagen { get; set; }
        
    }
}
