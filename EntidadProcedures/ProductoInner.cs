using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto__Final.EntidadProcedures
{
    public class ProductoInner
    {
        public int IdProducto { get; set; }

        public string Nombre { get; set; } = null!;

        public decimal? Precio { get; set; }

        public int IdLinea { get; set; }

        public int IdSubLinea { get; set; }

        public int IdCategoria { get; set; }

        public int IdUnidad { get; set; }

        public string? UrlImagen { get; set; }

        public string? Observacion { get; set; }

        public DateTime? FecRegistro { get; set; }

        public string? HorRegistro { get; set; }

        public int? UsuRegistro { get; set; }

        public DateTime? FecModifica { get; set; }

        public string? HorModifica { get; set; }

        public int? UsuModifica { get; set; }

        public int? Estado { get; set; }

        public int? Accion { get; set; }

        public string? NomCategoria { get; set; }

        public string? AbrUnidad { get; set; }

        public string? NomUnidad { get; set; }
    }
}
