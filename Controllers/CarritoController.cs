using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto__Final.EntidadProcedures;
using Proyecto__Final.Tablas;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public CarritoController(SistemaRestaurante _context)
        { _dbcontext = _context; }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registro([FromBody] Carrito data)
        {
            try
            {
                var cabMaestro_exis = _dbcontext.Carritos.Where(e => e.IdCliente == data.IdCliente).ToList();
                if (cabMaestro_exis.Count > 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Carrito con cliente ya existe", response = false });
                }
                await _dbcontext.Carritos.AddAsync(data);
                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { Message = data, response = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
        [HttpPut]
        [Route("actualizar")]
        public async Task<IActionResult> actualizar([FromBody] Carrito data)
        {
            try
            {
                var cabMaestro_exis = _dbcontext.Carritos.Find(data.IdCarrito);
                if (cabMaestro_exis == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Messsage = "Error no existe el nombre" });
                }
                cabMaestro_exis.IdMesa = data.IdMesa;
                cabMaestro_exis.IdCliente = data.IdCliente;
                cabMaestro_exis.CantidadPersonas = data.CantidadPersonas;
                cabMaestro_exis.TipoPago = data.TipoPago;
                cabMaestro_exis.TotalPago = data.TotalPago;
                cabMaestro_exis.Descuento = data.Descuento;
                cabMaestro_exis.DetalleCarritos = data.DetalleCarritos;
                cabMaestro_exis.FecModifica = DateTime.Now;
                cabMaestro_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                cabMaestro_exis.UsuModifica = data.UsuModifica is null ? cabMaestro_exis.UsuModifica : data.UsuModifica;
                cabMaestro_exis.Estado = data.Estado is null ? cabMaestro_exis.Estado : data.Estado;
                cabMaestro_exis.Accion = data.Accion is null ? cabMaestro_exis.Accion : data.Accion;

                _dbcontext.Carritos.Update(cabMaestro_exis);
                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { Message = cabMaestro_exis });
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
                var cabMaestro_exis = _dbcontext.Carritos.Find(id);
                if (cabMaestro_exis == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Messsage = "Error no existe el Id" });
                }
                _dbcontext.Carritos.Remove(cabMaestro_exis);
                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { Message = "Ok" });
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
                var cabMaestro_exis = _dbcontext.Carritos.Include(c => c.DetalleCarritos).FirstOrDefault(c => c.IdCarrito == id);

                if (cabMaestro_exis == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Messsage = "Error no existe el Id" });
                }
                return StatusCode(StatusCodes.Status200OK, new { Message = cabMaestro_exis });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }


        [HttpGet]
        [Route("buscar_ByCliente")]
        public async Task<IActionResult> buscar_ByCliente(int idCliente)
        {
            try
            {
                var lista = await _dbcontext.Set<CarritoInner>().FromSqlInterpolated($"EXEC usp_search_carrito_by_cliente {idCliente}").ToListAsync();
                CarritoInner carritoByCliente = new CarritoInner();

                if(lista.Count > 0)
                {
                    carritoByCliente = lista.FirstOrDefault();

                    var listaDetalle = await _dbcontext.Set<DetalleCarritoInner>().FromSqlInterpolated($"EXEC usp_search_detallecarrito_by_cliente {idCliente}").ToListAsync();

                    var listaDetallePersonalizado = await _dbcontext.Set<DetalleCarritoPersonalizadoInner>().FromSqlInterpolated($"EXEC usp_search_detallecarritoPersonalizado_by_cliente {idCliente}").ToListAsync();

                    listaDetalle.ForEach(cab =>
                    {
                        var listaDetalleNow = listaDetallePersonalizado.Where(d => d.IdDetalleCarrito == cab.IdDetalleCarrito).ToList();
                        if (listaDetalleNow.Count() > 0)
                        {
                            cab.DetalleCarritoPersonalizados = listaDetalleNow;
                        }
                    });

                    carritoByCliente.DetalleCarritos = listaDetalle;
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new { Message = "No hay carrito para este cliente", response = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Message = carritoByCliente, response = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("buscar_ByMesa")]
        public async Task<IActionResult> buscar_ByMesa(int idMesa)
        {
            try
            {
                var lista = await _dbcontext.Set<CarritoInner>().FromSqlInterpolated($"EXEC usp_search_carrito_by_mesa {idMesa}").ToListAsync();
                CarritoInner carritoByCliente = new CarritoInner();

                if (lista.Count > 0)
                {
                    carritoByCliente = lista.FirstOrDefault();

                    var listaDetalle = await _dbcontext.Set<DetalleCarritoInner>().FromSqlInterpolated($"EXEC usp_search_detallecarrito_by_mesa {idMesa}").ToListAsync();

                    var listaDetallePersonalizado = await _dbcontext.Set<DetalleCarritoPersonalizadoInner>().FromSqlInterpolated($"EXEC usp_search_detallecarritoPersonalizado_by_mesa {idMesa}").ToListAsync();

                    listaDetalle.ForEach(cab =>
                    {
                        var listaDetalleNow = listaDetallePersonalizado.Where(d => d.IdDetalleCarrito == cab.IdDetalleCarrito).ToList();
                        if (listaDetalleNow.Count() > 0)
                        {
                            cab.DetalleCarritoPersonalizados = listaDetalleNow;
                        }
                    });

                    carritoByCliente.DetalleCarritos = listaDetalle;
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new { Message = "No hay carrito para esta mesa", response = false });
                }

                return StatusCode(StatusCodes.Status200OK, new { Message = carritoByCliente, response = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }


        
    }
}
