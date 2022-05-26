using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MST_REST_Web_API.Migrations
{
    public partial class TesterIdtoScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TesterId",
                table: "Scripts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TesterId",
                table: "Scripts");
        }
    }
}
