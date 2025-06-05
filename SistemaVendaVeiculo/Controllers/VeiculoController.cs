using Microsoft.AspNetCore.Mvc;
using SistemaVendaVeiculo.Dtos;
using SistemaVendaVeiculo.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class VeiculoController : ControllerBase
{
    private readonly VeiculoService veiculoService;

    public VeiculoController(VeiculoService veiculoService)
    {
        this.veiculoService = veiculoService;
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarVeiculo([FromBody] VeiculoDto dto)
    {
        try
        {
            await veiculoService.CadastrarVeiculoAsync(dto);
            return Ok("Veículo cadastrado com sucesso");
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message, inner = ex.InnerException?.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarVeiculos()
    {
        try
        {
            var veiculos = await veiculoService.ListarVeiculosAsync();
            return Ok(veiculos);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message, inner = ex.InnerException?.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarVeiculoPorId(int id)
    {
        try
        {
            var veiculo = await veiculoService.BuscarVeiculoPorIdAsync(id);
            if (veiculo == null)
                return NotFound("Veículo não encontrado");
            return Ok(veiculo);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message, inner = ex.InnerException?.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarVeiculo(int id, [FromBody] AtualizarVeiculoDto dto)
    {
        try
        {
            await veiculoService.AtualizarVeiculoAsync(id, dto);
            return Ok("Veículo atualizado com sucesso");
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message, inner = ex.InnerException?.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarVeiculo(int id)
    {
        try
        {
            await veiculoService.DeletarVeiculoAsync(id);
            return Ok("Veículo deletado com sucesso");
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message, inner = ex.InnerException?.Message });
        }
    }
}
