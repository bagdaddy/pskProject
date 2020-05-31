using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TP.Migrations
{
    public partial class ADDED_SUBORDINATES_TO_EMPLOYEES : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSubject_Employee_EmployeeId",
                table: "EmployeeSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSubject_Subject_SubjectId",
                table: "EmployeeSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeSubject",
                table: "EmployeeSubject");

            migrationBuilder.RenameTable(
                name: "EmployeeSubject",
                newName: "EmployeeSubjects");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeSubject_SubjectId",
                table: "EmployeeSubjects",
                newName: "IX_EmployeeSubjects_SubjectId");

            migrationBuilder.AddColumn<Guid>(
                name: "BossId",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeSubjects",
                table: "EmployeeSubjects",
                columns: new[] { "EmployeeId", "SubjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_BossId",
                table: "Employee",
                column: "BossId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Employee_BossId",
                table: "Employee",
                column: "BossId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSubjects_Employee_EmployeeId",
                table: "EmployeeSubjects",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSubjects_Subject_SubjectId",
                table: "EmployeeSubjects",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Employee_BossId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSubjects_Employee_EmployeeId",
                table: "EmployeeSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSubjects_Subject_SubjectId",
                table: "EmployeeSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Employee_BossId",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeSubjects",
                table: "EmployeeSubjects");

            migrationBuilder.DropColumn(
                name: "BossId",
                table: "Employee");

            migrationBuilder.RenameTable(
                name: "EmployeeSubjects",
                newName: "EmployeeSubject");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeSubjects_SubjectId",
                table: "EmployeeSubject",
                newName: "IX_EmployeeSubject_SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeSubject",
                table: "EmployeeSubject",
                columns: new[] { "EmployeeId", "SubjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSubject_Employee_EmployeeId",
                table: "EmployeeSubject",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSubject_Subject_SubjectId",
                table: "EmployeeSubject",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
