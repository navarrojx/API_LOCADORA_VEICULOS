using Microsoft.AspNetCore.Mvc;
using SistemaVendaVeiculo.Dtos;
using SistemaVendaVeiculo.Service;
using System;
using System.Threading.Tasks;

namespace SistemaVendaVeiculo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly PagamentoService _pagamentoService;

        public PagamentoController(PagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService;
        }

     
        [HttpGet]
        public async Task<IActionResult> ListarPagamentos()
        {
            var pagamentos = await _pagamentoService.ListarPagamentosAsync();
            return Ok(pagamentos);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPagamentoPorId(int id)
        {
            var pagamento = await _pagamentoService.BuscarPagamentoPorIdAsync(id);
            if (pagamento == null)
                return NotFound(new { message = "Pagamento não encontrado." });

            return Ok(pagamento);
        }


        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarPagamento([FromBody] PagamentoDtos dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _pagamentoService.RegistrarPagamentoAsync(dto);
                return Ok(new { message = "Pagamento registrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("Atualizar/{id}")]
        public async Task<IActionResult> AtualizarPagamento(int id, [FromBody] PagamentoDtos dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _pagamentoService.AtualizarPagamentoAsync(id, dto);
                return Ok(new { message = "Pagamento atualizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPagamento(int id)
        {
            try
            {
                await _pagamentoService.DeletarPagamentoAsync(id);
                return Ok(new { message = "Pagamento deletado com sucesso!" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}