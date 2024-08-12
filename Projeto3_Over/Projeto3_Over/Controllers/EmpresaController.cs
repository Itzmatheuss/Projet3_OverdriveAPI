using System.Linq;
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
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IValidator<EmpresaModel> _validator;

        public EmpresaController(IEmpresaRepositorio empresaRepositorio, IValidator<EmpresaModel> validator)
        {
            _empresaRepositorio = empresaRepositorio;
            _validator = validator;
        }
        [HttpGet("all")]

        public async Task<ActionResult<List<EmpresaModel>>> GetAllEmpresas()
        {
            List<EmpresaModel> empresas = await _empresaRepositorio.GetAllEmpresas();

            return Ok(empresas);
        }
        [HttpGet("front")]
        public async Task<ActionResult<List<EmpresaDto>>> GetEmpresasFront()
        {
            List<EmpresaModel> empresas = await _empresaRepositorio.GetEmpresasFront();

            List<EmpresaDto> empresasDTO = empresas.Select(e => new EmpresaDto
            {
                Id = e.Id,
                NomeFantasia = e.NomeFantasia,
                CNPJ = e.CNPJ,
                Cidade = e.Cidade,
                Telefone = e.Telefone,
                Capital = e.Capital,
                Status = e.Status.GetDescription()
            }).ToList();

            return Ok(empresasDTO);

        }

        [HttpGet("id")]
        public async Task<ActionResult<List<EmpresaModel>>> GetEmpresaById(int id)
        {
            EmpresaModel empresa = await _empresaRepositorio.GetEmpresaById(id);
            return Ok(empresa);
        }


        [HttpGet("status")]
        public async Task<ActionResult<List<EmpresaModel>>> GetEmpresaByStatus(StatusAtual status)
        {
            List<EmpresaModel> empresas = await _empresaRepositorio.GetEmpresasByStatus(status);
            return Ok(empresas);
        }

        [HttpPost]
        public async Task<ActionResult<EmpresaModel>> Adicionar([FromBody] EmpresaModel empresa)
        {
            // Validação do modelo usando FluentValidation
            var validationResult = _validator.Validate(empresa);
            
            // Validações gerais do modelo
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.ToCustomValidationFailure());
            }


            // Verifica se o CNPJ já está cadastrado
            var verificaCnpj = await _empresaRepositorio.GetEmpresaByCnpj(empresa.CNPJ);
            if (verificaCnpj != null)
            {
                return BadRequest("<strong>CNPJ</strong> já cadastrado");
            }
            // Verifica se o telefone já está cadastrado
            var verificaTelefone = await _empresaRepositorio.GetEmpresaByPhone(empresa.Telefone);
            if (verificaTelefone != null)
            {
                return BadRequest("<strong>Telefone</strong> já cadastrado");
            }
            
            if(!EstadosValidos.Lista.Contains(empresa.Estado))
            {
                return BadRequest("<strong>Estado</strong> inválido");
            }

            // Busca empresas no mesmo estado para verificar nomes duplicados
           var empresaNoEstado = await _empresaRepositorio.GetEmpresasByEstado(empresa.Estado);
            if (empresaNoEstado.Any(e => e.Nome == empresa.Nome))
            {
                return BadRequest("Esse nome já existe no seu estado.");
            }

            
            // Adiciona a nova empresa
            EmpresaModel empresaAdicionada = await _empresaRepositorio.Adicionar(empresa);
            return Ok(empresaAdicionada);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmpresaModel>> Atualizar([FromBody] EmpresaModel empresa, int id)
        {
            // Validação do modelo usando FluentValidation
            var validationResult = _validator.Validate(empresa);
            // Validações gerais do modelo
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.ToCustomValidationFailure());
            }
            // Verifica se o CNPJ já está cadastrado
            var verificaCnpj = await _empresaRepositorio.GetEmpresaByCnpj(empresa.CNPJ);
            if (verificaCnpj != null && verificaCnpj.Id != id)
            {
                return BadRequest("<strong>CNPJ</strong> já cadastrado");
            }
            // Verifica se o telefone já está cadastrado
            var verificaTelefone = await _empresaRepositorio.GetEmpresaByPhone(empresa.Telefone);

            if (verificaTelefone != null && verificaTelefone.Id != id)
            {
                return BadRequest("<strong>Telefone</strong> já cadastrado");
            }
           
            if(!EstadosValidos.Lista.Contains(empresa.Estado))
            {
                return BadRequest("<strong>Estado</strong> inválido");
            }

            // Busca empresas no mesmo estado para verificar nomes duplicados
            var empresaNoEstado = await _empresaRepositorio.GetEmpresasByEstado(empresa.Estado);
            if (empresaNoEstado.Any(e => e.Nome == empresa.Nome && e.Id != id))
            {
                return BadRequest("Esse nome já existe no seu estado.");
            }

            

            EmpresaModel empresaAtualizada = await _empresaRepositorio.Atualizar(empresa, id);
            return Ok(empresaAtualizada);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {

            await _empresaRepositorio.AtualizarStatusUsuariosEmpresa(id, StatusAtual.Pendente);


            bool empresaApagada = await _empresaRepositorio.Apagar(id);
            if (!empresaApagada)
            {
                return BadRequest("Empresa não encontrada");
            }
            return Ok(empresaApagada);
        }

    }
}
