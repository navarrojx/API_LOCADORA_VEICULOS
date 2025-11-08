using System;
using System.Threading.Tasks;
using Xunit;
using SistemaVendaVeiculo.Service;  
using SistemaVendaVeiculo.Dtos;     
using SistemaVendaVeiculo.Model;    
using Microsoft.EntityFrameworkCore;

namespace SistemaVendaVeiculo.Test
{
    public class ClienteServiceTest
    {
        // Cria um ApplicationContext em memória
        private ApplicationContext CriarContextoInMemory()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Banco isolado por teste
                .Options;

            return new ApplicationContext(options);
        }

        [Fact]
        public async Task CadastrarCliente_DeveSalvarNoBanco()
        {
            // Arrange
            var context = CriarContextoInMemory();
            var service = new ClienteService(context);

            var dto = new ClienteDto
            {
                Nome = "João Silva",
                Email = "joao@email.com",
                CPF = "12345678900",
                Telefone = "11999999999",
                DataNascimento = new DateTime(1990, 1, 1),
                Endereco = "Rua A, 123"
            };

            // Act
            await service.CadastrarClienteAsync(dto);

            // Assert
            var cliente = await context.Clientes.FirstOrDefaultAsync(c => c.CPF == "12345678900");
            Assert.NotNull(cliente);
            Assert.Equal("João Silva", cliente.Nome);
        }

        [Fact]
        public async Task CadastrarCliente_SemNome_DeveLancarExcecao()
        {
            // Arrange
            var context = CriarContextoInMemory();
            var service = new ClienteService(context);

            var dto = new ClienteDto
            {
                Nome = "",
                Email = "email@email.com",
                CPF = "12345678900"
            };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => service.CadastrarClienteAsync(dto));
            Assert.Equal("Nome é obrigatório", ex.Message);
        }

        [Fact]
        public async Task BuscarClientePorCpf_DeveRetornarClienteCorreto()
        {
            // Arrange
            var context = CriarContextoInMemory();

            var cliente = new Cliente
            {
                Nome = "Maria",
                Email = "maria@email.com",
                CPF = "11122233344"
            };
            context.Clientes.Add(cliente);
            await context.SaveChangesAsync();

            var service = new ClienteService(context);

            // Act
            var result = await service.BuscarClientePorCpfAsync("11122233344");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Maria", result.Nome);
        }
    }
}
