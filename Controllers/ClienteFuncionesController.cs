using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto__Final.Tablas;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteFuncionesController : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public ClienteFuncionesController(SistemaRestaurante _context)
        { _dbcontext = _context; }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registrar([FromBody] ClienteFuncione data)
        {
            try
            {

                var Mesa_exis = _dbcontext.ClienteFunciones.Where(e => e.IdFuncion == data.IdFuncion && e.IdCliente == data.IdCliente).ToList();

                if (Mesa_exis.Count == 0)
                {
                    await _dbcontext.ClienteFunciones.AddAsync(data);
                }
                else
                {
                    Mesa_exis[0].Descripcion = data.Descripcion;
                    Mesa_exis[0].IdCliente = data.IdCliente;
                    Mesa_exis[0].HorSolicitada = data.HorSolicitada;

                    _dbcontext.ClienteFunciones.Update(Mesa_exis[0]);
                }

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
        public async Task<IActionResult> actualizar([FromBody] ClienteFuncione data)
        {
            try
            {
                var cliente_exis = _dbcontext.ClienteFunciones.Find(data.IdCliente);
                if (cliente_exis == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "error no existe el Id" });
                }
                cliente_exis.IdFuncion = data.IdFuncion;
                cliente_exis.HorSolicitada = data.HorSolicitada;
                cliente_exis.Descripcion = data.Descripcion;
                cliente_exis.FecModifica = DateTime.Now;
                cliente_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                cliente_exis.UsuModifica = data.UsuModifica is null ? cliente_exis.UsuModifica : data.UsuModifica;
                cliente_exis.Estado = data.Estado is null ? cliente_exis.Estado : data.Estado;
                cliente_exis.Accion = data.Accion is null ? cliente_exis.Accion : data.Accion;

                _dbcontext.ClienteFunciones.Update(cliente_exis);
                await _dbcontext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new { Message = cliente_exis });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("changeMesa")]
        public async Task<IActionResult> changeMesa([FromBody] ClienteFuncione data, int idMesaAnterior)
        {
            try
            {
                var mesa_cliente = _dbcontext.ClienteFunciones.Where(m => m.IdCliente == data.IdCliente && m.IdFuncion == 4).FirstOrDefault();

                var mesa_ahora = _dbcontext.Mesas.Find(data.Campo1Int);
                mesa_ahora.EstadoMesa = 2;
                if (mesa_cliente != null)
                {

                    mesa_cliente.Campo1Int = data.Campo1Int;
                    mesa_cliente.Descripcion = mesa_ahora.Nombre;
                    mesa_cliente.HorSolicitada = DateTime.Now.ToString("hh:mm:ss");

                    _dbcontext.ClienteFunciones.Update(mesa_cliente);

                    var mesa_anterior = _dbcontext.Mesas.Find(idMesaAnterior);
                    var mesa_funciones_anterior = _dbcontext.MesaFunciones.Where(m => m.IdMesa == idMesaAnterior).ToList();

                    mesa_anterior.EstadoMesa = 1;

                    mesa_funciones_anterior.ForEach(funciones =>
                    {
                        funciones.IdMesa = (int)data.Campo1Int;
                        if (funciones.IdFuncion == 4)
                        {
                            funciones.Descripcion = mesa_ahora.Nombre;
                        }
                    });

                    _dbcontext.Mesas.Update(mesa_anterior);
                    _dbcontext.MesaFunciones.UpdateRange(mesa_funciones_anterior);
                }
                else
                {
                    var addMesaFuncione = new MesaFuncione();

                    // let addMesaFunciones:MesaFunciones = new MesaFunciones();
                    addMesaFuncione.IdFuncion = 4;
                    addMesaFuncione.IdMesa = (int)data.Campo1Int;
                    addMesaFuncione.Descripcion = mesa_ahora.Nombre;
                    addMesaFuncione.HorSolicitada = DateTime.Now.ToString("hh:mm:ss");
                    // await this.mesaFuncionesService.registrar(addMesaFunciones).then();

                    _dbcontext.ClienteFunciones.Add(data);
                    _dbcontext.MesaFunciones.Add(addMesaFuncione);
                }

                _dbcontext.Mesas.Update(mesa_ahora);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { Message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> eliminar(int id)
        {
            try
            {
                var cliente_exis = _dbcontext.ClienteFunciones.Find(id);
                if (cliente_exis == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Messsage = "Error no existe el Id" });
                }
                _dbcontext.ClienteFunciones.Remove(cliente_exis);
                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { Message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }

        }

        [HttpDelete]
        [Route("eliminar_ByCliente")]
        public async Task<IActionResult> eliminar_ByCliente(int idCliente)
        {
            try
            {
                List<ClienteFuncione> cliente_exis = await _dbcontext.ClienteFunciones.Where(d => d.IdCliente == idCliente).ToListAsync();

                _dbcontext.ClienteFunciones.RemoveRange(cliente_exis);
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
                var cliente_exis = _dbcontext.ClienteFunciones.Find(id);
                if (cliente_exis == null)
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

        [HttpGet]
        [Route("buscar_ByCliente")]
        public async Task<IActionResult> buscar_ByCliente(int idCliente)
        {
            try
            {
                var cliente_exis = _dbcontext.ClienteFunciones.Where(d => d.IdCliente == idCliente);
                return StatusCode(StatusCodes.Status200OK, new { Message = cliente_exis });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
    }
}
