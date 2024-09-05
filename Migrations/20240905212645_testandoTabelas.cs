using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intelectah.Migrations
{
    public partial class testandoTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vendas_VeiculoID",
                table: "Vendas");

            migrationBuilder.AddColumn<int>(
                name: "VendaID",
                table: "Veiculos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_VeiculoID",
                table: "Vendas",
                column: "VeiculoID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vendas_VeiculoID",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "VendaID",
                table: "Veiculos");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_VeiculoID",
                table: "Vendas",
                column: "VeiculoID");
        }
    }
}
