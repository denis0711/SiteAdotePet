using Microsoft.EntityFrameworkCore.Migrations;

namespace SitePet.Infrastructure.Migrations
{
    public partial class Second2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioEmail",
                table: "Pets",
                newName: "Usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Usuario",
                table: "Pets",
                newName: "UsuarioEmail");
        }
    }
}
