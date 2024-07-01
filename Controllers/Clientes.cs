using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto__Final.Tablas;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Clientes : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public Clientes(SistemaRestaurante _context)
        { _dbcontext = _context; }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registrar([FromBody] Cliente data)
        {
            try
            {
                
                await _dbcontext.Clientes.AddAsync(data);
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
        public async Task<IActionResult> actualizar([FromBody] Cliente data)
        {
            try
            {
                var cliente_exis = _dbcontext.Clientes.Find(data.IdCliente);
                if (cliente_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "error no existe el Id" });
                }
                cliente_exis.IdUsuarioLogin = data.IdUsuarioLogin;
                cliente_exis.FecModifica = DateTime.Now;
                cliente_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                cliente_exis.UsuModifica = data.UsuModifica is null ? cliente_exis.UsuModifica : data.UsuModifica;
                cliente_exis.Estado = data.Estado is null ? cliente_exis.Estado : data.Estado;
                cliente_exis.Accion = data.Accion is null ? cliente_exis.Accion : data.Accion;

                _dbcontext.Clientes.Update(cliente_exis);
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
                var cliente_exis = _dbcontext.Clientes.Find(id);
                if (cliente_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Messsage = "Error no existe el Id" });
                }
                _dbcontext.Clientes.Remove(cliente_exis);
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
                var cliente_exis = _dbcontext.Clientes.Find(id);
                if (cliente_exis==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Messsage = "Error no existe el Id" });
                }
                return StatusCode(StatusCodes.Status200OK, new { Message = cliente_exis });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
    }
}
