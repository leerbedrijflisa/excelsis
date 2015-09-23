using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace Lisa.Excelsis.WebApi.Migrations
{
    public partial class AddedClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Organisation", table: "Exam");
            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    Name = table.Column<string>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tag",
                isNullable: false,
                defaultValue: "");
            migrationBuilder.AddColumn<string>(
                name: "Cohort",
                table: "Student",
                isNullable: false,
                defaultValue: "");
            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Student",
                isNullable: false,
                defaultValue: "");
            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "Student",
                isNullable: false,
                defaultValue: "");
            migrationBuilder.AddColumn<string>(
                name: "SchoolNumber",
                table: "Student",
                isNullable: false,
                defaultValue: "");
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Question",
                isNullable: false);
            migrationBuilder.AlterColumn<int>(
                name: "Subject",
                table: "Exam",
                isNullable: false);
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Exam",
                isNullable: false);
            migrationBuilder.AlterColumn<string>(
                name: "Cohort",
                table: "Exam",
                isNullable: false);
            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "Exam",
                isNullable: false,
                defaultValue: "");
            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "Criterium",
                isNullable: false);
            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                table: "Assessor",
                isNullable: false,
                defaultValue: "");
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Assessor",
                isNullable: false,
                defaultValue: "");
            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Assessor",
                isNullable: false,
                defaultValue: "");
            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "Assessor",
                isNullable: false,
                defaultValue: "");
            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Assessment",
                isNullable: false);
            migrationBuilder.AlterColumn<string>(
                name: "Examinee",
                table: "Assessment",
                isNullable: false);
            migrationBuilder.AlterColumn<int>(
                name: "ExamId",
                table: "Assessment",
                isNullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Name", table: "Tag");
            migrationBuilder.DropColumn(name: "Cohort", table: "Student");
            migrationBuilder.DropColumn(name: "Firstname", table: "Student");
            migrationBuilder.DropColumn(name: "Lastname", table: "Student");
            migrationBuilder.DropColumn(name: "SchoolNumber", table: "Student");
            migrationBuilder.DropColumn(name: "Organization", table: "Exam");
            migrationBuilder.DropColumn(name: "Abbreviation", table: "Assessor");
            migrationBuilder.DropColumn(name: "Email", table: "Assessor");
            migrationBuilder.DropColumn(name: "Firstname", table: "Assessor");
            migrationBuilder.DropColumn(name: "Lastname", table: "Assessor");
            migrationBuilder.DropTable("Subject");
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Question",
                isNullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Exam",
                isNullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Exam",
                isNullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "Cohort",
                table: "Exam",
                isNullable: true);
            migrationBuilder.AddColumn<string>(
                name: "Organisation",
                table: "Exam",
                isNullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "Question",
                table: "Criterium",
                isNullable: true);
            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Assessment",
                isNullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "Examinee",
                table: "Assessment",
                isNullable: true);
            migrationBuilder.AlterColumn<int>(
                name: "ExamId",
                table: "Assessment",
                isNullable: true);
        }
    }
}
