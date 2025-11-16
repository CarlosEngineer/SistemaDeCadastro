using SistemaDeCadastro.Dto;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Services
{
    public interface IUsuarioInterfaces
    {
        Task<ResponseModel<List<UsuarioDto>>> BuscarUsuarios();
    }
}
