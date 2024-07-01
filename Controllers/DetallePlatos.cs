using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto__Final.Tablas;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePlatos : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public DetallePlatos(SistemaRestaurante _context)
        { _dbcontext = _context; }
        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registrar([FromBody] DetallePlato data)
        {
            try
            {
                var detallePlato_exis = _dbcontext.DetallePlatos.Where(e => e.FlagRequerido == data.FlagRequerido).ToList();
                if (detallePlato_exis.Count>0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Detalle Plato ya existe" });
                }
                await _dbcontext.DetallePlatos.AddAsync(data);
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
        public async Task<IActionResult> actualziar([FromBody] DetallePlato data)
        {
            try
            {
                var detallePlato_exis = _dbcontext.DetallePlatos.Find(data.IdDetallePlato);
                if (detallePlato_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error No existe el Id" });
                }
                detallePlato_exis.IdPlato = data.IdPlato;// is null ? detallePlato_exis.IdPlato : data.IdPlato;
                detallePlato_exis.IdProducto = data.IdProducto;//is null ? detallePlato_exis.IdProducto : data.IdProducto;
                detallePlato_exis.IdMedida = data.IdMedida ;//is null ? detallePlato_exis.IdMedida : data.IdMedida;
                detallePlato_exis.Cantidad = data.Cantidad;//is null ? detallePlato_exis.Cantidad : data.Cantidad;
                detallePlato_exis.FlagRequerido = data.FlagRequerido;//is null ? detallePlato_exis.FlagRequerido : data.FlagRequerido;
                //detallePlato_exis.FecRegistro = DateTime.Now;
                //detallePlato_exis.HorRegistro = DateTime.Now.ToString("hh:mm:ss");
                //detallePlato_exis.UsuRegistro = data.UsuRegistro is null ? detallePlato_exis.UsuRegistro : data.UsuRegistro;
                detallePlato_exis.FecModifica = DateTime.Now;
                detallePlato_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                detallePlato_exis.UsuModifica = data.UsuModifica is null ? detallePlato_exis.UsuModifica : data.UsuModifica;
                detallePlato_exis.Estado = data.Estado is null ? detallePlato_exis.Estado : data.Estado;
                detallePlato_exis.Accion = data.Accion is null ? detallePlato_exis.Accion : data.Accion;

                _dbcontext.DetallePlatos.Update(detallePlato_exis);
                await _dbcontext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new { Message = detallePlato_exis });
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
                var detallePlato_exis = _dbcontext.DetallePlatos.Find(id);
                if (detallePlato_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error no existe el Id" });
                }
                _dbcontext.DetallePlatos.Remove(detallePlato_exis);
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
                var detallePlato_exis = _dbcontext.DetallePlatos.Find(id);
                if (detallePlato_exis==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Error No existe el Id" });
                }
                return StatusCode(StatusCodes.Status200OK, new { Message = detallePlato_exis });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
    }
}
