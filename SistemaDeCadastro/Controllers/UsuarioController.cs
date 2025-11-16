using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Services;

namespace SistemaDeCadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterfaces _usuarioInterfaces1;
        public UsuarioController(IUsuarioInterfaces usuarioInterfaces)
        {
            _usuarioInterfaces1 = usuarioInterfaces;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var usuarios = await _usuarioInterfaces1.BuscarUsuarios();
            if(usuarios.Status == false)
            {
                return NotFound(usuarios);
            }

            return Ok(usuarios);
        }
    }
}
