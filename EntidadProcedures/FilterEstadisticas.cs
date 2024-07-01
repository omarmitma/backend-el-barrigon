namespace Proyecto__Final.EntidadProcedures
{
    public class FilterEstadisticas
    {
        public int CantidadVentas { get; set; }
        public int PedidosPendiente { get; set; }
        public int PedidosCancelados { get; set; }
        public int TotalPedido { get; set; }
        public decimal TotalVentaNeto { get; set; }
        public decimal VentaPromedio { get; set; }
        public decimal MargenPorcentual { get; set; }
        public decimal MargenTotal { get; set; }
        public decimal PagoVisa { get; set; }
        public decimal PagosMasterCard { get; set; }
        public decimal PagosEfectivos { get; set; }
        public decimal PagosOtros { get; set; }

        public virtual ICollection<RankingPlatos> rankingPlato { get; set; } = new List<RankingPlatos>();
        public virtual ICollection<RankingCategoria> rankingCategoria { get; set; } = new List<RankingCategoria>();
    }
}
