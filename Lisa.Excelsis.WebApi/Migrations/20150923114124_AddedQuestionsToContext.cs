using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Lisa.Excelsis.WebApi.Migrations
{
    public partial class AddedQuestionsToContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Question_Exam_ExamId", table: "Question");
            migrationBuilder.AlterColumn<int>(
                name: "ExamId",
                table: "Question",
                isNullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ExamId",
                table: "Question",
                isNullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_Question_Exam_ExamId",
                table: "Question",
                column: "ExamId",
                principalTable: "Exam",
                principalColumn: "Id");
        }
    }
}
