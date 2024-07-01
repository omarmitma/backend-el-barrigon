using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto__Final.Tablas;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleCarritoController : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public DetalleCarritoController(SistemaRestaurante _context)
        { _dbcontext = _context; }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registro([FromBody] DetalleCarrito data)
        {
            try
            {
                await _dbcontext.DetalleCarritos.AddAsync(data);
                var carrito = _dbcontext.Carritos.Find(data.IdCarrito);

                await _dbcontext.SaveChangesAsync();

                List<DetalleCarrito> detaCarrito = await _dbcontext.DetalleCarritos.Where(s => s.IdCarrito == data.IdCarrito).ToListAsync();
                carrito.TotalPago = 0;
                detaCarrito.ForEach(d => carrito.TotalPago += d.Precio);
                _dbcontext.Carritos.Update(carrito);

                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { Message = data });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
        [HttpPut]
        [Route("actualizar")]
        public async Task<IActionResult> actualizar([FromBody] DetalleCarrito data)
        {
            try
            {
                var cabMaestro_exis = _dbcontext.DetalleCarritos.Find(data.IdDetalleCarrito);

                List<DetalleCarritoPersonalizado> oDeta = await _dbcontext.DetalleCarritoPersonalizados.Where(s => s.IdDetalleCarrito == data.IdDetalleCarrito).ToListAsync();

                //Eliminar registros en base al ID de la cabecera
                _dbcontext.DetalleCarritoPersonalizados.RemoveRange(oDeta);

                foreach(var deta in data.DetalleCarritoPersonalizados){
                    deta.IdDetalleCarritoPersonalizado = 0;
                }

                if (cabMaestro_exis == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Messsage = "Error no existe el nombre" });
                }
                cabMaestro_exis.IdPlato = data.IdPlato;
                cabMaestro_exis.TiempoEspera = data.TiempoEspera;
                cabMaestro_exis.Precio = data.Precio;
                cabMaestro_exis.Cantidad = data.Cantidad;
                cabMaestro_exis.Comentario = data.Comentario;
                cabMaestro_exis.FlagPedido = data.FlagPedido;
                cabMaestro_exis.DetalleCarritoPersonalizados = data.DetalleCarritoPersonalizados;
                cabMaestro_exis.FecModifica = DateTime.Now;
                cabMaestro_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                cabMaestro_exis.UsuModifica = data.UsuModifica is null ? cabMaestro_exis.UsuModifica : data.UsuModifica;
                cabMaestro_exis.Estado = data.Estado is null ? cabMaestro_exis.Estado : data.Estado;
                cabMaestro_exis.Accion = data.Accion is null ? cabMaestro_exis.Accion : data.Accion;

                _dbcontext.DetalleCarritos.Update(cabMaestro_exis);

                var carrito = _dbcontext.Carritos.Find(data.IdCarrito);
                List<DetalleCarrito> detaCarrito = await _dbcontext.DetalleCarritos.Where(s => s.IdCarrito == data.IdCarrito).ToListAsync();
                carrito.TotalPago = 0;
                detaCarrito.ForEach(d => carrito.TotalPago += d.Precio);
                
                _dbcontext.Carritos.Update(carrito);

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
                var cabMaestro_exis = _dbcontext.DetalleCarritos.Find(id);

                if (cabMaestro_exis == null)
                {
                    return StatusCode(StatusCodes.Status200OK, new { Messsage = "Error no existe el Id" });
                }

                int idCarrito = cabMaestro_exis.IdCarrito;
                List<DetalleCarritoPersonalizado> oDeta = await _dbcontext.DetalleCarritoPersonalizados.Where(s => s.IdDetalleCarrito == id).ToListAsync();

                //Eliminar registros en base al ID de la cabecera
                _dbcontext.DetalleCarritoPersonalizados.RemoveRange(oDeta);
                _dbcontext.DetalleCarritos.Remove(cabMaestro_exis);
                await _dbcontext.SaveChangesAsync();

                var carrito = _dbcontext.Carritos.Find(idCarrito);
                List<DetalleCarrito> detaCarrito = await _dbcontext.DetalleCarritos.Where(s => s.IdCarrito == idCarrito).ToListAsync();
                carrito.TotalPago = 0;
                detaCarrito.ForEach(d => carrito.TotalPago += d.Precio);

                _dbcontext.Carritos.Update(carrito);

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
                var cabMaestro_exis = _dbcontext.DetalleCarritos.Include(c => c.DetalleCarritoPersonalizados).FirstOrDefault(c => c.IdDetalleCarrito == id);

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
    }
}
