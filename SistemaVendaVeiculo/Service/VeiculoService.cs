using SistemaVendaVeiculo.Model;
using SistemaVendaVeiculo.Dtos;
using SistemaDeVendaDeVeiculo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SistemaVendaVeiculo.Service
{
    public class VeiculoService
    {
        private readonly ApplicationContext _context;

        public VeiculoService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CadastrarVeiculoAsync(VeiculoDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Modelo))
                throw new Exception("Modelo é obrigatório");

            var fabricante = await _context.Fabricantes.FindAsync(dto.IdFabricante);
            if (fabricante == null)
                throw new Exception("Fabricante não encontrado");

            var veiculo = new Veiculo
            {
                
                Modelo = dto.Modelo,
                AnoFabricacao = dto.AnoFabricacao,
                Quilometragem = dto.Quilometragem,
                Cor = dto.Cor,
                Placa = dto.Placa,
                TipoCombustivel = dto.TipoCombustivel,
                IdFabricante = dto.IdFabricante
            };

            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Veiculo>> ListarVeiculosAsync()
        {
            return await _context.Veiculos.Include(v => v.Fabricante).ToListAsync();
        }

        public async Task<Veiculo?> BuscarVeiculoPorIdAsync(int id)
        {
            return await _context.Veiculos.Include(v => v.Fabricante).FirstOrDefaultAsync(v => v.IdVeiculo == id);
        }

        public async Task AtualizarVeiculoAsync(int id, AtualizarVeiculoDto dto)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);

            if (veiculo == null)
                throw new Exception("Veículo não encontrado");

            veiculo.Modelo = dto.Modelo;
            veiculo.AnoFabricacao = dto.AnoFabricacao;
            veiculo.Quilometragem = dto.Quilometragem;
            veiculo.Cor = dto.Cor;
            veiculo.Placa = dto.Placa;
            veiculo.TipoCombustivel = dto.TipoCombustivel;

            _context.Veiculos.Update(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarVeiculoAsync(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
                throw new Exception("Veículo não encontrado");
            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Veiculo>> ListarVeiculosPorFabricanteAsync(int idFabricante)
        {
            return await _context.Veiculos
                .Where(v => v.IdFabricante == idFabricante)
                .Include(v => v.Fabricante)
                .ToListAsync();
        }

    
    }
}
