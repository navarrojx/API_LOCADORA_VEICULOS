using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVendaVeiculo.Migrations
{
    /// <inheritdoc />
    public partial class ValorLocação : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValorLocacao",
                table: "Aluguel",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorLocacao",
                table: "Aluguel");
        }
    }
}
