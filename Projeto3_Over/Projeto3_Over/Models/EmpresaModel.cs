using Projeto3_Over.Enums;
using System.ComponentModel;

namespace Projeto3_Over.Models
{
    public class EmpresaModel
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string NomeFantasia { get; set; }
        public required string CNPJ { get; set; }
        public DateTimeOffset? DataCadastro { get; set; }
        public required string Cnae { get; set; }
        public required string NaturezaJuridica { get; set; }
        public required string Cep { get; set; }
        public required string Cidade { get; set; }
        public required string Rua { get; set; }
        public required string Bairro { get; set; }
        public required string Numero { get; set; }
        public required string Estado { get; set; }
        public string? Complemento { get; set; }
        public required string Telefone { get; set; }
        public required decimal Capital { get; set; }
        public required StatusAtual Status { get; set; }

    }
}
