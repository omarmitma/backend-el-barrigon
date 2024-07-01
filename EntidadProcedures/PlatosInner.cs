using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Proyecto__Final.Tablas;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto__Final.EntidadProcedures
{
    public class PlatosInner
    {
        public int IdPlato { get; set; }

        public int IdLinea { get; set; }

        public int IdSubLinea { get; set; }

        public int IdCategoria { get; set; }

        public string Nombre { get; set; } = null!;

        public string Abreviatura { get; set; } = null!;

        public decimal Precio { get; set; }

        public decimal PrecioAdicional { get; set; }

        public decimal PrecioOferta { get; set; }

        public string? UrlImagen { get; set; }

        public string? TiempoEspera { get; set; }

        public string? Observacion { get; set; }

        public DateTime? FecRegistro { get; set; }

        public string? HorRegistro { get; set; }

        public int? UsuRegistro { get; set; }

        public DateTime? FecModifica { get; set; }

        public string? HorModifica { get; set; }

        public int? UsuModifica { get; set; }

        public int? Estado { get; set; }

        public int? Accion { get; set; }

        public string NomCategoria { get; set; }

        public virtual ICollection<PlatosDetalleInner> DetallePlatos { get; set; } = new List<PlatosDetalleInner>();
    }
}
