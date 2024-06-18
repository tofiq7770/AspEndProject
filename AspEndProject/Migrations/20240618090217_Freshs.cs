using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspEndProject.Migrations
{
    public partial class Freshs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "İmage",
                table: "Freshs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "İmage",
                table: "Freshs");
        }
    }
}
