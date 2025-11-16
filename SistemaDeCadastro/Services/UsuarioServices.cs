using AutoMapper;
using Dapper;
using SistemaDeCadastro.Dto;
using SistemaDeCadastro.Models;
using System.Data.SqlClient;

namespace SistemaDeCadastro.Services
{
    public class UsuarioServices : IUsuarioInterfaces
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UsuarioServices(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        [Obsolete]
        public async Task<ResponseModel<List<UsuarioDto>>> BuscarUsuarios()
        {

            ResponseModel<List<UsuarioDto>> response = new ResponseModel<List<UsuarioDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.QueryAsync<Usuario>("select * from Usuarios");

                if (usuarioBanco.Count() == 0)
                {
                    response.Mensagem = "Banco vazio!";
                    response.Status = false;
                    return response;

                }


                var usuarioMapeado = _mapper.Map<List<UsuarioDto>>(usuarioBanco);

                response.Dados = usuarioMapeado;
                response.Mensagem = "Usuaros Localizados com sucesso";
            }
              return response;
        }
    }
}
