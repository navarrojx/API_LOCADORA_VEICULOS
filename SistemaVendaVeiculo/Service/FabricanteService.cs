using SistemaDeVendaDeVeiculo;
using Microsoft.EntityFrameworkCore;
using SistemaVendaVeiculo.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SistemaVendaVeiculo.Service
{
    public class FabricanteService
    {
        private readonly ApplicationContext _context;

        public FabricanteService(ApplicationContext context)
        {
            _context = context;
        }

        // Cadastrar Fabricante
        public async Task CadastrarFabricanteAsync(Fabricante fabricante)
        {
            if (string.IsNullOrWhiteSpace(fabricante.Nome))
                throw new Exception("Nome do fabricante é obrigatório.");

            if (fabricante.Fundacao == default)
                throw new Exception("Data de fundação do fabricante é obrigatória.");

            _context.Fabricantes.Add(fabricante);
            await _context.SaveChangesAsync();
        }

        // Obter Fabricante por ID
        public async Task<Fabricante?> ObterFabricantePorIdAsync(int id)
        {
            var fabricante = await _context.Fabricantes.FindAsync(id);
            if (fabricante == null)
            {
                throw new Exception("Fabricante não encontrado");
                
            }
            return fabricante;
        }

        // Listar todos os Fabricantes
        public async Task<List<Fabricante>> ListarFabricantesAsync()
        {
            return await _context.Fabricantes.ToListAsync();
        }

        // Atualizar Fabricante
        public async Task AtualizarFabricanteAsync(int id, Fabricante fabricante)
        {
            var fabricanteExistente = await _context.Fabricantes.FindAsync(id);
            if (fabricanteExistente == null)
                throw new Exception("Fabricante não encontrado.");

            fabricanteExistente.Nome = fabricante.Nome;
            fabricanteExistente.PaisOrigem = fabricante.PaisOrigem;
            fabricanteExistente.Fundacao = fabricante.Fundacao;

            _context.Fabricantes.Update(fabricanteExistente);
            await _context.SaveChangesAsync();
        }

        // Deletar Fabricante
        public async Task DeletarFabricanteAsync(int id)
        {
            var fabricante = await _context.Fabricantes.FindAsync(id);
            if (fabricante == null)
                throw new Exception("Fabricante não encontrado.");

            _context.Fabricantes.Remove(fabricante);
            await _context.SaveChangesAsync();
        }
    }
}
