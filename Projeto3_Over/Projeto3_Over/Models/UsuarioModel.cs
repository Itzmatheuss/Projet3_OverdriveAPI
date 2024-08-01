using Projeto3_Over.Enums;
using System.ComponentModel;

namespace Projeto3_Over.Models
{

    public class UsuarioModel
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string UserName { get; set; }
        public required string CPF { get; set; }
        public required string Telefone { get; set; }
        public required StatusAtual Status { get; set; }
        public int? EmpresaId { get; set; }
        public EmpresaModel? Empresa { get; set; }   
    }
}
