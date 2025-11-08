using SistemaVendaVeiculo.Model;
using SistemaVendaVeiculo.Dtos;
using SistemaVendaVeiculo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SistemaVendaVeiculo.Service
{
    public class ClienteService
    {
        private readonly ApplicationContext _context;
        public ClienteService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CadastrarClienteAsync(ClienteDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new Exception("Nome é obrigatório");

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new Exception("O email é obrigatório");

            if (string.IsNullOrWhiteSpace(dto.CPF))
                throw new Exception("CPF é obrigatório");

            var cliente = new Cliente
            {
                Nome = dto.Nome,
                Email = dto.Email,
                CPF = dto.CPF,
                Telefone = dto.Telefone,
                DataNascimento = dto.DataNascimento,
                Endereco = dto.Endereco
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cliente>> ListarClientesAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> BuscarClientePorIdAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                throw new Exception("Cliente não encontrado");

            return cliente;
        }


        public async Task AtualizarClienteAsync(int id, ClienteDto dto)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                throw new Exception("Cliente não encontrado");

            cliente.Nome = dto.Nome;
            cliente.Email = dto.Email;
            cliente.CPF = dto.CPF;
            cliente.Telefone = dto.Telefone;
            cliente.DataNascimento = dto.DataNascimento;
            cliente.Endereco = dto.Endereco;

            await _context.SaveChangesAsync();
        }

        public async Task DeletarClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                throw new Exception("Cliente não encontrado");

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
        public async Task<Cliente?> BuscarClientePorCpfAsync(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new Exception("O CPF para busca não pode ser vazio.");

            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.CPF == cpf);
        }

    }
}
