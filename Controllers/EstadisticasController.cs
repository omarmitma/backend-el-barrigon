using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto__Final.Tablas;
using Proyecto__Final.EntidadProcedures;

namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticasController : ControllerBase
    {
        
        public readonly SistemaRestaurante _dbcontext;
        public EstadisticasController(SistemaRestaurante _context)
        { _dbcontext = _context; }

        [HttpGet]
        [Route("buscar_estadisticas")]
        public async Task<IActionResult> buscar_estadisticas(string fecIni, string fecFin)
        {
            try
            {
                var data = await _dbcontext.Set<FilterEstadisticas>().FromSqlInterpolated($"EXEC usp_filter_estadisticas {fecIni},{fecFin}").ToListAsync();
                data.FirstOrDefault().rankingCategoria = await _dbcontext.Set<RankingCategoria>().FromSqlInterpolated($"EXEC usp_filter_estadisticas_ranking_categoria {fecIni},{fecFin}").ToListAsync();
                data.FirstOrDefault().rankingPlato = await _dbcontext.Set<RankingPlatos>().FromSqlInterpolated($"EXEC usp_filter_estadisticas_ranking_plato {fecIni},{fecFin}").ToListAsync();

                return StatusCode(StatusCodes.Status200OK, new { Message = data.FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

    }
}
