using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto__Final.EntidadProcedures;
using Proyecto__Final.Tablas;
using System.Xml.Linq;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pedidos : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public Pedidos(SistemaRestaurante context)
        { _dbcontext = context; }
        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registrar([FromBody] Pedido data)
        {
            try
            {
                var pedido_exis = _dbcontext.Pedidos.Where(e => e.Descuento == data.Descuento).ToList();
                if (pedido_exis.Count>0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Pedido ya esta registrado" });
                }
                await _dbcontext.Pedidos.AddAsync(data);
                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { Message = data });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("confirmPedido")]
        public async Task<IActionResult> confirmPedido([FromBody] Pedido data)
        {
            try
            {

                var lista = await _dbcontext.Set<CarritoInner>().FromSqlInterpolated($"EXEC usp_search_carrito_by_cliente {data.IdCliente}").ToListAsync();
                CarritoInner carritoByCliente = new CarritoInner();

                if (lista.Count > 0)
                {
                    carritoByCliente = lista.FirstOrDefault();

                    var listaDetalle = await _dbcontext.Set<DetalleCarritoInner>().FromSqlInterpolated($"EXEC usp_search_detallecarrito_by_cliente {data.IdCliente}").ToListAsync();

                    var listaDetallePersonalizado = await _dbcontext.Set<DetalleCarritoPersonalizadoInner>().FromSqlInterpolated($"EXEC usp_search_detallecarritoPersonalizado_by_cliente {data.IdCliente}").ToListAsync();

                    listaDetalle.ForEach(cab =>
                    {
                        var listaDetalleNow = listaDetallePersonalizado.Where(d => d.IdDetalleCarrito == cab.IdDetalleCarrito).ToList();
                        if (listaDetalleNow.Count() > 0)
                        {
                            cab.DetalleCarritoPersonalizados = listaDetalleNow;
                        }

                        var updateDetalleCarrito = _dbcontext.DetalleCarritos.Find(cab.IdDetalleCarrito);
                        updateDetalleCarrito.FlagPedido = 1;

                        _dbcontext.DetalleCarritos.Update(updateDetalleCarrito);
                    });

                    carritoByCliente.DetalleCarritos = listaDetalle;

                    data.IdMesa = carritoByCliente.IdMesa;
                    data.CantidadPersonas = carritoByCliente.CantidadPersonas;
                    data.TipoPago = 29;
                    data.TotalPago = carritoByCliente.TotalPago;
                    data.Descuento = carritoByCliente.Descuento;
                    data.Estado = 5;

                    listaDetalle.ForEach(deta =>
                    {
                        if (deta.FlagPedido == 0)
                        {
                            DetallePedido detaPedido = new DetallePedido();
                            detaPedido.IdPlato = deta.IdPlato;
                            detaPedido.TiempoEspera = deta.TiempoEspera;
                            detaPedido.Precio = deta.Precio;
                            detaPedido.Cantidad = deta.Cantidad;
                            detaPedido.Comentario = deta.Comentario;

                            foreach (var detaPersonalizado in deta.DetalleCarritoPersonalizados)
                            {
                                DetallePedidoPersonalizado addDetaPersonalizado = new DetallePedidoPersonalizado();
                                addDetaPersonalizado.IdProducto = detaPersonalizado.IdProducto;
                                addDetaPersonalizado.IdMedida = detaPersonalizado.IdMedida;
                                addDetaPersonalizado.Cantidad = detaPersonalizado.Cantidad;
                                addDetaPersonalizado.FlagRequerido = detaPersonalizado.FlagRequerido;
                                detaPedido.DetallePedidoPersonalizados.Add(addDetaPersonalizado);
                            }

                            data.DetallePedidos.Add(detaPedido);
                        }
                    });

                    var pedidoCliente = _dbcontext.Pedidos.Where(p => p.IdCliente == data.IdCliente && p.Estado != 4 && p.Estado != 6 && p.Estado != 2).ToList();

                    if(pedidoCliente.Count() > 0)
                    {
                        foreach(var detalle in data.DetallePedidos)
                        {
                            detalle.IdPedido = pedidoCliente.FirstOrDefault().IdPedido;
                            detalle.IdDetallePedido = 0;
                            await _dbcontext.DetallePedidos.AddAsync(detalle);
                        }
                    }
                    else
                    {
                        await _dbcontext.Pedidos.AddAsync(data);
                    }
                
                    await _dbcontext.SaveChangesAsync();

                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new { Message = "No hay carrito para este cliente", response = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Message = data });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
        [HttpPut]
        [Route("actualizar")]
        public async Task<IActionResult> actualizar([FromBody] Pedido data)
        {
            try
            {
                var pedido_exis = _dbcontext.Pedidos.Find(data.IdPedido);
                if (pedido_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error no existe el Pedido" });
                }
                pedido_exis.IdMesa = data.IdMesa;//is null ? pedido_exis.IdMesa : data.IdMesa;
                pedido_exis.TotalPago = data.TotalPago is null ? pedido_exis.TotalPago : data.TotalPago;
                pedido_exis.Descuento = data.Descuento is null ? pedido_exis.Descuento : data.Descuento;
                //pedido_exis.FecRegistro = DateTime.Now;
                //pedido_exis.HorRegistro = DateTime.Now.ToString("hh:mm:ss");
                //pedido_exis.UsuRegistro = data.UsuRegistro is null ? pedido_exis.UsuRegistro : data.UsuRegistro;
                pedido_exis.FecModifica = DateTime.Now;
                pedido_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                pedido_exis.UsuModifica = data.UsuModifica is null ? pedido_exis.UsuModifica : data.UsuModifica;
                pedido_exis.Estado = data.Estado is null ? pedido_exis.Estado : data.Estado;
                pedido_exis.Accion = data.Accion is null ? pedido_exis.Accion : data.Accion;

                _dbcontext.Pedidos.Update(pedido_exis);
                await _dbcontext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new { Message = pedido_exis });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> eliminar(int id)
        {
            try
            {
                var pedido_exis = _dbcontext.Pedidos.Find(id);
                if (pedido_exis == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error no existe el Pedido" });
                }
                _dbcontext.Pedidos.Remove(pedido_exis);
                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { Message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
        [HttpGet]
        [Route("buscar")]
        public async Task<IActionResult> buscar(int id)
        {
            try
            {
                var pedido_exis = _dbcontext.Pedidos.Find(id);
                if (pedido_exis == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Error no existe el Pedido" });
                }
                return StatusCode(StatusCodes.Status200OK, new { Message = pedido_exis });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("buscar_All")]
        public async Task<IActionResult> buscar_All()
        {
            try
            {
                var lista = await _dbcontext.Set<PedidoInner>().FromSqlInterpolated($"EXEC usp_search_pedido").ToListAsync();

                var listaDetalle = await _dbcontext.Set<DetallePedidoInner>().FromSqlInterpolated($"EXEC usp_search_detallepedido").ToListAsync();

                var listaDetallePersonalizado = await _dbcontext.Set<DetallePedidoPersonalizadoInner>().FromSqlInterpolated($"EXEC usp_search_detallepedidoPersonalizado").ToListAsync();

                lista.ForEach(pedido =>
                {

                    var listaPedidoDetalleNow = listaDetalle.Where(d => d.IdPedido == pedido.IdPedido).ToList();
                    listaPedidoDetalleNow.ForEach(cab =>
                    {
                        var listaDetalleNow = listaDetallePersonalizado.Where(d => d.IdDetallePedido == cab.IdDetallePedido).ToList();
                        if (listaDetalleNow.Count() > 0)
                        {
                            cab.DetallePedidoPersonalizados = listaDetalleNow;
                        }
                    });
                    pedido.DetallePedidos = listaPedidoDetalleNow;
                });


                return StatusCode(StatusCodes.Status200OK, new { Message = lista, response = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("buscar_All_Filter_Date")]
        public async Task<IActionResult> buscar_All_Filter_Date(string fecIni, string fecFin)
        {
            try
            {
                var lista = await _dbcontext.Set<PedidoInner>().FromSqlInterpolated($"EXEC usp_search_pedido_by_fecha {fecIni}, {fecFin}").ToListAsync();

                var listaDetalle = await _dbcontext.Set<DetallePedidoInner>().FromSqlInterpolated($"EXEC usp_search_detallepedido_by_fecha {fecIni}, {fecFin}").ToListAsync();

                var listaDetallePersonalizado = await _dbcontext.Set<DetallePedidoPersonalizadoInner>().FromSqlInterpolated($"EXEC usp_search_detallepedidoPersonalizado_by_fecha {fecIni}, {fecFin}").ToListAsync();

                lista.ForEach(pedido =>
                {

                    var listaPedidoDetalleNow = listaDetalle.Where(d => d.IdPedido == pedido.IdPedido).ToList();
                    listaDetalle.ForEach(cab =>
                    {
                        var listaDetalleNow = listaDetallePersonalizado.Where(d => d.IdDetallePedido == cab.IdDetallePedido).ToList();
                        if (listaDetalleNow.Count() > 0)
                        {
                            cab.DetallePedidoPersonalizados = listaDetalleNow;
                        }
                    });
                    pedido.DetallePedidos = listaDetalle;
                });


                return StatusCode(StatusCodes.Status200OK, new { Message = lista, response = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        
        [HttpPut]
        [Route("terminar_pedido")]
        public async Task<IActionResult> terminar_pedido([FromBody] Pedido data)
        {
            try
            {
                var pedido_exis = _dbcontext.Pedidos.Find(data.IdPedido);

                if (pedido_exis == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Error no existe el Pedido" });
                }

                var carrito = _dbcontext.Carritos.Where(c => c.IdCliente == data.IdCliente).FirstOrDefault();
                var detalleCarritoPedido = _dbcontext.DetalleCarritos.Where(dc => dc.IdCarrito == carrito.IdCarrito && dc.FlagPedido == 1);
                _dbcontext.DetalleCarritos.RemoveRange(detalleCarritoPedido);

                pedido_exis.Estado = 4;

                Venta addVenta = new Venta();
                addVenta.IdCliente = pedido_exis.IdCliente;
                addVenta.IdPedido = pedido_exis.IdPedido;
                addVenta.CantidadPersonas = pedido_exis.CantidadPersonas;
                addVenta.TipoPago = pedido_exis.TipoPago;
                addVenta.TotalPago = pedido_exis.TotalPago;
                addVenta.Descuento = pedido_exis.Descuento;
                addVenta.FecRegistro = DateTime.Now;
                addVenta.HorRegistro = DateTime.Now.ToString("hh:mm:ss");
                addVenta.FecModifica = DateTime.Now;
                addVenta.HorModifica = DateTime.Now.ToString("hh:mm:ss");

                _dbcontext.Ventas.Add(addVenta);
                _dbcontext.Pedidos.Update(pedido_exis);
                await _dbcontext.SaveChangesAsync();

                List<DetalleCarrito> detaCarrito = await _dbcontext.DetalleCarritos.Where(s => s.IdCarrito == carrito.IdCarrito).ToListAsync();
                carrito.TotalPago = 0;
                detaCarrito.ForEach(d => carrito.TotalPago += d.Precio);

                _dbcontext.Carritos.Update(carrito);

                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { Message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("cancelar_pedido")]
        public async Task<IActionResult> cancelar_pedido([FromBody] Pedido data)
        {
            try
            {
                var pedido_exis = _dbcontext.Pedidos.Find(data.IdPedido);

                if (pedido_exis == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Error no existe el Pedido" });
                }

                var carrito = _dbcontext.Carritos.Where(c => c.IdCliente == data.IdCliente).FirstOrDefault();
                var detalleCarritoPedido = _dbcontext.DetalleCarritos.Where(dc => dc.IdCarrito == carrito.IdCarrito && dc.FlagPedido == 1);
                _dbcontext.DetalleCarritos.RemoveRange(detalleCarritoPedido);

                List<DetalleCarrito> detaCarrito = await _dbcontext.DetalleCarritos.Where(s => s.IdCarrito == carrito.IdCarrito).ToListAsync();
                carrito.TotalPago = 0;
                detaCarrito.ForEach(d => carrito.TotalPago += d.Precio);

                _dbcontext.Carritos.Update(carrito);

                pedido_exis.Estado = 6;

                _dbcontext.Pedidos.Update(pedido_exis);
                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { Message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

    }
}
