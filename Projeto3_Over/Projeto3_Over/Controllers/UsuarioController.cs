using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto3_Over.DTOs;
using Projeto3_Over.Enums;
using Projeto3_Over.Extensions;
using Projeto3_Over.Models;
using Projeto3_Over.Models.Error;
using Projeto3_Over.Repositorios.Interfaces;

namespace Projeto3_Over.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IValidator<UsuarioModel> _validator;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IValidator<UsuarioModel> validator, IEmpresaRepositorio empresaRepositorio )
        {
            _usuarioRepositorio = usuarioRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _validator = validator;
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

        [HttpGet("ultimo-user")]
        public async Task<ActionResult<UsuarioModel>> GetLastAddedUser()
        {
            UsuarioModel usuario = await _usuarioRepositorio.GetLastAddedUser();
            if (usuario == null)
            {
                return BadRequest("<strong>Usuário</strong> não encontrado");
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Adicionar([FromBody] UsuarioModel usuario)
        {
            var validationResult = _validator.Validate(usuario);
            var verificaCpf = await _usuarioRepositorio.GetUserByCpf(usuario.CPF);

            if (verificaCpf != null)
            {
                return BadRequest("<strong>CPF</strong> já cadastrado");
            }
            
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.ToCustomValidationFailure());
            }
            if(usuario.EmpresaId != null)
            {
               return BadRequest("Usuário não pode ser adicionado a uma empresa");
            }
            if(usuario.Status != StatusAtual.Pendente)
            {
                return BadRequest("Usuário não pode ser adicionado com status diferente de pendente");
            }
            var verificaTelefone = await _usuarioRepositorio.GetUserByPhone(usuario.Telefone);
            if (verificaTelefone != null)
            {
                return BadRequest("<strong>Telefone</strong> já cadastrado");
            }

            

            UsuarioModel usuarioAdicionado = await _usuarioRepositorio.Adicionar(usuario);
            return Ok(usuarioAdicionado);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> Atualizar([FromBody] UsuarioModel usuario, int id)
        {
            // Valida o modelo recebido
            var validationResult = _validator.Validate(usuario);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.ToCustomValidationFailure());
            }

            // Verifica se o CPF e telefone já estão cadastrados
            var verificaCpf = await _usuarioRepositorio.GetUserByCpf(usuario.CPF);
            var verificaTelefone = await _usuarioRepositorio.GetUserByPhone(usuario.Telefone);

            if (verificaCpf != null && verificaCpf.Id != id)
            {
                return BadRequest("<strong>CPF</strong> já cadastrado");
            }

            if (verificaTelefone != null && verificaTelefone.Id != id)
            {
                return BadRequest("<strong>Telefone</strong> já cadastrado");
            }

            // Verifica o status do usuário e a presença de EmpresaId
            if (usuario.Status == StatusAtual.Inativo || usuario.Status == StatusAtual.Pendente)
            {
                // Se o status é Inativo ou Pendente, garante que EmpresaId seja null
                usuario.EmpresaId = null;
            }
            else if (usuario.Status == StatusAtual.Ativo)
            {
                // Se o status é Ativo, verifica se EmpresaId é válido
                if (!usuario.EmpresaId.HasValue)
                {
                    return BadRequest("Empresa não encontrada");
                }

                var empresaExistente = await _empresaRepositorio.GetEmpresaById(usuario.EmpresaId.Value);
                if (empresaExistente == null)
                {
                    return BadRequest("Empresa associada não encontrada.");
                }
                if(empresaExistente.Status != StatusAtual.Ativo)
                {
                    return BadRequest("Empresa associada não está ativa.");
                }
            }
            else
            {
                return BadRequest("Status do usuário inválido.");
            }

            // Atualiza o usuário no banco de dados
            UsuarioModel usuarioAtualizado = await _usuarioRepositorio.Atualizar(usuario, id);

            return Ok(usuarioAtualizado);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {

            bool usuario = await _usuarioRepositorio.Apagar(id);
            if (!usuario)
            {
                return BadRequest("Usuario não encontrado");
            }

            return Ok(true);
        }

    }
}
