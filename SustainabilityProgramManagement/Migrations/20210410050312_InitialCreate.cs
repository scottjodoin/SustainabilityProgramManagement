using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SustainabilityProgramManagement.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SustainabilityProgram",
                columns: table => new
                {
                    SustainabilityProgramId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramName = table.Column<string>(nullable: true),
                    Abbreviation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SustainabilityProgram", x => x.SustainabilityProgramId);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectCode = table.Column<string>(nullable: true),
                    ProjectName = table.Column<string>(nullable: true),
                    ProjectEndDate = table.Column<DateTime>(nullable: false),
                    SustainabilityProgramId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Project_SustainabilityProgram_SustainabilityProgramId",
                        column: x => x.SustainabilityProgramId,
                        principalTable: "SustainabilityProgram",
                        principalColumn: "SustainabilityProgramId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StaffMember",
                columns: table => new
                {
                    StaffMemberId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    SustainabilityProgramId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMember", x => x.StaffMemberId);
                    table.ForeignKey(
                        name: "FK_StaffMember_SustainabilityProgram_SustainabilityProgramId",
                        column: x => x.SustainabilityProgramId,
                        principalTable: "SustainabilityProgram",
                        principalColumn: "SustainabilityProgramId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSchedule",
                columns: table => new
                {
                    ProjectScheduleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Days = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    StaffMemberId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSchedule", x => x.ProjectScheduleId);
                    table.ForeignKey(
                        name: "FK_ProjectSchedule_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectSchedule_StaffMember_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMember",
                        principalColumn: "StaffMemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackingLog",
                columns: table => new
                {
                    TrackingLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffMemberId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true),
                    Hours = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingLog", x => x.TrackingLogId);
                    table.ForeignKey(
                        name: "FK_TrackingLog_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrackingLog_StaffMember_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMember",
                        principalColumn: "StaffMemberId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_SustainabilityProgramId",
                table: "Project",
                column: "SustainabilityProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSchedule_ProjectId",
                table: "ProjectSchedule",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSchedule_StaffMemberId",
                table: "ProjectSchedule",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMember_SustainabilityProgramId",
                table: "StaffMember",
                column: "SustainabilityProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingLog_ProjectId",
                table: "TrackingLog",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingLog_StaffMemberId",
                table: "TrackingLog",
                column: "StaffMemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectSchedule");

            migrationBuilder.DropTable(
                name: "TrackingLog");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "StaffMember");

            migrationBuilder.DropTable(
                name: "SustainabilityProgram");
        }
    }
}
