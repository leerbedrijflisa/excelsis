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
    partial class Initial
    {
        public override string Id
        {
            get { return "20150922124351_Initial"; }
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta7-15540")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn);

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Assessment", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ExamId");

                    b.Property<string>("Examinee");

                    b.Property<int?>("TeacherId");

                    b.Key("Id");
                });

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Assessor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Key("Id");
                });

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Criterium", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Answer");

                    b.Property<int?>("AssessmentId");

                    b.Property<string>("Question");

                    b.Property<int>("Rating");

                    b.Key("Id");
                });

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cohort");

                    b.Property<int>("DocumentationId");

                    b.Property<string>("Name");

                    b.Property<string>("Organisation");

                    b.Property<string>("Subject");

                    b.Key("Id");
                });

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

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

                    b.Key("Id");
                });

            modelBuilder.Entity("Lisa.Excelsis.WebApi.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

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
