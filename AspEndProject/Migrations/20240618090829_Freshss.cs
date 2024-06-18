using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspEndProject.Migrations
{
    public partial class Freshss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "İmage",
                table: "Freshs",
                newName: "Image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Freshs",
                newName: "İmage");
        }
    }
}
