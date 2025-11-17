namespace SistemaDeCadastro.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public double Salario { get; set; }
        public string CPF { get; set; } = string.Empty;
        public bool Situacao { get; set; }
        public string Senha { get; set; } = string.Empty;
    }
}
