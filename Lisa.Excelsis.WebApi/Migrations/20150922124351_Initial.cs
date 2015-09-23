using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace Lisa.Excelsis.WebApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assessment",
                columns: table => new
                {
                    Id = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    ExamId = table.Column<int>(isNullable: true),
                    Examinee = table.Column<string>(isNullable: true),
                    TeacherId = table.Column<int>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessment", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Assessor",
                columns: table => new
                {
                    Id = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessor", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    Id = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    Cohort = table.Column<string>(isNullable: true),
                    DocumentationId = table.Column<int>(isNullable: false),
                    Name = table.Column<string>(isNullable: true),
                    Organisation = table.Column<string>(isNullable: true),
                    Subject = table.Column<string>(isNullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    Id = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Criterium",
                columns: table => new
                {
                    Id = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    Answer = table.Column<bool>(isNullable: true),
                    AssessmentId = table.Column<int>(isNullable: true),
                    Question = table.Column<string>(isNullable: true),
                    Rating = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterium", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Criterium_Assessment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "Id");
                });
            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    Description = table.Column<string>(isNullable: true),
                    ExamId = table.Column<int>(isNullable: true),
                    Rating = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Exam_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Assessor");
            migrationBuilder.DropTable("Criterium");
            migrationBuilder.DropTable("Question");
            migrationBuilder.DropTable("Result");
            migrationBuilder.DropTable("Student");
            migrationBuilder.DropTable("Tag");
            migrationBuilder.DropTable("Assessment");
            migrationBuilder.DropTable("Exam");
        }
    }
}
