using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using LoginJWT.Models;
using LoginJWT.Models.DTOS;
using LoginJWT.Custom;

namespace LoginJWT.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly DbPruebaContext _context;
        private readonly Utilities _utilities;
        public AccessController(DbPruebaContext context, Utilities utilities)
        {
            _context = context;
            _utilities = utilities;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario
            {
                Nombre = usuarioDTO.Nombre,
                Correo = usuarioDTO.Correo,
                Clave = _utilities.encriptarSHA256(usuarioDTO.Clave)
            };
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            if (usuario.Idusuario != 0)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            var usuario = 
                await _context.Usuarios.FirstOrDefaultAsync(x => x.Correo == loginDTO.Correo && x.Clave == _utilities.encriptarSHA256(loginDTO.Clave));

            if(usuario == null)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token ="" });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilities.GRJWT(usuario) });
        }
    }
    
}
