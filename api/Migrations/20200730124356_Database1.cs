using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class Database1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_veiculos_TiposVeiculo_TipoVeiculoId",
                table: "veiculos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Imagens",
                table: "Imagens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TiposVeiculo",
                table: "TiposVeiculo");

            migrationBuilder.RenameTable(
                name: "Imagens",
                newName: "imagens");

            migrationBuilder.RenameTable(
                name: "TiposVeiculo",
                newName: "tipos_veiculo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_imagens",
                table: "imagens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tipos_veiculo",
                table: "tipos_veiculo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_veiculos_tipos_veiculo_TipoVeiculoId",
                table: "veiculos",
                column: "TipoVeiculoId",
                principalTable: "tipos_veiculo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_veiculos_tipos_veiculo_TipoVeiculoId",
                table: "veiculos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_imagens",
                table: "imagens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tipos_veiculo",
                table: "tipos_veiculo");

            migrationBuilder.RenameTable(
                name: "imagens",
                newName: "Imagens");

            migrationBuilder.RenameTable(
                name: "tipos_veiculo",
                newName: "TiposVeiculo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Imagens",
                table: "Imagens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TiposVeiculo",
                table: "TiposVeiculo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_veiculos_TiposVeiculo_TipoVeiculoId",
                table: "veiculos",
                column: "TipoVeiculoId",
                principalTable: "TiposVeiculo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
