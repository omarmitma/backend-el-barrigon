using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto__Final.Tablas;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabezeraMaestros : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public CabezeraMaestros(SistemaRestaurante _context)
        {  _dbcontext = _context; }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registro([FromBody] CabezeraMaestro data)
        {
            try
            {
                var cabMaestro_exis = _dbcontext.CabezeraMaestros.Where(e => e.Nombre ==data.Nombre).ToList();
                if (cabMaestro_exis.Count>0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Cabeceara ya existe" });
                }
                await _dbcontext.CabezeraMaestros.AddAsync(data);
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
        public async Task<IActionResult> actualizar([FromBody] CabezeraMaestro data)
        {
            try
            {
                var cabMaestro_exis = _dbcontext.CabezeraMaestros.Find(data.IdCabezeraMaestro);
                if (cabMaestro_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Messsage = "Error no existe el nombre" });
                }
                cabMaestro_exis.Nombre = data.Nombre is null ? cabMaestro_exis.Nombre :data.Nombre;
                //cabMaestro_exis.FecRegistro = DateTime.Now;
                //cabMaestro_exis.HorRegistro = DateTime.Now.ToString("hh:mm:ss");
                //cabMaestro_exis.UsuRegistro = data.UsuRegistro is null ? cabMaestro_exis.UsuRegistro :data.UsuRegistro; 
                cabMaestro_exis.FecModifica = DateTime.Now;
                cabMaestro_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                cabMaestro_exis.UsuModifica = data.UsuModifica is null ? cabMaestro_exis.UsuModifica :data.UsuModifica;
                cabMaestro_exis.Estado = data.Estado is null ? cabMaestro_exis.Estado :data.Estado;
                cabMaestro_exis.Accion = data.Accion is null ? cabMaestro_exis.Accion :data.Accion;

                _dbcontext.CabezeraMaestros.Update(cabMaestro_exis);
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
                var cabMaestro_exis = _dbcontext.CabezeraMaestros.Find(id);
                if (cabMaestro_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Messsage = "Error no existe el Id" });
                }
                _dbcontext.CabezeraMaestros.Remove(cabMaestro_exis);
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
                var cabMaestro_exis = _dbcontext.CabezeraMaestros.Include(c => c.DetalleMaestros).FirstOrDefault(c => c.IdCabezeraMaestro == id);

                if (cabMaestro_exis==null)
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
