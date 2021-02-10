using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleTemplate_Shop.Migrations
{
    public partial class AddMark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Marked",
                table: "CallBacks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marked",
                table: "CallBacks");
        }
    }
}
