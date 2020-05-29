using Microsoft.EntityFrameworkCore.Migrations;

namespace TP.Migrations
{
    public partial class ADDED_RESTRICTIONS_TO_EMPLOYEES : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GlobalDayLimit",
                table: "Employee",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocalDayLimit",
                table: "Employee",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GlobalDayLimit",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "LocalDayLimit",
                table: "Employee");
        }
    }
}
