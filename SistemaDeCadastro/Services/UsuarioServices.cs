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

        public async Task<ResponseModel<UsuarioDto>> BuscarPorId(int usuarioId)
        {
            ResponseModel<UsuarioDto> response = new ResponseModel<UsuarioDto>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.QueryFirstOrDefaultAsync<Usuario>
                    ("select * from Usuarios where id = @Id", new {Id = usuarioId});

                if(usuarioBanco == null)
                {
                    response.Mensagem = "Nenhum Usuario encontrado";
                    response.Status = false;
                    return response;
                }

                var usuarioMapeado = _mapper.Map<UsuarioDto>(usuarioBanco);

                response.Dados = usuarioMapeado;
                response.Mensagem = "Usuario encontrado com sucesso!";


            }

            return response;
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

        public async Task<ResponseModel<List<UsuarioDto>>> CriaUsuario(UsuarioCriarDto usuarioCriarDto)
        {
            ResponseModel<List<UsuarioDto>> response = new ResponseModel<List<UsuarioDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync
                    ("insert into Usuarios (NomeCompleto, Email, Cargo, Salario, Cpf, Situacao, Senha)"
                    + "values(@NomeCompleto, @Email, @Cargo, @Salario, @Cpf, @Situacao, @Senha )", usuarioCriarDto);

                if(usuarioBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro";
                    response.Status = false; 
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);

                var usuariosMapeados = _mapper.Map<List<UsuarioDto>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Lista Atualiazada com sucesso!";


            }

            return response;

        }

        private static async Task<IEnumerable<Usuario>>ListarUsuarios(SqlConnection connection)
        {
            return  await connection.QueryAsync<Usuario>("select * from Usuarios");
        }

        public async Task<ResponseModel<List<UsuarioDto>>> EditarUsuario(UsuarioEditarDto usuarioEditarDto)
        {
            ResponseModel<List<UsuarioDto>> response = new ResponseModel<List<UsuarioDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("update Usuarios set NomeCompleto = @NomeCompleto," +
                                                                                            "Email = @Email," +
                                                                                            "Cargo = @Cargo," +
                                                                                            "Salario = @Salario, " +
                                                                                            "Cpf = @Cpf, " +
                                                                                            "Situacao = @Situacao where Id = @Id", usuarioEditarDto);

                if (usuarioBanco == 0)
                {
                    response.Mensagem = "Usuário não encontrado";
                    response.Status = false;
                    return response;
                }

                // Atualizar lista após edição
                var usuarios = await ListarUsuarios(connection);

                var usuariosMapeados = _mapper.Map<List<UsuarioDto>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuário editado com sucesso!";
            }

            return response;
        }

        public async Task<ResponseModel<List<UsuarioDto>>> RemoverUsuario(int usuarioId)
        {
            ResponseModel<List<UsuarioDto>> response = new ResponseModel<List<UsuarioDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuarioBanco = await connection.ExecuteAsync("delete from Usuarios where id = @Id", new { Id = usuarioId });

                if (usuarioBanco == 0)
                {
                    response.Mensagem = "Usuário não encontrado";
                    response.Status = false;
                    return response;
                }

                // Atualizar lista após edição
                var usuarios = await ListarUsuarios(connection);

                var usuariosMapeados = _mapper.Map<List<UsuarioDto>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuário removido com sucesso!";
            }

            return response;
        }
    }
}
