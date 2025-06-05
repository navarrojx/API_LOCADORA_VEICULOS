using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVendaVeiculo.Migrations
{
    /// <inheritdoc />
    public partial class AjusteRelacionamentoPagamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagamento_Aluguel_AluguelIdAluguel",
                table: "Pagamento");

            migrationBuilder.DropIndex(
                name: "IX_Pagamento_AluguelIdAluguel",
                table: "Pagamento");

            migrationBuilder.DropColumn(
                name: "AluguelIdAluguel",
                table: "Pagamento");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_IdAluguel",
                table: "Pagamento",
                column: "IdAluguel");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagamento_Aluguel_IdAluguel",
                table: "Pagamento",
                column: "IdAluguel",
                principalTable: "Aluguel",
                principalColumn: "IdAluguel",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagamento_Aluguel_IdAluguel",
                table: "Pagamento");

            migrationBuilder.DropIndex(
                name: "IX_Pagamento_IdAluguel",
                table: "Pagamento");

            migrationBuilder.AddColumn<int>(
                name: "AluguelIdAluguel",
                table: "Pagamento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_AluguelIdAluguel",
                table: "Pagamento",
                column: "AluguelIdAluguel");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagamento_Aluguel_AluguelIdAluguel",
                table: "Pagamento",
                column: "AluguelIdAluguel",
                principalTable: "Aluguel",
                principalColumn: "IdAluguel",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
