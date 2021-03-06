// <auto-generated />
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

            modelBuilder.Entity("SustainabilityProgramManagement.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProjectCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ProjectEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SustainabilityProgramId")
                        .HasColumnType("int");

                    b.HasKey("ProjectId");

                    b.HasIndex("SustainabilityProgramId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("SustainabilityProgramManagement.Models.ProjectSchedule", b =>
                {
                    b.Property<int>("ProjectScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Days")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int?>("StaffMemberId")
                        .HasColumnType("int");

                    b.HasKey("ProjectScheduleId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("StaffMemberId");

                    b.ToTable("ProjectSchedule");
                });

            modelBuilder.Entity("SustainabilityProgramManagement.Models.StaffMember", b =>
                {
                    b.Property<int>("StaffMemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SustainabilityProgramId")
                        .HasColumnType("int");

                    b.HasKey("StaffMemberId");

                    b.HasIndex("SustainabilityProgramId");

                    b.ToTable("StaffMember");
                });

            modelBuilder.Entity("SustainabilityProgramManagement.Models.SustainabilityProgram", b =>
                {
                    b.Property<int>("SustainabilityProgramId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgramName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SustainabilityProgramId");

                    b.ToTable("SustainabilityProgram");
                });

            modelBuilder.Entity("SustainabilityProgramManagement.Models.TrackingLog", b =>
                {
                    b.Property<int>("TrackingLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Hours")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int?>("StaffMemberId")
                        .HasColumnType("int");

                    b.HasKey("TrackingLogId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("StaffMemberId");

                    b.ToTable("TrackingLog");
                });

            modelBuilder.Entity("SustainabilityProgramManagement.Models.Project", b =>
                {
                    b.HasOne("SustainabilityProgramManagement.Models.SustainabilityProgram", "SustainabilityProgram")
                        .WithMany("Projects")
                        .HasForeignKey("SustainabilityProgramId");
                });

            modelBuilder.Entity("SustainabilityProgramManagement.Models.ProjectSchedule", b =>
                {
                    b.HasOne("SustainabilityProgramManagement.Models.Project", "Project")
                        .WithMany("ProjectSchedules")
                        .HasForeignKey("ProjectId");

                    b.HasOne("SustainabilityProgramManagement.Models.StaffMember", "StaffMember")
                        .WithMany("ProjectSchedules")
                        .HasForeignKey("StaffMemberId");
                });

            modelBuilder.Entity("SustainabilityProgramManagement.Models.StaffMember", b =>
                {
                    b.HasOne("SustainabilityProgramManagement.Models.SustainabilityProgram", "SustainabilityProgram")
                        .WithMany("Staff")
                        .HasForeignKey("SustainabilityProgramId");
                });

            modelBuilder.Entity("SustainabilityProgramManagement.Models.TrackingLog", b =>
                {
                    b.HasOne("SustainabilityProgramManagement.Models.Project", "Project")
                        .WithMany("TrackingLogs")
                        .HasForeignKey("ProjectId");

                    b.HasOne("SustainabilityProgramManagement.Models.StaffMember", "StaffMember")
                        .WithMany("TrackingLogs")
                        .HasForeignKey("StaffMemberId");
                });
#pragma warning restore 612, 618
        }
    }
}
