using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto__Final.Tablas;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleMaestros : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;
        public DetalleMaestros(SistemaRestaurante _context)
        { _dbcontext = _context; }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> registro([FromBody] DetalleMaestro data)
        {
            try
            {
                var detaMaestro_exis = _dbcontext.DetalleMaestros.Where(e => e.Nombre == data.Nombre).ToList();
                if (detaMaestro_exis.Count>0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "detalle maestro ya existe" });
                }
                await _dbcontext.DetalleMaestros.AddAsync(data);
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
        public async Task<IActionResult> actualizar([FromBody] DetalleMaestro data)
        {
            try
            {
                var detaMaestro_exis = _dbcontext.DetalleMaestros.Find(data.IdDetalleMaestro);
                if (detaMaestro_exis == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error no Existe Id" });
                }
                detaMaestro_exis.IdCabezeraMaestro = data.IdCabezeraMaestro;// is null ? detaMaestro_exis.IdCabezeraMaestro : data.IdDetalleMaestro;
                detaMaestro_exis.Nombre = data.Nombre is null ? detaMaestro_exis.Nombre : data.Nombre;
                detaMaestro_exis.Abreviatura = data.Abreviatura is null ? detaMaestro_exis.Abreviatura : data.Abreviatura;
                detaMaestro_exis.Campo1Nvarchar = data.Campo1Nvarchar is null ? detaMaestro_exis.Campo1Nvarchar : data.Campo1Nvarchar;
                detaMaestro_exis.Campo2Nvarchar = data.Campo2Nvarchar is null ? detaMaestro_exis.Campo2Nvarchar : data.Campo2Nvarchar;
                detaMaestro_exis.Campo3Nvarchar = data.Campo3Nvarchar is null ? detaMaestro_exis.Campo3Nvarchar : data.Campo3Nvarchar;
                detaMaestro_exis.Campo1Int = data.Campo1Int is null ? detaMaestro_exis.Campo1Int : data.Campo1Int;
                detaMaestro_exis.Campo2Int = data.Campo2Int is null ? detaMaestro_exis.Campo2Int : data.Campo2Int;
                detaMaestro_exis.Campo3Int = data.Campo3Int is null ? detaMaestro_exis.Campo3Int : data.Campo3Int;
                detaMaestro_exis.Campo1Decimal = data.Campo1Decimal is null ? detaMaestro_exis.Campo1Decimal : data.Campo1Decimal;
                detaMaestro_exis.Campo2Decimal = data.Campo2Decimal is null ? detaMaestro_exis.Campo2Decimal : data.Campo2Decimal;
                detaMaestro_exis.Campo3Decimal = data.Campo3Decimal is null ? detaMaestro_exis.Campo3Decimal : data.Campo3Decimal;
                //detaMaestro_exis.FecRegistro = DateTime.Now;
                //detaMaestro_exis.HorRegistro = DateTime.Now.ToString("hh:mm:ss");
                //detaMaestro_exis.UsuRegistro = data.UsuRegistro is null ? detaMaestro_exis.UsuRegistro : data.UsuRegistro;
                detaMaestro_exis.FecModifica = DateTime.Now;
                detaMaestro_exis.HorModifica = DateTime.Now.ToString("hh:mm:ss");
                detaMaestro_exis.UsuModifica = data.UsuModifica is null ? detaMaestro_exis.UsuModifica : data.UsuModifica;
                detaMaestro_exis.Estado = data.Estado is null ? detaMaestro_exis.Estado : data.Estado;
                detaMaestro_exis.Accion = data.Accion is null ? detaMaestro_exis.Accion : data.Accion;

                _dbcontext.DetalleMaestros.Update(detaMaestro_exis);
                await _dbcontext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, new { Message = detaMaestro_exis });
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
                var detaMaestro_exis = _dbcontext.DetalleMaestros.Find(id);
                if (detaMaestro_exis == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Error, No existe Id" });
                }
                _dbcontext.DetalleMaestros.Remove(detaMaestro_exis);
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
                var detaMaestro_exis = _dbcontext.DetalleMaestros.Find(id);
                if (detaMaestro_exis==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Messsage = "Error no existe el Id" });
                }
                return StatusCode(StatusCodes.Status200OK, new { Message = detaMaestro_exis });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        ///*-----------------------------------Procedures--------------------------------*/
        //[HttpGet]
        //[Route("buscarFiltros")]
        //public async Task<IActionResult> buscarFiltros(int tipoOden = 0, int idCategoria = 0, int precioMenor = 0, int precioMayor= 0)
        //{
        //    try
        //    {
        //        var lista = await _dbcontext.Set<ProdUPCLista>().FromSqlInterpolated($"EXEC uspUPC_ByID {Convert.ToInt32(IdUPC)}").ToListAsync();
        //        return StatusCode(StatusCodes.Status200OK, new { Message = detaMaestro_exis });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status400BadRequest, ex);
        //    }
        //}


    }
}
