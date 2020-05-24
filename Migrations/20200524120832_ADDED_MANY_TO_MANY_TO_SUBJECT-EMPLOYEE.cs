using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TP.Migrations
{
    public partial class ADDED_MANY_TO_MANY_TO_SUBJECTEMPLOYEE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Employee_EmployeeId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_EmployeeId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Subject");

            migrationBuilder.CreateTable(
                name: "EmployeeSubject",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    SubjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSubject", x => new { x.EmployeeId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_EmployeeSubject_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSubject_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSubject_SubjectId",
                table: "EmployeeSubject",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeSubject");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Subject",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subject_EmployeeId",
                table: "Subject",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Employee_EmployeeId",
                table: "Subject",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
