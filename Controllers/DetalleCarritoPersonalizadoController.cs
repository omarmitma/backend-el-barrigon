using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto__Final.Tablas;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleCarritoPersonalizadoController : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public DetalleCarritoPersonalizadoController(SistemaRestaurante _context)
        { _dbcontext = _context; }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registro([FromBody] DetalleCarritoPersonalizado data)
        {
            try
            {
                await _dbcontext.DetalleCarritoPersonalizados.AddAsync(data);
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
        public async Task<IActionResult> actualizar([FromBody] DetalleCarritoPersonalizado data)
        {
            try
            {
                var cabMaestro_exis = _dbcontext.DetalleCarritoPersonalizados.Find(data.IdDetalleCarritoPersonalizado);
                if (cabMaestro_exis == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Messsage = "Error no existe el nombre" });
                }
                cabMaestro_exis.IdProducto = data.IdProducto;
                cabMaestro_exis.IdMedida = data.IdMedida;
                cabMaestro_exis.Cantidad = data.Cantidad;
                cabMaestro_exis.FlagRequerido = data.FlagRequerido;
                cabMaestro_exis.FecModifica = DateTime.Now;
                cabMaestro_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                cabMaestro_exis.UsuModifica = data.UsuModifica is null ? cabMaestro_exis.UsuModifica : data.UsuModifica;
                cabMaestro_exis.Estado = data.Estado is null ? cabMaestro_exis.Estado : data.Estado;
                cabMaestro_exis.Accion = data.Accion is null ? cabMaestro_exis.Accion : data.Accion;

                _dbcontext.DetalleCarritoPersonalizados.Update(cabMaestro_exis);
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
                var cabMaestro_exis = _dbcontext.DetalleCarritoPersonalizados.Find(id);
                if (cabMaestro_exis == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Messsage = "Error no existe el Id" });
                }
                _dbcontext.DetalleCarritoPersonalizados.Remove(cabMaestro_exis);
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
                var cabMaestro_exis = _dbcontext.DetalleCarritoPersonalizados.FirstOrDefault(c => c.IdDetalleCarrito == id);

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
