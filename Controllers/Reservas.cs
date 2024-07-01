using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto__Final.Tablas;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Reservas : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public Reservas(SistemaRestaurante _context)
        { _dbcontext = _context; }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registrar([FromBody] Reserva data)
        {
            try
            {
                var Reservas_exis = _dbcontext.Reservas.Where(e => e.CantidadPersonas == data.CantidadPersonas).ToList();
                if (Reservas_exis.Count>0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Ya esta Reservado" });
                }
                await _dbcontext.Reservas.AddAsync(data);
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
        public async Task<IActionResult> actualizar([FromBody] Reserva data)
        {
            try
            {
                var Reservas_exis = _dbcontext.Reservas.Find(data.IdReservas);
                if (Reservas_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error la Reserva no esta registrada" });
                }
                Reservas_exis.IdCliente = data.IdCliente;//is null ? Reservas_exis.IdCliente : data.IdCliente;
                // Reservas_exis.FecRegistro = DateTime.Now;
                //Reservas_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                //Reservas_exis.UsuRegistro = data.UsuRegistro is null ? Reservas_exis.UsuRegistro : data.UsuRegistro;
                Reservas_exis.FecModifica = DateTime.Now;
                Reservas_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                Reservas_exis.UsuModifica = data.UsuModifica is null ? Reservas_exis.UsuModifica : data.UsuModifica;
                Reservas_exis.Estado = data.Estado is null ? Reservas_exis.Estado : data.Estado;
                Reservas_exis.Accion = data.Accion is null ? Reservas_exis.Accion : data.Accion;

                _dbcontext.Reservas.Update(Reservas_exis);
                await _dbcontext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new { Message = Reservas_exis });
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
                var Reservas_exis = _dbcontext.Reservas.Find(id);
                if (Reservas_exis == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error la Reserva no esta registrada" });
                }
                _dbcontext.Reservas.Remove(Reservas_exis);
                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status400BadRequest, new { Message = "ok" });
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
                var Reservas_exis = _dbcontext.Reservas.Find(id);
                if (Reservas_exis == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Error la Reserva no esta registrada" });
                }
                return StatusCode(StatusCodes.Status200OK, new { Message = Reservas_exis });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
    }
}