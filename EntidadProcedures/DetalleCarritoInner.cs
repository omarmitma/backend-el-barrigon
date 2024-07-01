using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Proyecto__Final.Tablas;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto__Final.EntidadProcedures
{
    public class DetalleCarritoInner
    {
        public int IdDetalleCarrito { get; set; }

        public int IdCarrito { get; set; }

        public int IdPlato { get; set; }

        public string TiempoEspera { get; set; } = null!;

        public decimal? Precio { get; set; }

        public int Cantidad { get; set; }

        public string? Comentario { get; set; }
        public int FlagPedido { get; set; }

        public DateTime? FecRegistro { get; set; }

        public string? HorRegistro { get; set; }

        public int? UsuRegistro { get; set; }

        public DateTime? FecModifica { get; set; }

        public string? HorModifica { get; set; }

        public int? UsuModifica { get; set; }

        public int? Estado { get; set; }

        public int? Accion { get; set; }

        public string? NomPlato { get; set; }
        public string? UrlImagen { get; set; }
        public string? NomCategoria { get; set; }
        
        public virtual ICollection<DetalleCarritoPersonalizadoInner> DetalleCarritoPersonalizados { get; set; } = new List<DetalleCarritoPersonalizadoInner>();
    }
}
