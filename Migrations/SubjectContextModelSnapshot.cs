﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TP.Data.Contexts;

namespace TP.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class SubjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TP.Data.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BossId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("BossId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("TP.Data.Entities.EmployeeSubject", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EmployeeId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("EmployeeSubjects");
                });

            modelBuilder.Entity("TP.Data.Entities.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentSubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ParentSubjectId");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("TP.Data.Entities.Employee", b =>
                {
                    b.HasOne("TP.Data.Entities.Employee", null)
                        .WithMany("Subordinates")
                        .HasForeignKey("BossId");
                });

            modelBuilder.Entity("TP.Data.Entities.EmployeeSubject", b =>
                {
                    b.HasOne("TP.Data.Entities.Employee", "Employee")
                        .WithMany("LearnedSubjects")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TP.Data.Entities.Subject", "Subject")
                        .WithMany("EmployeesWhoLearnedIt")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TP.Data.Entities.Subject", b =>
                {
                    b.HasOne("TP.Data.Entities.Subject", null)
                        .WithMany("ChildSubjects")
                        .HasForeignKey("ParentSubjectId");
                });
#pragma warning restore 612, 618
        }
    }
}
