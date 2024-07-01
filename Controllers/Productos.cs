using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto__Final.EntidadProcedures;
using Proyecto__Final.Tablas;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Productos : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public Productos(SistemaRestaurante _context)
        { _dbcontext = _context; }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registro([FromBody] Producto data)
        {
            try
            {
                var producto_exis = _dbcontext.Productos.Where(e => e.Nombre == data.Nombre).ToList();
                if (producto_exis.Count>0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Producto ya existe" });
                }
                await _dbcontext.Productos.AddAsync(data);
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
        public async Task<IActionResult> actualizar([FromBody] Producto data)
        {
            try
            {
                var producto_exis = _dbcontext.Productos.Find(data.IdProducto);
                if (producto_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error no existe el Producto" });
                }
                //producto_exis.Nombre = data.Nombre is null ? producto_exis.Nombre : data.Nombre;
                producto_exis.Precio = data.Precio is null ? producto_exis.Precio : data.Precio;
                producto_exis.IdLinea = data.IdLinea;//is null ? producto_exis.IdLinea : data.IdLinea;
                producto_exis.IdCategoria = data.IdCategoria;//is null ? producto_exis.IdCategoria : data.IdCategoria;
                producto_exis.Nombre = data.Nombre;
                producto_exis.IdUnidad = data.IdUnidad;
                producto_exis.UrlImagen = data.UrlImagen;
                producto_exis.Observacion = data.Observacion;
                //producto_exis.FecRegistro = DateTime.Now;
                //producto_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                //producto_exis.UsuRegistro = data.UsuRegistro is null ? producto_exis.UsuRegistro : data.UsuRegistro;
                producto_exis.FecModifica = DateTime.Now;
                producto_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                producto_exis.UsuModifica = data.UsuModifica is null ? producto_exis.UsuModifica : data.UsuModifica;
                producto_exis.Estado = data.Estado is null ? producto_exis.Estado : data.Estado;
                producto_exis.Accion = data.Accion is null ? producto_exis.Accion : data.Accion;

                _dbcontext.Productos.Update(producto_exis);
                await _dbcontext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new { Message = producto_exis });
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
                var producto_exis = _dbcontext.Productos.Find(id);
                if (producto_exis==null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error no existe el Producto" });
                }
                _dbcontext.Productos.Remove(producto_exis);
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
                var producto_exis = _dbcontext.Productos.Find(id);
                if (producto_exis==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Error no existe el Productos" });
                }
                return StatusCode(StatusCodes.Status200OK, new { Message = producto_exis });
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
                var lista = await _dbcontext.Set<ProductoInner>().FromSqlInterpolated($"EXEC usp_search_all_producto").ToListAsync();


                return StatusCode(StatusCodes.Status200OK, new { Message = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("buscarAll_Inner_by_id")]
        public async Task<IActionResult> buscarAll_Inner_by_id(int idProducto)
        {
            try
            {
                var lista = await _dbcontext.Set<ProductoInner>().FromSqlInterpolated($"EXEC usp_search_all_producto_by_id {idProducto}").ToListAsync();


                return StatusCode(StatusCodes.Status200OK, new { Message = lista.FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }
    }

}
