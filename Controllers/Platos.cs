//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto__Final.EntidadProcedures;
using Proyecto__Final.Tablas;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Platos : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public Platos(SistemaRestaurante _context)
        { _dbcontext = _context; }
        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registrar([FromBody] Plato data)
        {
            try
            {
                var plato_exis = _dbcontext.Platos.Where(e => e.Nombre == data.Nombre).ToList();
                if (plato_exis.Count>0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Plato ya existe" });
                }
                await _dbcontext.Platos.AddAsync(data);
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
        public async Task<IActionResult> actualizar([FromBody] Plato data)
        {
            try
            {
                var plato_exis = _dbcontext.Platos.Find(data.IdPlato);

                _dbcontext.DetallePlatos.RemoveRange(_dbcontext.DetallePlatos.Where(s => s.IdPlato == data.IdPlato));
                if (plato_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error no existe el Plato" });
                }

                foreach (var deta in data.DetallePlatos) {
                    deta.IdDetallePlato = 0;
                }
                plato_exis.IdLinea = data.IdLinea; //is null ? plato_exis.IdLinea: data.IdLinea;
                plato_exis.IdSubLinea = data.IdSubLinea;//is null ? plato_exis.IdSubLinea : data.IdSubLinea;
                plato_exis.IdCategoria = data.IdCategoria;// is null ? plato_exis.IdCategoria : data.IdCategoria;
                plato_exis.Nombre = data.Nombre is null ? plato_exis.Nombre : data.Nombre;
                plato_exis.Abreviatura = data.Abreviatura is null ? plato_exis.Abreviatura : data.Abreviatura;
                plato_exis.Precio = data.Precio;//is null ? plato_exis.Precio : data.Precio;
                plato_exis.PrecioAdicional = data.PrecioAdicional;//is null ? plato_exis.Precio : data.Precio;
                plato_exis.PrecioOferta = data.PrecioOferta;//is null ? plato_exis.Precio : data.Precio;
                plato_exis.UrlImagen = data.UrlImagen;//is null ? plato_exis.Precio : data.Precio;
                plato_exis.TiempoEspera = data.TiempoEspera;
                plato_exis.Observacion = data.Observacion;
                plato_exis.DetallePlatos = data.DetallePlatos;
                //plato_exis.FecRegistro = DateTime.Now;
                //plato_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                //plato_exis.UsuRegistro = data.UsuRegistro is null ? plato_exis.UsuRegistro : data.UsuRegistro;
                plato_exis.FecModifica = DateTime.Now;
                plato_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                plato_exis.UsuModifica = data.UsuModifica is null ? plato_exis.UsuModifica : data.UsuModifica;
                plato_exis.Estado = data.Estado is null ? plato_exis.Estado :data.Estado;
                plato_exis.Accion =data.Accion is null ? plato_exis.Accion :data.Accion;

                _dbcontext.Platos.Update(plato_exis);
                await _dbcontext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new { Message = plato_exis });
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
                var plato_exis = _dbcontext.Platos.Find(id);
                _dbcontext.DetallePlatos.RemoveRange(_dbcontext.DetallePlatos.Where(s => s.IdPlato == id));
                if (plato_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error no existe el Plato" });
                }
                _dbcontext.Platos.Remove(plato_exis);
                await _dbcontext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, new { Message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("buscar")]
        public async Task<IActionResult> buscar(int id)
        {
            try
            {
                var plato_exis = _dbcontext.Platos.Find(id);
                if (plato_exis==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Error no existe el Pedido" });
                }
                return StatusCode(StatusCodes.Status200OK, new { Message = plato_exis });
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
                var plato_exis = _dbcontext.Platos.Include(c => c.DetallePlatos);
                return StatusCode(StatusCodes.Status200OK, new { Message = plato_exis });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("buscarAll_Inner")]
        public async Task<IActionResult> buscarAll_Inner()
        {
            try
            {
                var lista = await _dbcontext.Set<PlatosInner>().FromSqlInterpolated($"EXEC usp_search_all_plato").ToListAsync();

                var listaDetalle = await _dbcontext.Set<PlatosDetalleInner>().FromSqlInterpolated($"EXEC usp_search_all_plato_detalle").ToListAsync();

                lista.ForEach(cab =>
                {
                    var listaDetalleNow = listaDetalle.Where(d => d.IdPlato == cab.IdPlato).ToList();
                    if(listaDetalleNow.Count() > 0)
                    {
                        cab.DetallePlatos = listaDetalleNow;
                    }
                });

                return StatusCode(StatusCodes.Status200OK, new { Message = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("buscarAll_Inner_by_id")]
        public async Task<IActionResult> buscarAll_Inner_by_id(int idPlato)
        {
            try
            {
                var lista = await _dbcontext.Set<PlatosInner>().FromSqlInterpolated($"EXEC usp_search_all_plato_by_id {idPlato}").ToListAsync();

                var platoById = lista.FirstOrDefault();

                var listaDetalle = await _dbcontext.Set<PlatosDetalleInner>().FromSqlInterpolated($"EXEC usp_search_all_plato_detalle_by_id {idPlato}").ToListAsync();

                platoById.DetallePlatos = listaDetalle;

                return StatusCode(StatusCodes.Status200OK, new { Message = platoById });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
        /*-----------------------------------Procedures--------------------------------*/
        //[HttpGet]
        //[Route("buscarFiltros")]
        //public async Task<IActionResult> buscarFiltros(int tipoOden = 0, int idCategoria = 0, int precioMenor = 0, int precioMayor = 0)
        //{
        //    try
        //    {
        //        var lista = await _dbcontext.Set<ProPlatos>().FromSqlInterpolated($"EXEC uspUPC_ByID {tipoOden}").ToListAsync();
        //        return StatusCode(StatusCodes.Status200OK, new { Message = detaMaestro_exis });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status400BadRequest, ex);
        //    }
        //}
    }
}
