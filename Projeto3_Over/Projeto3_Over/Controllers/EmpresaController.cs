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
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;

        public EmpresaController(IEmpresaRepositorio empresaRepositorio)
        {
            _empresaRepositorio = empresaRepositorio;
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
        [HttpGet("name")]
        public async Task<ActionResult<List<EmpresaModel>>> GetEmpresaByName(string name)
        {
            EmpresaModel empresa = await _empresaRepositorio.GetEmpresaByName(name);
            return Ok(empresa);
        }

        [HttpGet("cnpj")]
        public async Task<ActionResult<List<EmpresaModel>>> GetEmpresaByCnpj(string cnpj)
        {
            EmpresaModel empresa = await _empresaRepositorio.GetEmpresaByCnpj(cnpj);
            return Ok(empresa);
        }

        [HttpGet("status")]
        public async Task<ActionResult<List<EmpresaModel>>> GetEmpresaByStatus(StatusAtual status)
        {
            EmpresaModel empresa = await _empresaRepositorio.GetEmpresaByStatus(status);
            return Ok(empresa);
        }

        [HttpGet("cidade")]
        public async Task<ActionResult<List<EmpresaModel>>> GetEmpresaByCity(string cidade)
        {
            EmpresaModel empresa = await _empresaRepositorio.GetEmpresaByCity(cidade);
            return Ok(empresa);
        }
        [HttpPost]
        public async Task<ActionResult<EmpresaModel>> Adicionar([FromBody] EmpresaModel empresa)
        {
            EmpresaModel empresaAdicionada = await _empresaRepositorio.Adicionar(empresa);
            return Ok(empresaAdicionada);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<EmpresaModel>> Atualizar([FromBody] EmpresaModel empresa, int id)
        {
            EmpresaModel empresaAtualizada = await _empresaRepositorio.Atualizar(empresa, id);
            return Ok(empresaAtualizada);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool empresaApagada = await _empresaRepositorio.Apagar(id);
            return Ok(empresaApagada);
        }

    }
}
