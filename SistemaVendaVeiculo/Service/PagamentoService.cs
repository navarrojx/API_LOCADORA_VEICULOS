using SistemaVendaVeiculo.Model;
using SistemaVendaVeiculo.Dtos;
using Microsoft.EntityFrameworkCore;
using SistemaVendaVeiculo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


namespace SistemaVendaVeiculo.Service
{
    public class PagamentoService
    {
        private readonly ApplicationContext _context;

        public PagamentoService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pagamento>> ListarPagamentosAsync()
        {
            return await _context.Pagamentos.ToListAsync();
        }

        public async Task<Pagamento> BuscarPagamentoPorIdAsync(int id)
        {
            return await _context.Pagamentos.FindAsync(id);
        }


        public async Task RegistrarPagamentoAsync(PagamentoDtos pagamentoDto)
{
    await using var transaction = await _context.Database.BeginTransactionAsync();

    try
    {
        var aluguel = await _context.Alugueis.FindAsync(pagamentoDto.IdAluguel)
                      ?? throw new InvalidOperationException("Aluguel não encontrado");

        var totalJaPago = await _context.Pagamentos
            .Where(p => p.IdAluguel == pagamentoDto.IdAluguel)
            .SumAsync(p => p.ValorPago);

        var totalPago = totalJaPago + pagamentoDto.ValorPago;

        if (totalPago > aluguel.ValorTotal)
            throw new InvalidOperationException("O valor pago excede o valor total do aluguel");

                var pagamento = new Pagamento
                {
                    IdAluguel = pagamentoDto.IdAluguel,
                    DataPagamento = DateTime.UtcNow,
                    MetodoPagamento = pagamentoDto.MetodoPagamento,
                    ValorPago = pagamentoDto.ValorPago,
                    StatusPagamento = totalPago == aluguel.ValorTotal
                        ? StatusPagamento.Pago
                        : StatusPagamento.EmAtraso
                };

        _context.Pagamentos.Add(pagamento);

        if (totalPago == aluguel.ValorTotal)
        {
            aluguel.Devolvido = true;

            var veiculo = await _context.Veiculos.FindAsync(aluguel.IdVeiculo);
            if (veiculo != null &&
                aluguel.QuilometragemFinal.HasValue &&
                aluguel.QuilometragemFinal > aluguel.QuilometragemInicial)
            {
                veiculo.Quilometragem += aluguel.QuilometragemFinal.Value - aluguel.QuilometragemInicial;
                _context.Veiculos.Update(veiculo);
            }
        }

        _context.Alugueis.Update(aluguel);

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }
    catch
    {
        await transaction.RollbackAsync();
        throw;
    }
}


        public async Task AtualizarPagamentoAsync(int id, PagamentoDtos dto)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento == null)
                throw new Exception("Pagamento não encontrado");

            pagamento.ValorPago = dto.ValorPago;
            pagamento.MetodoPagamento = dto.MetodoPagamento;
            pagamento.StatusPagamento = dto.StatusPagamento;

            _context.Pagamentos.Update(pagamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarPagamentoAsync(int id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento == null)
                throw new Exception("Pagamento não encontrado");

            _context.Pagamentos.Remove(pagamento);
            await _context.SaveChangesAsync();
        }
    }
}
