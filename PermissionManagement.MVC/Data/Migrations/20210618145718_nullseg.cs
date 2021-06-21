using Microsoft.EntityFrameworkCore.Migrations;

namespace PermissionManagement.MVC.Migrations
{
    public partial class nullseg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comptes_Segments_SegmentID",
                table: "Comptes");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Segments_SegmentID",
                table: "Contacts");

            migrationBuilder.AlterColumn<int>(
                name: "SegmentID",
                table: "Contacts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SegmentID",
                table: "Comptes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            

            migrationBuilder.AddForeignKey(
                name: "FK_Comptes_Segments_SegmentID",
                table: "Comptes",
                column: "SegmentID",
                principalTable: "Segments",
                principalColumn: "SegmentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Segments_SegmentID",
                table: "Contacts",
                column: "SegmentID",
                principalTable: "Segments",
                principalColumn: "SegmentID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comptes_Segments_SegmentID",
                table: "Comptes");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Segments_SegmentID",
                table: "Contacts");

            migrationBuilder.AlterColumn<int>(
                name: "SegmentID",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SegmentID",
                table: "Comptes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompteID",
                table: "Comptes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comptes_Segments_SegmentID",
                table: "Comptes",
                column: "SegmentID",
                principalTable: "Segments",
                principalColumn: "SegmentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Segments_SegmentID",
                table: "Contacts",
                column: "SegmentID",
                principalTable: "Segments",
                principalColumn: "SegmentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
