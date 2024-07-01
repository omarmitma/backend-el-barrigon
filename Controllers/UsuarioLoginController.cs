using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto__Final.Tablas;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioLoginController : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public UsuarioLoginController(SistemaRestaurante _context)
        { _dbcontext = _context; }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registrar([FromBody] UsuarioLogin data)
        {
            try
            {

                await _dbcontext.UsuarioLogins.AddAsync(data);
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
        public async Task<IActionResult> actualizar([FromBody] UsuarioLogin data)
        {
            try
            {
                var cliente_exis = _dbcontext.UsuarioLogins.Find(data.IdUsuarioLogin);
                if (cliente_exis == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "error no existe el Id" });
                }
                cliente_exis.FecModifica = DateTime.Now;
                cliente_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                cliente_exis.UsuModifica = data.UsuModifica is null ? cliente_exis.UsuModifica : data.UsuModifica;
                cliente_exis.Estado = data.Estado is null ? cliente_exis.Estado : data.Estado;
                cliente_exis.Accion = data.Accion is null ? cliente_exis.Accion : data.Accion;

                _dbcontext.UsuarioLogins.Update(cliente_exis);
                await _dbcontext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new { Message = cliente_exis });
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
                var cliente_exis = _dbcontext.UsuarioLogins.Find(id);
                if (cliente_exis == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Messsage = "Error no existe el Id" });
                }
                _dbcontext.UsuarioLogins.Remove(cliente_exis);
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
