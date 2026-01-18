using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoryControll.InfraData.Common.Migrations
{
    /// <inheritdoc />
    public partial class VinculoUsuarioColaborador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Colaboradores_Usuario",
                table: "Colaboradores");

            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "Colaboradores");

            migrationBuilder.AddColumn<long>(
                name: "ColaboradorId",
                table: "Usuarios",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserLoginId",
                table: "Usuarios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioId",
                table: "Colaboradores",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UsuarioAppId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ColaboradorId",
                table: "Usuarios",
                column: "ColaboradorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_UsuarioId",
                table: "Colaboradores",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UsuarioAppId",
                table: "AspNetUsers",
                column: "UsuarioAppId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Usuarios_UsuarioAppId",
                table: "AspNetUsers",
                column: "UsuarioAppId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Colaboradores_ColaboradorId",
                table: "Usuarios",
                column: "ColaboradorId",
                principalTable: "Colaboradores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Usuarios_UsuarioAppId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Colaboradores_ColaboradorId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_ColaboradorId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Colaboradores_UsuarioId",
                table: "Colaboradores");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UsuarioAppId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ColaboradorId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UserLoginId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Colaboradores");

            migrationBuilder.DropColumn(
                name: "UsuarioAppId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "Colaboradores",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_Usuario",
                table: "Colaboradores",
                column: "Usuario",
                unique: true);
        }
    }
}
