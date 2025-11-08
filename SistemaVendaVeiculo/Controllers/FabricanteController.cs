using Microsoft.AspNetCore.Mvc;
using SistemaVendaVeiculo.Service;
using SistemaVendaVeiculo.Dtos;
using SistemaVendaVeiculo;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using SistemaVendaVeiculo.Model;


namespace SistemaVendaVeiculo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FabricanteController : ControllerBase
    {
        private readonly FabricanteService _fabricanteService;

        public FabricanteController(FabricanteService fabricanteService)
        {
            _fabricanteService = fabricanteService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarFabricante([FromBody] FabricanteDto dto)
        {
            try
            {
                var fabricante = new Fabricante
                {
                    Nome = dto.Nome,
                    PaisOrigem = dto.PaisOrigem,
                    Fundacao = dto.Fundacao
                };

                await _fabricanteService.CadastrarFabricanteAsync(fabricante);
                return Ok("Fabricante cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterFabricantePorId(int id)
        {
            try
            {
                var fabricante = await _fabricanteService.ObterFabricantePorIdAsync(id);
                return Ok(fabricante);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
                
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarFabricantes()
        {
            try
            {
                var fabricantes = await _fabricanteService.ListarFabricantesAsync();
                var dtos = fabricantes.Select(f => new FabricanteDto
                {
                    Nome = f.Nome,
                    PaisOrigem = f.PaisOrigem,
                    Fundacao = f.Fundacao
                }).ToList();

                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarFabricante(int id, [FromBody] FabricanteDto dto)
        {
            try
            {
                var fabricante = new Fabricante
                {
                    IdFabricante = id,
                    Nome = dto.Nome,
                    PaisOrigem = dto.PaisOrigem,
                    Fundacao = dto.Fundacao
                };

                await _fabricanteService.AtualizarFabricanteAsync(id, fabricante);
                return Ok("Fabricante atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarFabricante(int id)
        {
            try
            {
                await _fabricanteService.DeletarFabricanteAsync(id);
                return Ok("Fabricante deletado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
