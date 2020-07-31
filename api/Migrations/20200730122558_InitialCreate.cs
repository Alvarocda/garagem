using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fabricantes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CriadoPor = table.Column<int>(nullable: false),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    AtualizadoPor = table.Column<int>(nullable: false),
                    AtualizadoEm = table.Column<DateTime>(nullable: false),
                    DesativadoPor = table.Column<int>(nullable: false),
                    DesativadoEm = table.Column<DateTime>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fabricantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Imagens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CriadoPor = table.Column<int>(nullable: false),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    AtualizadoPor = table.Column<int>(nullable: false),
                    AtualizadoEm = table.Column<DateTime>(nullable: false),
                    DesativadoPor = table.Column<int>(nullable: false),
                    DesativadoEm = table.Column<DateTime>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    URL = table.Column<string>(nullable: true),
                    VeiculoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposVeiculo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CriadoPor = table.Column<int>(nullable: false),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    AtualizadoPor = table.Column<int>(nullable: false),
                    AtualizadoEm = table.Column<DateTime>(nullable: false),
                    DesativadoPor = table.Column<int>(nullable: false),
                    DesativadoEm = table.Column<DateTime>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposVeiculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CriadoPor = table.Column<int>(nullable: false),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    AtualizadoPor = table.Column<int>(nullable: false),
                    AtualizadoEm = table.Column<DateTime>(nullable: false),
                    DesativadoPor = table.Column<int>(nullable: false),
                    DesativadoEm = table.Column<DateTime>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Senha = table.Column<byte[]>(nullable: false),
                    Chave = table.Column<byte[]>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Role = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "modelos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CriadoPor = table.Column<int>(nullable: false),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    AtualizadoPor = table.Column<int>(nullable: false),
                    AtualizadoEm = table.Column<DateTime>(nullable: false),
                    DesativadoPor = table.Column<int>(nullable: false),
                    DesativadoEm = table.Column<DateTime>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    FabricanteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modelos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_modelos_fabricantes_FabricanteId",
                        column: x => x.FabricanteId,
                        principalTable: "fabricantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "veiculos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CriadoPor = table.Column<int>(nullable: false),
                    CriadoEm = table.Column<DateTime>(nullable: false),
                    AtualizadoPor = table.Column<int>(nullable: false),
                    AtualizadoEm = table.Column<DateTime>(nullable: false),
                    DesativadoPor = table.Column<int>(nullable: false),
                    DesativadoEm = table.Column<DateTime>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    ModeloId = table.Column<int>(nullable: false),
                    Ano = table.Column<string>(maxLength: 9, nullable: false),
                    KM = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Observacao = table.Column<string>(maxLength: 500, nullable: true),
                    Cor = table.Column<string>(maxLength: 30, nullable: false),
                    TipoVeiculoId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 1, nullable: false),
                    FabricanteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_veiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_veiculos_fabricantes_FabricanteId",
                        column: x => x.FabricanteId,
                        principalTable: "fabricantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_veiculos_modelos_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "modelos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_veiculos_TiposVeiculo_TipoVeiculoId",
                        column: x => x.TipoVeiculoId,
                        principalTable: "TiposVeiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_modelos_FabricanteId",
                table: "modelos",
                column: "FabricanteId");

            migrationBuilder.CreateIndex(
                name: "IX_veiculos_FabricanteId",
                table: "veiculos",
                column: "FabricanteId");

            migrationBuilder.CreateIndex(
                name: "IX_veiculos_ModeloId",
                table: "veiculos",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_veiculos_TipoVeiculoId",
                table: "veiculos",
                column: "TipoVeiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Imagens");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "veiculos");

            migrationBuilder.DropTable(
                name: "modelos");

            migrationBuilder.DropTable(
                name: "TiposVeiculo");

            migrationBuilder.DropTable(
                name: "fabricantes");
        }
    }
}
