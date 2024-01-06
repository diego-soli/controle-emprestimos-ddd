using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmprestimosLivros.Infra.Data.Migrations
{
    public partial class colunaIsadminUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Usuario");
        }
    }
}
