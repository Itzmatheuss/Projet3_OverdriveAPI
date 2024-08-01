using Projeto3_Over.Enums;
using Projeto3_Over.Models;

namespace Projeto3_Over.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<List<UsuarioModel>> GetAllUsers();
        Task<List<UsuarioModel>> GetUsersFront();
        Task<UsuarioModel> GetUserById(int id);
        Task<UsuarioModel> GetUserByName(string name);
        Task<UsuarioModel> GetUserByCPF(string cpf);
        Task<UsuarioModel> GetUsersByEmpresa(string empresa);
        Task<UsuarioModel> GetUserByStatus(StatusAtual status);
        Task<UsuarioModel> Adicionar(UsuarioModel usuario);
        Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id);
        Task<bool> Apagar(int id);
    }
}
