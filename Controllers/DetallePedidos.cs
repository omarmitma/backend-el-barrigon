using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto__Final.Tablas;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidos : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public DetallePedidos(SistemaRestaurante _context)
        { _dbcontext = _context; }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registrar([FromBody] DetallePedido data)
        {
            try
            {
                var DetaPedido_exis = _dbcontext.DetallePedidos.Where(e => e.Comentario == data.Comentario).ToList();
                if (DetaPedido_exis.Count>0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Detalle pedido ya esta registrado" });
                }
                await _dbcontext.DetallePedidos.AddAsync(data);
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
        public async Task<IActionResult> actualizar([FromBody] DetallePedido data)
        {
            try
            {
                var DetaPedido_exis = _dbcontext.DetallePedidos.Find(data.IdDetallePedido);
                if (DetaPedido_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Messsage = "Error no existe el Id" });
                }
                DetaPedido_exis.IdPedido = data.IdPedido;//is null ? DetaPedido_exis.IdPedido : data.IdPedido;
                DetaPedido_exis.IdPlato = data.IdPlato;//is null ? DetaPedido_exis.IdPlato : data.IdPlato;
                DetaPedido_exis.Precio = data.Precio is null ? DetaPedido_exis.Precio : data.Precio;
                DetaPedido_exis.Comentario =data.Comentario is null ? DetaPedido_exis.Comentario : data.Comentario;
                //DetaPedido_exis.FecRegistro = DateTime.Now;
                // DetaPedido_exis.HorRegistro = DateTime.Now.ToString("hh:mm:ss");
                //DetaPedido_exis.UsuRegistro = data.UsuRegistro is null ? DetaPedido_exis.UsuRegistro : data.UsuRegistro;
                DetaPedido_exis.FecModifica = DateTime.Now;
                DetaPedido_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                DetaPedido_exis.UsuModifica = data.UsuModifica is null ? DetaPedido_exis.UsuModifica : data.UsuModifica;
                DetaPedido_exis.Estado = data.Estado is null ? DetaPedido_exis.Estado : data.Estado;
                DetaPedido_exis.Accion = data.Accion is null ? DetaPedido_exis.Accion : data.Accion;

                _dbcontext.DetallePedidos.Update(DetaPedido_exis);
                await _dbcontext.AddRangeAsync();
                return StatusCode(StatusCodes.Status200OK, new { Message = DetaPedido_exis });
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
                var DetaPedido_exis = _dbcontext.DetallePedidos.Find(id);
                if (DetaPedido_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Messsage = "Error no existe el Id" });
                }
                _dbcontext.DetallePedidos.Remove(DetaPedido_exis);
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
                var DetaPedido_exis = _dbcontext.DetallePedidos.Find(id);
                if (DetaPedido_exis==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Messsage = "Error no existe el Id" });
                }
                return StatusCode(StatusCodes.Status200OK, new { Message = DetaPedido_exis });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
    }
}
