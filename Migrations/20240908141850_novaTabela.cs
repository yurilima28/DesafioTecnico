using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intelectah.Migrations
{
    public partial class novaTabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Fabricantes_FabricantesFabricanteID",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_FabricantesFabricanteID",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "FabricantesFabricanteID",
                table: "Vendas");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_FabricanteID",
                table: "Vendas",
                column: "FabricanteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Fabricantes_FabricanteID",
                table: "Vendas",
                column: "FabricanteID",
                principalTable: "Fabricantes",
                principalColumn: "FabricanteID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Fabricantes_FabricanteID",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_FabricanteID",
                table: "Vendas");

            migrationBuilder.AddColumn<int>(
                name: "FabricantesFabricanteID",
                table: "Vendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_FabricantesFabricanteID",
                table: "Vendas",
                column: "FabricantesFabricanteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Fabricantes_FabricantesFabricanteID",
                table: "Vendas",
                column: "FabricantesFabricanteID",
                principalTable: "Fabricantes",
                principalColumn: "FabricanteID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
