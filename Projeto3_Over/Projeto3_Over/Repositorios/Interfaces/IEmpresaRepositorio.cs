using Projeto3_Over.Enums;
using Projeto3_Over.Models;

namespace Projeto3_Over.Repositorios.Interfaces
{
    public interface IEmpresaRepositorio
    {
        Task<List<EmpresaModel>> GetAllEmpresas();
        Task<List<EmpresaModel>> GetEmpresasFront();
        Task<EmpresaModel>? GetEmpresaById(int id);
        Task<List<EmpresaModel>> GetEmpresasByStatus(StatusAtual status);
        Task<EmpresaModel> GetEmpresaByCnpj(string cnpj);
        Task<List<EmpresaModel>> GetEmpresasByEstado(string estado);
        Task<EmpresaModel> GetEmpresaByPhone(string phone);
        Task<EmpresaModel> Adicionar(EmpresaModel empresa);
        Task<EmpresaModel> Atualizar(EmpresaModel empresa, int id);
        Task<bool> AtualizarStatusUsuariosEmpresa(int id, StatusAtual novoStatus);
        Task<bool> Apagar(int id);
    }
}
