using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intelectah.Migrations
{
    public partial class TabelaConcessionarias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concessionarias",
                columns: table => new
                {
                    ConcessionariaID = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnderecoCompleto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CapacidadeMax = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_FabricanteID",
                table: "Veiculos",
                column: "FabricanteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_Fabricantes_FabricanteID",
                table: "Veiculos",
                column: "FabricanteID",
                principalTable: "Fabricantes",
                principalColumn: "FabricanteID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_Fabricantes_FabricanteID",
                table: "Veiculos");

            migrationBuilder.DropTable(
                name: "Concessionarias");

            migrationBuilder.DropIndex(
                name: "IX_Veiculos_FabricanteID",
                table: "Veiculos");
        }
    }
}
