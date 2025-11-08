using SistemaVendaVeiculo;
using SistemaVendaVeiculo.Model;
using Microsoft.EntityFrameworkCore;
using SistemaVendaVeiculo.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

 

namespace SistemaVendaVeiculo.Service
{
    public class AluguelService
    {
        private readonly ApplicationContext _context;

        public AluguelService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Aluguel> CadastrarAluguelAsync(AluguelDto dto)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.CPF == dto.CPF);

            if (cliente == null)
                throw new InvalidOperationException("Cliente não encontrado");

            var veiculo = await _context.Veiculos.FindAsync(dto.IdVeiculo);
            if (veiculo == null)
                throw new InvalidOperationException("Veículo não encontrado");

            if (dto.DataFim <= dto.DataInicio)
                throw new ArgumentException("Data de fim deve ser posterior à data de início");

            if (dto.QuilometragemFinal < dto.QuilometragemInicial)
                throw new ArgumentException("Quilometragem final não pode ser menor que a inicial");

            int dias = (dto.DataFim - dto.DataInicio).Days;
            decimal valorDasDiarias = dto.ValorDiaria * dias;
            decimal valorTotal = dto.ValorLocacao + valorDasDiarias;

            var aluguel = new Aluguel
            {
                IdCliente = cliente.IdCliente,
                IdVeiculo = dto.IdVeiculo,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim,
                QuilometragemInicial = dto.QuilometragemInicial,
                QuilometragemFinal = null,
                ValorDiaria = dto.ValorDiaria,
                ValorLocacao = dto.ValorLocacao,
                ValorTotal = valorTotal,
                Devolvido = false,
                Observacoes = dto.Observacoes
            };

            _context.Alugueis.Add(aluguel);
            await _context.SaveChangesAsync();

            return aluguel;
        }

        public async Task<Aluguel> BuscarAluguelPorIdAsync(int id)
        {
            var aluguel = await _context.Alugueis.FindAsync(id);
                if (aluguel == null)
                throw new Exception("Aluguel não encontrado");

            return aluguel;
        }

        public async Task<List<Aluguel>> ListarAlugueisAsync()
        {
            var alugueis = await _context.Alugueis.Include(a => a.Cliente).Include(a => a.Veiculo).ToListAsync();
            return alugueis;
        }

        public async Task AtualizarAluguelAsync(int id, AluguelDto dto)
        {
            var aluguel = await _context.Alugueis.FindAsync(id);
            if (aluguel == null)
                throw new Exception("Aluguel não encontrado");

            aluguel.DataInicio = dto.DataInicio;
            aluguel.DataFim = dto.DataFim;
            aluguel.QuilometragemInicial = dto.QuilometragemInicial;
            aluguel.Observacoes = dto.Observacoes;

            _context.Alugueis.Update(aluguel);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAluguelAsync(int id)
        {
            var aluguel = await _context.Alugueis.FindAsync(id);
            if (aluguel == null)
                throw new Exception("Aluguel não encontrado");

            _context.Alugueis.Remove(aluguel);
            await _context.SaveChangesAsync();
        }
    }
}
