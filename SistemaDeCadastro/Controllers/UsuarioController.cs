using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Dto;
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
            if (usuarios.Status == false)
            {
                return NotFound(usuarios);
            }

            return Ok(usuarios);
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> BuscaPorId(int usuarioId)
        {
            var usuarios = await _usuarioInterfaces1.BuscarPorId(usuarioId);

            if (usuarios.Status == false)
            {
                return NotFound(usuarios);
            }
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            var usuarios = await _usuarioInterfaces1.CriaUsuario(usuarioCriarDto);
            if(usuarios.Status == false)
            {
                return NotFound(usuarios);
            }
            return Ok(usuarios);
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            var usuario = await _usuarioInterfaces1.EditarUsuario(usuarioEditarDto);

            if(usuario.Status == false ) return BadRequest(usuario);

            return Ok(usuario);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoverUsuario(int usuarioId)
        {
            var usuario =await _usuarioInterfaces1.RemoverUsuario(usuarioId);

            if(usuario.Status == false ) return NotFound(usuario);  

            return Ok(usuario);
        }

        
    }
}
