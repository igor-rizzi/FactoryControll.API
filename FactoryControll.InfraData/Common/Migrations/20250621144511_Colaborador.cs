using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FactoryControll.InfraData.Common.Migrations
{
    /// <inheritdoc />
    public partial class Colaborador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colaboradores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAdmissao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataRescisao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Usuario = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CargoId = table.Column<int>(type: "integer", nullable: false),
                    FuncaoId = table.Column<int>(type: "integer", nullable: false),
                    CargoId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaboradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colaboradores_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Colaboradores_Cargos_CargoId1",
                        column: x => x.CargoId1,
                        principalTable: "Cargos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Colaboradores_Funcoes_FuncaoId",
                        column: x => x.FuncaoId,
                        principalTable: "Funcoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_Nome",
                table: "Cargos",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_CargoId",
                table: "Colaboradores",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_CargoId1",
                table: "Colaboradores",
                column: "CargoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_Cpf",
                table: "Colaboradores",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_Email",
                table: "Colaboradores",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_FuncaoId",
                table: "Colaboradores",
                column: "FuncaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_Usuario",
                table: "Colaboradores",
                column: "Usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Funcoes_Nome",
                table: "Funcoes",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colaboradores");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "Funcoes");
        }
    }
}
