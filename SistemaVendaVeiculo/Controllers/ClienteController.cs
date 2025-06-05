using Microsoft.AspNetCore.Mvc;
using SistemaVendaVeiculo.Service;
using SistemaVendaVeiculo.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace SistemaVendaVeiculo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService clienteService;
        public ClienteController(ClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarCliente([FromBody] ClienteDto dto)
        {
            try
            {
                await clienteService.CadastrarClienteAsync(dto);
                return Ok("Cliente cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message, inner = ex.InnerException?.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarClientes()
        {
            try
            {
                var clientes = await clienteService.ListarClientesAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message, inner = ex.InnerException?.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarClientePorId(int id)
        {
            try
            {
                var cliente = await clienteService.BuscarClientePorIdAsync(id);
                if (cliente == null)
                    return NotFound("Cliente não encontrado");

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message, inner = ex.InnerException?.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente(int id, [FromBody] ClienteDto dto)
        {
            try
            {
                await clienteService.AtualizarClienteAsync(id, dto);
                return Ok("Cliente atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message, inner = ex.InnerException?.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarCliente(int id)
        {
            try
            {
                await clienteService.DeletarClienteAsync(id);
                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message, inner = ex.InnerException?.Message });
            }
        }


        [HttpGet("cpf")]
        public async Task<IActionResult> BuscarClientePorCpf([FromQuery] string valor)
        {
            try
            {
                var cliente = await clienteService.BuscarClientePorCpfAsync(valor);
                if (cliente == null)
                    return NotFound("Cliente não encontrado com esse CPF.");

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message, inner = ex.InnerException?.Message });
            }
        }

        

    } } 

