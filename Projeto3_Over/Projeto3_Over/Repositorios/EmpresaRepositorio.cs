using Microsoft.EntityFrameworkCore;
using Projeto3_Over.Data;
using Projeto3_Over.Enums;
using Projeto3_Over.Models;
using Projeto3_Over.Models.Error;
using Projeto3_Over.Repositorios.Interfaces;

namespace Projeto3_Over.Repositorios
{
    public class EmpresaRepositorio : IEmpresaRepositorio
    {
        private readonly Projeto3DBContext _dbContext;
        public EmpresaRepositorio(Projeto3DBContext projeto3DBContext)
        {
            _dbContext = projeto3DBContext;
        }
        public async Task<List<EmpresaModel>> GetAllEmpresas()
        {
            return await _dbContext.Empresas.ToListAsync();
        }
        public async Task<List<EmpresaModel>> GetEmpresasFront()
        {
            return await _dbContext.Empresas.ToListAsync();
        }
        public async Task<EmpresaModel> GetEmpresaById(int id)
        {
            return await _dbContext.Empresas.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<EmpresaModel>> GetEmpresasByStatus(StatusAtual status)
        {
            return await _dbContext.Empresas
                                  .Where(e => e.Status == status)
                                  .ToListAsync();
        }
        public async Task<EmpresaModel> Adicionar(EmpresaModel empresa)
        {
            _dbContext.Empresas.Add(empresa);
            await _dbContext.SaveChangesAsync();

            return empresa;
        }
        public async Task<EmpresaModel> GetEmpresaByCnpj(string cnpj)
        {
            return await _dbContext.Empresas.FirstOrDefaultAsync(e => e.CNPJ == cnpj);
        }

        public async Task<List<EmpresaModel>> GetEmpresasByEstado(string estado)
        {
            return await _dbContext.Empresas.Where(e => e.Estado == estado).ToListAsync();
        }
        public async Task<EmpresaModel> GetEmpresaByPhone(string phone)
        {
            return await _dbContext.Empresas.FirstOrDefaultAsync(e => e.Telefone == phone);
        }

        public async Task<EmpresaModel> Atualizar(EmpresaModel empresa, int id)
        {
            
            EmpresaModel empresaId = await GetEmpresaById(id);

            if (empresaId == null)
            {
                throw new Exception("Empresa não encontrada");
            }

            empresaId.Nome = empresa.Nome;
            empresaId.NomeFantasia = empresa.NomeFantasia;
            empresaId.CNPJ = empresa.CNPJ;
            empresaId.DataCadastro = empresa.DataCadastro;
            empresaId.Cnae = empresa.Cnae;
            empresaId.NaturezaJuridica = empresa.NaturezaJuridica;
            empresaId.Cep = empresa.Cep;
            empresaId.Cidade = empresa.Cidade;
            empresaId.Rua = empresa.Rua;
            empresaId.Bairro = empresa.Bairro;
            empresaId.Numero = empresa.Numero;
            empresaId.Estado = empresa.Estado;
            empresaId.Complemento = empresa.Complemento;
            empresaId.Telefone = empresa.Telefone;
            empresaId.Capital = empresa.Capital;
            empresaId.Status = empresa.Status;
       

            _dbContext.Empresas.Update(empresaId);
            await _dbContext.SaveChangesAsync();

            return empresaId;
        }
        public async Task<bool> AtualizarStatusUsuariosEmpresa(int id, StatusAtual novoStatus)
        {
            var usuarios = await _dbContext.Usuarios.Where(u => u.EmpresaId == id).ToListAsync();
            foreach (var usuario in usuarios)
            {
                usuario.Status = novoStatus;
            }
            _dbContext.Usuarios.UpdateRange(usuarios);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Apagar(int id)
        {
            EmpresaModel empresaId = await GetEmpresaById(id);

            if (empresaId == null)
            {
                return false;
            }

            var usuarios = await _dbContext.Usuarios.Where(u => u.EmpresaId == id).ToListAsync();
            foreach (var usuario in usuarios)
            {
                usuario.EmpresaId = null;
                _dbContext.Usuarios.Update(usuario);
            }
            await _dbContext.SaveChangesAsync();

            _dbContext.Empresas.Remove(empresaId);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
