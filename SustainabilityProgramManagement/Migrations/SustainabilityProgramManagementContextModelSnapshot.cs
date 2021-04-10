﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SustainabilityProgramManagement.Data;

namespace SustainabilityProgramManagement.Migrations
{
    [DbContext(typeof(SustainabilityProgramManagementContext))]
    partial class SustainabilityProgramManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SustainabilityProgramManagement.Models.Program", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Program");
                });

            modelBuilder.Entity("SustainabilityProgramManagement.Models.Project", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ProgramID")
                        .HasColumnType("int");

                    b.Property<string>("ProjectCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ProgramID");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("SustainabilityProgramManagement.Models.ProjectSchedule", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Days")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectID")
                        .HasColumnType("int");

                    b.Property<int?>("StaffMemberID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("StaffMemberID");

                    b.ToTable("ProjectSchedule");
                });

            modelBuilder.Entity("SustainabilityProgramManagement.Models.StaffMember", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProgramID")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProgramID");

                    b.HasIndex("ProjectID");

                    b.ToTable("StaffMember");
                });

            modelBuilder.Entity("SustainabilityProgramManagement.Models.TrackingLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Hours")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectID")
                        .HasColumnType("int");

                    b.Property<int?>("StaffMemberID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("StaffMemberID");

                    b.ToTable("TrackingLog");
                });

            modelBuilder.Entity("SustainabilityProgramManagement.Models.Project", b =>
                {
                    b.HasOne("SustainabilityProgramManagement.Models.Program", "Program")
                        .WithMany("Projects")
                        .HasForeignKey("ProgramID");
                });

            modelBuilder.Entity("SustainabilityProgramManagement.Models.ProjectSchedule", b =>
                {
                    b.HasOne("SustainabilityProgramManagement.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID");

                    b.HasOne("SustainabilityProgramManagement.Models.StaffMember", "StaffMember")
                        .WithMany("ProjectSchedules")
                        .HasForeignKey("StaffMemberID");
                });

            modelBuilder.Entity("SustainabilityProgramManagement.Models.StaffMember", b =>
                {
                    b.HasOne("SustainabilityProgramManagement.Models.Program", "Program")
                        .WithMany("Staff")
                        .HasForeignKey("ProgramID");

                    b.HasOne("SustainabilityProgramManagement.Models.Project", null)
                        .WithMany("Staff")
                        .HasForeignKey("ProjectID");
                });

            modelBuilder.Entity("SustainabilityProgramManagement.Models.TrackingLog", b =>
                {
                    b.HasOne("SustainabilityProgramManagement.Models.Project", "Project")
                        .WithMany("TrackingLogs")
                        .HasForeignKey("ProjectID");

                    b.HasOne("SustainabilityProgramManagement.Models.StaffMember", "StaffMember")
                        .WithMany("TrackingLogs")
                        .HasForeignKey("StaffMemberID");
                });
#pragma warning restore 612, 618
        }
    }
}
