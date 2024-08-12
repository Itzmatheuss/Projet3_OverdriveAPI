using Microsoft.EntityFrameworkCore;
using Projeto3_Over.Data;
using Projeto3_Over.Enums;
using Projeto3_Over.Models;
using Projeto3_Over.Repositorios.Interfaces;

namespace Projeto3_Over.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly Projeto3DBContext _dbContext;
        public UsuarioRepositorio(Projeto3DBContext projeto3DBContext)
        {
            _dbContext = projeto3DBContext;
        }

        public async Task<List<UsuarioModel>> GetAllUsers()
        {
            return await _dbContext.Usuarios
                .Include(u => u.Empresa)
                .ToListAsync();
        }
        public async Task<List<UsuarioModel>> GetUsersFront()
        {
            return await _dbContext.Usuarios
                .Include(u => u.Empresa)
                .ToListAsync();
        }

        public async Task<UsuarioModel> GetUserById(int id)
        {
            return await _dbContext.Usuarios.Include(u => u.Empresa).FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<UsuarioModel> GetUserByCpf(string cpf)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.CPF == cpf);
        }
        public async Task<UsuarioModel> GetUserByPhone(string phone)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Telefone == phone);
        }

        public async Task<UsuarioModel> GetLastAddedUser()
        {
            return await _dbContext.Usuarios.OrderByDescending(u => u.Id).FirstOrDefaultAsync();
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }
        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioId = await GetUserById(id);

            if (usuarioId == null)
            {
                throw new Exception("Usuário não encontrado");
            }


            usuarioId.Nome = usuario.Nome;
            usuarioId.UserName = usuario.UserName;
            usuarioId.CPF = usuario.CPF;
            usuarioId.Telefone = usuario.Telefone;
            usuarioId.Status = usuario.Status;
            usuarioId.EmpresaId = usuario.EmpresaId;



            _dbContext.Usuarios.Update(usuarioId);
            await _dbContext.SaveChangesAsync();

            return usuarioId;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioId = await GetUserById(id);

            if (usuarioId == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            _dbContext.Usuarios.Remove(usuarioId);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
