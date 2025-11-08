using Microsoft.EntityFrameworkCore;
using SistemaVendaVeiculo;
using SistemaVendaVeiculo.Model;

namespace SistemaVendaVeiculo
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Veiculo>().ToTable("Veiculo");
            modelBuilder.Entity<Fabricante>().ToTable("Fabricante");
            modelBuilder.Entity<Aluguel>().ToTable("Aluguel");
            modelBuilder.Entity<Pagamento>().ToTable("Pagamento");
            modelBuilder.Entity<Aluguel>()
     
                
                
      .Property(a => a.ValorDiaria)
     .HasPrecision(18, 2);

            modelBuilder.Entity<Aluguel>()
                .Property(a => a.ValorTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Pagamento>()
                .Property(p => p.ValorPago)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Pagamento>()
    .HasOne(p => p.Aluguel)
    .WithMany(a => a.Pagamentos)
    .HasForeignKey(p => p.IdAluguel);  



        }

        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Aluguel> Alugueis { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
    }
}
