using AutoMapper;
using SistemaDeCadastro.Dto;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<Usuario, UsuarioDto>();
        }
    }
}
