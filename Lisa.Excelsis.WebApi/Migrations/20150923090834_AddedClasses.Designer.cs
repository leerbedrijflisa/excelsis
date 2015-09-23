using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Lisa.Excelsis.WebApi.Models;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace Lisa.Excelsis.WebApi.Migrations
{
    [DbContext(typeof(ExcelsisDb))]
    partial class AddedClasses
    {
        public override string Id
        {
            get { return "20150923090834_AddedClasses"; }
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta7-15540")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn);

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Assessment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExamId");

                    b.Property<string>("Examinee")
                        .Required();

                    b.Property<int>("TeacherId");

                    b.Key("Id");
                });

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Assessor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation")
                        .Required();

                    b.Property<string>("Email")
                        .Required();

                    b.Property<string>("Firstname")
                        .Required();

                    b.Property<string>("Lastname")
                        .Required();

                    b.Key("Id");
                });

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Criterium", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Answer");

                    b.Property<int?>("AssessmentId");

                    b.Property<string>("Question")
                        .Required();

                    b.Property<int>("Rating");

                    b.Key("Id");
                });

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cohort")
                        .Required();

                    b.Property<int>("DocumentationId");

                    b.Property<string>("Name")
                        .Required();

                    b.Property<string>("Organization")
                        .Required();

                    b.Property<int>("Subject");

                    b.Key("Id");
                });

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .Required();

                    b.Property<int?>("ExamId");

                    b.Property<int>("Rating");

                    b.Key("Id");
                });

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Key("Id");
                });

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cohort")
                        .Required();

                    b.Property<string>("Firstname")
                        .Required();

                    b.Property<string>("Lastname")
                        .Required();

                    b.Property<string>("SchoolNumber")
                        .Required();

                    b.Key("Id");
                });

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .Required();

                    b.Key("Id");
                });

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .Required();

                    b.Key("Id");
                });

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Criterium", b =>
                {
                    b.Reference("Lisa.Excelsis.WebApi.Models.Assessment")
                        .InverseCollection()
                        .ForeignKey("AssessmentId");
                });

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Question", b =>
                {
                    b.Reference("Lisa.Excelsis.WebApi.Models.Exam")
                        .InverseCollection()
                        .ForeignKey("ExamId");
                });
        }
    }
}
