using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MST_REST_Web_API.Migrations
{
    public partial class Smallchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Scripts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSend",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Scripts");

            migrationBuilder.DropColumn(
                name: "IsSend",
                table: "Orders");
        }
    }
}
