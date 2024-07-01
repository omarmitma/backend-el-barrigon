using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Proyecto__Final.Tablas;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto__Final.EntidadProcedures
{
    public class CarritoInner
    {
        public int IdCarrito { get; set; }
        public int IdMesa { get; set; }

        public int IdCliente { get; set; }

        public int CantidadPersonas { get; set; }

        public int TipoPago { get; set; }

        public decimal? TotalPago { get; set; }

        public decimal? Descuento { get; set; }

        public DateTime? FecRegistro { get; set; }

        public string? HorRegistro { get; set; }

        public int? UsuRegistro { get; set; }

        public DateTime? FecModifica { get; set; }

        public string? HorModifica { get; set; }

        public int? UsuModifica { get; set; }

        public int? Estado { get; set; }

        public int? Accion { get; set; }

        public string? NomMesa { get; set; }

        public string? NomCliente { get; set; }


        public string? NomTipoPago { get; set; }

        public virtual ICollection<DetalleCarritoInner> DetalleCarritos { get; set; } = new List<DetalleCarritoInner>();
    }
}
