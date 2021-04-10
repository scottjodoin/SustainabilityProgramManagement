using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SustainabilityProgramManagement.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Program",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(nullable: true),
                    Abbreviation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Program", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectCode = table.Column<string>(nullable: true),
                    ProjectName = table.Column<string>(nullable: true),
                    ProgramID = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Project_Program_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "Program",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StaffMember",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ProgramID = table.Column<int>(nullable: true),
                    ProjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMember", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StaffMember_Program_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "Program",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaffMember_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSchedule",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffMemberID = table.Column<int>(nullable: true),
                    ProjectID = table.Column<int>(nullable: true),
                    Days = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSchedule", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProjectSchedule_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectSchedule_StaffMember_StaffMemberID",
                        column: x => x.StaffMemberID,
                        principalTable: "StaffMember",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackingLog",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffMemberID = table.Column<int>(nullable: true),
                    ProjectID = table.Column<int>(nullable: true),
                    Hours = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrackingLog_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrackingLog_StaffMember_StaffMemberID",
                        column: x => x.StaffMemberID,
                        principalTable: "StaffMember",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProgramID",
                table: "Project",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSchedule_ProjectID",
                table: "ProjectSchedule",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSchedule_StaffMemberID",
                table: "ProjectSchedule",
                column: "StaffMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMember_ProgramID",
                table: "StaffMember",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMember_ProjectID",
                table: "StaffMember",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingLog_ProjectID",
                table: "TrackingLog",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingLog_StaffMemberID",
                table: "TrackingLog",
                column: "StaffMemberID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectSchedule");

            migrationBuilder.DropTable(
                name: "TrackingLog");

            migrationBuilder.DropTable(
                name: "StaffMember");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Program");
        }
    }
}
