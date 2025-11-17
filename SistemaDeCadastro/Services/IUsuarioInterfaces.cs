using SistemaDeCadastro.Dto;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Services
{
    public interface IUsuarioInterfaces
    {
        Task<ResponseModel<List<UsuarioDto>>> BuscarUsuarios();
        Task<ResponseModel<UsuarioDto>> BuscarPorId(int id );
        Task<ResponseModel<List<UsuarioDto>>> CriaUsuario(UsuarioCriarDto usuarioCriarDto);
        Task<ResponseModel<List<UsuarioDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto);
        Task<ResponseModel<List<UsuarioDto>>> RemoverUsuario(int usuarioId);

    }
}
