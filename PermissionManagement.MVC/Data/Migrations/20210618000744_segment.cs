using Microsoft.EntityFrameworkCore.Migrations;

namespace PermissionManagement.MVC.Migrations
{
    public partial class segment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CRaison",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "DRaison",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "GRaison",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "PRaison",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "Cville",
                table: "Ressources");

            migrationBuilder.DropColumn(
                name: "Nville",
                table: "Ressources");

            migrationBuilder.DropColumn(
                name: "Sville",
                table: "Ressources");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CRaison",
                table: "Segments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DRaison",
                table: "Segments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GRaison",
                table: "Segments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PRaison",
                table: "Segments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cville",
                table: "Ressources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nville",
                table: "Ressources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sville",
                table: "Ressources",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
