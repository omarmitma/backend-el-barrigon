using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto__Final.Tablas;
using System.Xml.Linq;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Empleados : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public Empleados(SistemaRestaurante context)
        { _dbcontext = context; }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registro([FromBody] Empleado data)
        {
            try
            {
                await _dbcontext.Empleados.AddAsync(data);
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
        public async Task<IActionResult> actualizar([FromBody] Empleado data)
        {
            try
            {
                var emple_exis = _dbcontext.Empleados.Find(data.IdEmpleado);
                if (emple_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error No esxite el Id" });
                }
                emple_exis.Cargo = data.Cargo is null ? emple_exis.Cargo : data.Cargo;
                emple_exis.Sucursal = data.Sucursal is null ? emple_exis.Sucursal : data.Sucursal;
                emple_exis.FecModifica = DateTime.Now;
                emple_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                emple_exis.UsuModifica = data.UsuModifica is null ? emple_exis.UsuModifica : data.UsuModifica;
                emple_exis.Estado = data.Estado is null ? emple_exis.Estado : data.Estado;
                emple_exis.Accion = data.Accion is null ? emple_exis.Accion : data.Accion;

                //emple_exis.UsuarioLogins = data.UsuarioLogins;
                _dbcontext.Empleados.Update(emple_exis);
                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { Message = emple_exis });
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
                var emple_exis = _dbcontext.Empleados.Find(id);
                if (emple_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error No esxite el Id" });
                }
                _dbcontext.Empleados.Remove(emple_exis);
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
                var emple_exis = _dbcontext.Empleados.Find(id);
                if (emple_exis==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Error No esxite el Id" });
                }
                return StatusCode(StatusCodes.Status200OK, new { Message = emple_exis });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
    }
}
