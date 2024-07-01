using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto__Final.Tablas;
using System.Xml.Linq;


namespace Proyecto__Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        public readonly SistemaRestaurante _dbcontext;

        public Login(SistemaRestaurante _context)
        { _dbcontext = _context; }

        [HttpPost]
        [Route("logeo")]
        public IActionResult logeo([FromBody] UsuarioLogin data)
        {
            try
            {
                var user = _dbcontext.UsuarioLogins.Include(c => c.Empleados).Include(c => c.Clientes).Where(d => d.Usuario == data.Usuario && d.Clave == data.Clave).ToList();
                if (user.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Usuario o clave incorrecta" });
                }

                return StatusCode(StatusCodes.Status200OK, new { message = "Logeado" , user = user.FirstOrDefault() });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }

        }
        //[HttpPost]
        //[Route("registro")]
        //public async Task<IActionResult> registro([FromBody] UsuarioLogin data)
        //{
        //    try
        //    {
        //        var usuario_exis = _dbcontext.Login.Where(e => e.IdUsuarioLogin =data.IdUsuarioLogin).ToList();
        //        if (usuario_exis.Count > 0)
        //        {
        //            return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Usuario ya existe" });
        //        }
        //        await _dbcontext.Empleados.AddAsync(data);
        //        await _dbcontext.SaveChangesAsync();

        //        return StatusCode(StatusCodes.Status200OK, new { Message = data });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status400BadRequest, ex);
        //    }
        //}

    }
}
