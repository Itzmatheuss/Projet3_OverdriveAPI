namespace Projeto3_Over.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Cpf { get; set; }
        public required string UserName { get; set; }
        public required string Status { get; set; }
        public int? EmpresaId { get; set; }
        public EmpresaDto? Empresa { get; set; }

    }
}
