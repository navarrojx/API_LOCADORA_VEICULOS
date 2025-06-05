using Microsoft.AspNetCore.Mvc;
using SistemaVendaVeiculo.Dtos;
using SistemaVendaVeiculo.Service;
using SistemaVendaVeiculo.Model;
using SistemaVendaVeiculo.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaVendaVeiculo;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

[ApiController]
[Route("api/[controller]")]
public class AluguelController : ControllerBase
{
    private readonly AluguelService _aluguelService;

    public AluguelController(AluguelService aluguelService)
    {
        _aluguelService = aluguelService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarAlugueis()
    {
        var alugueis = await _aluguelService.ListarAlugueisAsync();
        return Ok(alugueis);
    }



    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarAluguelPorIdAsync(int id)
    {
        try
        {
            var aluguel = await _aluguelService.BuscarAluguelPorIdAsync(id);
            return Ok(aluguel);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPost]
    public async Task<IActionResult> CadastrarAluguel([FromBody] AluguelDto dto)
    {
        try
        {
            var aluguel = await _aluguelService.CadastrarAluguelAsync(dto);

            return CreatedAtAction(nameof(ListarAlugueis), new { id = aluguel.IdAluguel }, aluguel);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarAluguel(int id, [FromBody] AluguelDto dto)
    {
        await _aluguelService.AtualizarAluguelAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarAluguel(int id)
    {
        await _aluguelService.DeletarAluguelAsync(id);
        return NoContent();
    }

 
}


