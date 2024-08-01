using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto3_Over.DTOs;
using Projeto3_Over.Enums;
using Projeto3_Over.Extensions;
using Projeto3_Over.Models;
using Projeto3_Over.Repositorios.Interfaces;

namespace Projeto3_Over.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        [HttpGet("all")]
        public async Task<ActionResult<List<UsuarioDto>>> GetAllUsers()
        {
            List<UsuarioModel> usuarios = await _usuarioRepositorio.GetAllUsers();

            return Ok(usuarios);
        }
        [HttpGet("front")]
        public async Task<ActionResult<List<UsuarioDto>>> GetUsersFront()
        {
            List<UsuarioModel> usuarios = await _usuarioRepositorio.GetUsersFront();

            List<UsuarioDto> usuariosDTO = usuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nome = u.Nome,
                UserName = u.UserName,
                Cpf = u.CPF,
                Status = u.Status.GetDescription(),
                EmpresaId = u.EmpresaId,
                Empresa = u.Empresa != null ? new EmpresaDto
                {
                    Id = u.Empresa.Id,
                    NomeFantasia = u.Empresa.NomeFantasia,
                    CNPJ = u.Empresa.CNPJ,
                    Cidade = u.Empresa.Cidade,
                    Telefone = u.Empresa.Telefone,
                    Capital = u.Empresa.Capital,
                    Status = u.Empresa.Status.GetDescription()
                } : null
            }).ToList();

            return Ok(usuariosDTO);

        }
        [HttpGet("id")]
        public async Task<ActionResult<List<UsuarioModel>>> GetUserById(int id)
        {
            UsuarioModel usuario = await _usuarioRepositorio.GetUserById(id);
            return Ok(usuario);
        }
        [HttpGet("cpf")]
        public async Task<ActionResult<List<UsuarioModel>>> GetUserByCpf(string cpf)
        {
            UsuarioModel usuario = await _usuarioRepositorio.GetUserByCPF(cpf);
            return Ok(usuario);
        }

        [HttpGet("name")]
        public async Task<ActionResult<List<UsuarioModel>>> GetUserByName(string name)
        {
            UsuarioModel usuario = await _usuarioRepositorio.GetUserByName(name);
            return Ok(usuario);
        }
        [HttpGet("status")]
        public async Task<ActionResult<List<UsuarioModel>>> GetUserByStatus(StatusAtual status)
        {
            UsuarioModel usuario = await _usuarioRepositorio.GetUserByStatus(status);
            return Ok(usuario);
        }
        [HttpGet("empresa")]
        public async Task<ActionResult<List<UsuarioModel>>> GetUsersByEmpresa(string empresa)
        {
            UsuarioModel usuario = await _usuarioRepositorio.GetUsersByEmpresa(empresa);
            return Ok(usuario);
        }
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Adicionar([FromBody] UsuarioModel usuario)
        {
            UsuarioModel usuarioAdicionado = await _usuarioRepositorio.Adicionar(usuario);
            return Ok(usuarioAdicionado);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> Atualizar([FromBody] UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioAtualizado = await _usuarioRepositorio.Atualizar(usuario, id);
            return Ok(usuarioAtualizado);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool usuarioApagado = await _usuarioRepositorio.Apagar(id);
            return Ok(usuarioApagado);
        }
    }
}
