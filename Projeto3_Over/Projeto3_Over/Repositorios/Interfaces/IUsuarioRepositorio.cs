using Projeto3_Over.Enums;
using Projeto3_Over.Models;

namespace Projeto3_Over.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<List<UsuarioModel>> GetAllUsers();
        Task<List<UsuarioModel>> GetUsersFront();
        Task<UsuarioModel> GetUserById(int id);
        Task<UsuarioModel> GetUserByCpf(string cpf);
        Task<UsuarioModel> GetUserByPhone(string phone);
        Task<UsuarioModel> GetLastAddedUser();
        Task<UsuarioModel> Adicionar(UsuarioModel usuario);
        Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id);
        Task<bool> Apagar(int id);
    }
}
