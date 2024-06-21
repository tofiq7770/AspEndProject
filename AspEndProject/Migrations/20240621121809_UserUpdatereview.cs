using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspEndProject.Migrations
{
    public partial class UserUpdatereview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Reviews");
        }
    }
}
