using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Proyecto__Final.Tablas;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ventas : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public Ventas(SistemaRestaurante _context)
        {  _dbcontext = _context; }
        
        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registro([FromBody] Venta data)
        {
            try
            {
                await _dbcontext.Ventas.AddAsync(data);
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
        public async Task<IActionResult> actualizar([FromBody] Venta data)
        {
            try
            {
                var venta_exis = _dbcontext.Ventas.Find(data.IdVentas);
                if (venta_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error No existe la Venta" });
                }
                venta_exis.IdEmpleado = data.IdEmpleado;// is null ? venta_exis.IdEmpleado : data.IdEmpleado;
                venta_exis.IdCliente = data.IdCliente;//is null ? venta_exis.IdCliente: data.IdCliente;
                //venta_exis.FecRegistro = DateTime.Now;
                //venta_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                //venta_exis.UsuRegistro = data.UsuRegistro is null ? venta_exis.UsuRegistro : data.UsuRegistro;
                venta_exis.FecModifica = DateTime.Now;
                venta_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                venta_exis.UsuModifica = data.UsuModifica is null ? venta_exis.UsuModifica : data.UsuModifica;
                venta_exis.Estado = data.Estado is null ? venta_exis.Estado : data.Estado;
                venta_exis.Accion = data.Accion is null ? venta_exis.Accion : data.Accion;

                _dbcontext.Ventas.Update(venta_exis);
                await _dbcontext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new { Message = venta_exis });
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
                var venta_exis = _dbcontext.Ventas.Find(id);
                if (venta_exis == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error No existe la Venta" });
                }
                _dbcontext.Ventas.Remove(venta_exis);
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
                var venta_exis = _dbcontext.Ventas.Find(id);
                if (venta_exis == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Error No existe la Venta" });
                }
                return StatusCode(StatusCodes.Status200OK, new { Message = venta_exis });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
    }
}
