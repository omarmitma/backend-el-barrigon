using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto__Final.Tablas;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidoPersonalizados : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public DetallePedidoPersonalizados(SistemaRestaurante _context)
        { _dbcontext = _context; }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registrar([FromBody] DetallePedidoPersonalizado data)
        {
            try
            {
                var PedPersonali_exis = _dbcontext.DetallePedidoPersonalizados.Where(e => e.Cantidad == data.Cantidad).ToList();
                if (PedPersonali_exis.Count>0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "El Pedido Personalizado Ya existe ya existe" });
                }
                await _dbcontext.DetallePedidoPersonalizados.AddAsync(data);
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
        public async Task<IActionResult> actualizar([FromBody] DetallePedidoPersonalizado data)
        {
            try
            {
                var PedPersonali_exis = _dbcontext.DetallePedidoPersonalizados.Find(data.IdDetallePedidoPersonalizado);
                if (PedPersonali_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Messsage = "Error no existe el Id" });
                }
                PedPersonali_exis.IdDetallePedido = data.IdDetallePedido;//is null ? PedPersonali_exis.IdDetallePedido : data.IdDetallePedido;
                PedPersonali_exis.IdProducto = data.IdProducto;//is null ? PedPersonali_exis.IdProducto : data.IdProducto;
                PedPersonali_exis.IdMedida = data.IdMedida ;//is null ? PedPersonali_exis.IdMedida : data.IdMedida;
                PedPersonali_exis.Cantidad = data.Cantidad;//is null ? PedPersonali_exis.Cantidad : data.Cantidad;
                PedPersonali_exis.FlagRequerido =data.FlagRequerido;//is null ? PedPersonali_exis.FlagRequerido: data.FlagRequerido;
                //PedPersonali_exis.FecRegistro = DateTime.Now;
                //PedPersonali_exis.HorRegistro = DateTime.Now.ToString("hh:mm:ss");
                //PedPersonali_exis.UsuRegistro = data.UsuRegistro is null ? PedPersonali_exis.UsuRegistro : data.UsuRegistro;
                PedPersonali_exis.FecModifica = DateTime.Now;
                PedPersonali_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                PedPersonali_exis.UsuModifica = data.UsuModifica is null ? PedPersonali_exis.UsuModifica : data.UsuModifica;
                PedPersonali_exis.Estado = data.Estado is null ? PedPersonali_exis.Estado : data.Estado;
                PedPersonali_exis.Accion = data.Accion is null ? PedPersonali_exis.Accion : data.Accion;

                _dbcontext.DetallePedidoPersonalizados.Update(PedPersonali_exis);
                await _dbcontext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new { Message = PedPersonali_exis });
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
                var PedPersonali_exis = _dbcontext.DetallePedidoPersonalizados.Find(id);
                if (PedPersonali_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Messsage = "Error no existe el Id" });
                }
                _dbcontext.DetallePedidoPersonalizados.Remove(PedPersonali_exis);
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
                var PedPersonali_exis = _dbcontext.DetallePedidoPersonalizados.Find(id);
                if (PedPersonali_exis==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Messsage = "Error no existe el Id" });
                }
                return StatusCode(StatusCodes.Status200OK, new { Message = PedPersonali_exis });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }

        }
    }
}
