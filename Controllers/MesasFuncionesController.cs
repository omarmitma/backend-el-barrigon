using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Proyecto__Final.hubs;
using Proyecto__Final.Tablas;
using System.Xml.Linq;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesasFuncionesController : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        private readonly IHubContext<llamarMesero> _hubContext;
        public MesasFuncionesController(SistemaRestaurante _context, IHubContext<llamarMesero> hubContext)
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
        public async Task<IActionResult> registrar([FromBody] MesaFuncione data)
        {
            try
            {
                var Mesa_exis = _dbcontext.MesaFunciones.Where(e => e.IdFuncion == data.IdFuncion && e.IdMesa == data.IdMesa).ToList();

                var mesa = _dbcontext.Mesas.Where(e => e.IdMesa == data.IdMesa).FirstOrDefault();
                if (mesa != null)
                {
                    if (data.IdFuncion == 1) mesa.EstadoMesa = 3;
                    if (data.IdFuncion == 2) mesa.EstadoMesa = 2;
                    if (data.IdFuncion == 3) mesa.EstadoMesa = 2;
                    if (data.IdFuncion == 4) mesa.EstadoMesa = 2;
                    if (data.IdFuncion == 5) mesa.EstadoMesa = 3;
                    if (data.IdFuncion == 6) mesa.EstadoMesa = 2;
                    _dbcontext.Mesas.Update(mesa);
                }

                if (Mesa_exis.Count == 0)
                {
                    await _dbcontext.MesaFunciones.AddAsync(data);
                }
                else
                {
                    Mesa_exis[0].Descripcion = data.Descripcion;
                    Mesa_exis[0].IdFuncion = data.IdFuncion;
                    Mesa_exis[0].IdMesa = data.IdMesa;
                    Mesa_exis[0].HorSolicitada = data.HorSolicitada;

                    _dbcontext.MesaFunciones.Update(Mesa_exis[0]);
                }

                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { Message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
        [HttpPut]
        [Route("actualizar")]
        public async Task<IActionResult> actualizar([FromBody] MesaFuncione data)
        {
            try
            {
                var Mesa_exis = _dbcontext.MesaFunciones.Find(data.IdMesa);
                if (Mesa_exis == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error no existe la funcion de Mesa" });
                }
                Mesa_exis.Descripcion = data.Descripcion;//is null ? Mesa_exis.NroMesa : data.NroMesa;
                Mesa_exis.HorSolicitada = data.HorSolicitada;// is null ? Mesa_exis.Capasidad : data.Capasidad;
                Mesa_exis.FecModifica = DateTime.Now;
                Mesa_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                Mesa_exis.UsuModifica = data.UsuModifica is null ? Mesa_exis.UsuModifica : data.UsuModifica;
                Mesa_exis.Estado = data.Estado is null ? Mesa_exis.Estado : data.Estado;
                Mesa_exis.Accion = data.Accion is null ? Mesa_exis.Accion : data.Accion;

                _dbcontext.MesaFunciones.Update(Mesa_exis);
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
                var Mesa_exis = _dbcontext.MesaFunciones.Find(id);
                if (Mesa_exis == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error no existe la funcion de Mesa" });
                }
                _dbcontext.MesaFunciones.Remove(Mesa_exis);
                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { Message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpDelete]
        [Route("eliminar_ByMesa")]
        public async Task<IActionResult> eliminar_ByMesa(int idMesa)
        {
            try
            {
                List<MesaFuncione> dataByCabecera = await _dbcontext.MesaFunciones.Where(s => s.IdMesa == idMesa).ToListAsync();

                var mesa = _dbcontext.Mesas.Where(e => e.IdMesa == idMesa).FirstOrDefault();
                if (mesa != null)
                {
                    mesa.EstadoMesa = 1;
                    _dbcontext.Mesas.Update(mesa);
                }

                //Eliminar registros en base al ID de la cabecera
                _dbcontext.MesaFunciones.RemoveRange(dataByCabecera);
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
                var Mesa_exis = _dbcontext.MesaFunciones.Find(id);
                if (Mesa_exis == null)
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
        [Route("buscar_ByMesa")]
        public async Task<IActionResult> buscar_ByMesa(int idMesa)
        {
            try
            {
                List<MesaFuncione> Mesa_exis = await _dbcontext.MesaFunciones.Where(d => d.IdMesa == idMesa).ToListAsync();
                return StatusCode(StatusCodes.Status200OK, new { Message = Mesa_exis });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
    }
}
