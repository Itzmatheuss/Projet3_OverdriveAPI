using Projeto3_Over.Enums;
using Projeto3_Over.Models;

namespace Projeto3_Over.Repositorios.Interfaces
{
    public interface IEmpresaRepositorio
    {
        Task<List<EmpresaModel>> GetAllEmpresas();
        Task<List<EmpresaModel>> GetEmpresasFront();
        Task<EmpresaModel> GetEmpresaById(int id);
        Task<EmpresaModel> GetEmpresaByName(string name);
        Task<EmpresaModel> GetEmpresaByCnpj(string cnpj);
        Task<EmpresaModel> GetEmpresaByStatus(StatusAtual status);
        Task<EmpresaModel> GetEmpresaByCity(string cidade);
        Task<EmpresaModel> Adicionar(EmpresaModel empresa);
        Task<EmpresaModel> Atualizar(EmpresaModel empresa, int id);
        Task<bool> Apagar(int id);
    }
}
