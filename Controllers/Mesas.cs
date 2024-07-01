 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Proyecto__Final.Tablas;
using Proyecto__Final.hubs;
using Microsoft.EntityFrameworkCore;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Mesas : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        private readonly IHubContext<llamarMesero> _hubContext;
        public Mesas(SistemaRestaurante _context, IHubContext<llamarMesero> hubContext)
        {  
            _dbcontext = _context;
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessage(string user, string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
            return StatusCode(StatusCodes.Status200OK, new { Message = "Mensaje recibido" });
        }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registrar([FromBody] Mesa data)
        {
            try
            {
                var Mesa_exis = _dbcontext.Mesas.Where(e => e.Nombre == data.Nombre).ToList();
                if (Mesa_exis.Count>0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Esta Mesa ya esta en Uso" });
                }
                await _dbcontext.Mesas.AddAsync(data);
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
        public async Task<IActionResult> actualizar([FromBody] Mesa data)
        {
            try
            {
                var Mesa_exis = _dbcontext.Mesas.Find(data.IdMesa);
                if (Mesa_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error no existe la Mesa" });
                }
                Mesa_exis.Nombre = data.Nombre;//is null ? Mesa_exis.NroMesa : data.NroMesa;
                Mesa_exis.Capacidad = data.Capacidad;// is null ? Mesa_exis.Capasidad : data.Capasidad;
                Mesa_exis.FecModifica = DateTime.Now;
                Mesa_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                Mesa_exis.UsuModifica = data.UsuModifica is null ? Mesa_exis.UsuModifica : data.UsuModifica;
                Mesa_exis.Estado = data.Estado is null ? Mesa_exis.Estado : data.Estado;
                Mesa_exis.Accion = data.Accion is null ? Mesa_exis.Accion : data.Accion;

                _dbcontext.Mesas.Update(Mesa_exis);
                _dbcontext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new { Message = Mesa_exis });
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
                var Mesa_exis = _dbcontext.Mesas.Find(id);
                if (Mesa_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error no existe la Mesa" });
                }
                _dbcontext.Mesas.Remove(Mesa_exis);
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
                var Mesa_exis = _dbcontext.Mesas.Find(id);
                if (Mesa_exis==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Error no existe la Mesa" });
                }
                return StatusCode(StatusCodes.Status200OK, new { Message = Mesa_exis });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("buscarAll")]
        public async Task<IActionResult> buscarAll()
        {
            try
            {
                var Mesa_exis = _dbcontext.Mesas.Include(c => c.MesaFunciones);
                return StatusCode(StatusCodes.Status200OK, new { Message = Mesa_exis });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
    }
}
