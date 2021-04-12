using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using System.IO;
using CsvHelper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SustainabilityProgramManagement.Data;
using System.Globalization;
using CsvHelper.Configuration;

namespace SustainabilityProgramManagement.Models
{
    public static class SeedData
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
            };

            // Object-type tables
            using (var context = new SustainabilityProgramManagementContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SustainabilityProgramManagementContext>>()))
            {
                // SustainabilityProgram
                if (!context.SustainabilityProgram.Any())
                {
                    using (var reader = new StreamReader("SeedData/tPrograms.csv"))
                    using (var csv = new CsvReader(reader, config))
                    {
                        var records = csv.GetRecords<SustainabilityProgram>().ToArray();
                        context.SustainabilityProgram.AddRange(records);
                    }
                    context.SaveChangesWithIdentityInsert<SustainabilityProgram>();
                }


                // StaffMember
                if (!context.StaffMember.Any())
                {
                    using (var reader = new StreamReader("SeedData/tStaff.csv"))
                    using (var csv = new CsvReader(reader, config))
                    {
                        var records = csv.GetRecords<StaffMember>().ToArray();
                        foreach (var record in records)
                        {
                            await SeedStaffMember(context, record);
                        }

                    }
                }

                // Project
                if (!context.Project.Any())
                {
                    using (var reader = new StreamReader("SeedData/tProjects.csv"))
                    using (var csv = new CsvReader(reader, config))
                    {
                        var records = csv.GetRecords<Project>().ToArray();
                        foreach (var record in records)
                        {
                            await SeedProject(context, record);
                        }

                    }
                }


                // ProjectSchedule
                if (!context.ProjectSchedule.Any())
                {
                    using (var reader = new StreamReader("SeedData/tProjectSchedules.csv"))
                    using (var csv = new CsvReader(reader, config))
                    {
                        ProjectSchedule[] records = csv.GetRecords<ProjectSchedule>().ToArray();
                        foreach (var record in records)
                            await SeedProjectSchedule(context, record);

                    }
                }

                // TrackingLog
                if (!context.TrackingLog.Any())
                {
                    using (var reader = new StreamReader("SeedData/tTrackingLog.csv"))
                    using (var csv = new CsvReader(reader, config))
                    {
                        var records = csv.GetRecords<TrackingLog>().ToArray();
                        foreach (var record in records)
                            await SeedTrackingLog(context, record);

                    }
                }
            }
        }
        private async static Task<StaffMember> SeedStaffMember(this SustainabilityProgramManagementContext context, StaffMember staffMember)
        {
            var sql = $@"SET IDENTITY_INSERT [dbo].[StaffMember] ON
            INSERT INTO [dbo].[StaffMember]
            ([StaffMemberId], [FirstName], [LastName], [SustainabilityProgramId])
            VALUES ('{staffMember.StaffMemberId}', '{staffMember.FirstName}', '{staffMember.LastName}', '{staffMember.SustainabilityProgramId}' )
            SET IDENTITY_INSERT [dbo].[StaffMember] OFF";
            await context.Database.ExecuteSqlRawAsync(sql);
            return staffMember;
        }
        private async static Task<Project> SeedProject(this SustainabilityProgramManagementContext context, Project project)
        {
            var sql = $@"SET IDENTITY_INSERT [dbo].[Project] ON
            INSERT INTO [dbo].[Project]
            ([ProjectId], [ProjectCode], [ProjectName], [ProjectEndDate], [SustainabilityProgramId])
            VALUES ('{project.ProjectId
            }', '{project.ProjectCode
            }', '{project.ProjectName
            }', '{project.ProjectEndDate
            }', '{project.SustainabilityProgramId}' ) 
            SET IDENTITY_INSERT [dbo].[Project] OFF";
            await context.Database.ExecuteSqlRawAsync(sql);
            return project;
        }
        private async static Task<ProjectSchedule> SeedProjectSchedule(this SustainabilityProgramManagementContext context, ProjectSchedule projectSchedule)
        {
            var staffMember = await context.StaffMember.FindAsync(projectSchedule.StaffMemberId);
            var project = await context.Project.FindAsync(projectSchedule.ProjectId);

            if (staffMember == null || project == null)
                return null;

            projectSchedule.StaffMember = null;
            projectSchedule.Project = null;

            // using SQL to seed because the foreign keys are very picky.

            var sql = $@"SET IDENTITY_INSERT [dbo].[ProjectSchedule] ON
            INSERT INTO [dbo].[ProjectSchedule] ([ProjectScheduleId], [Days], [StaffMemberId], [ProjectId])
            VALUES ('{projectSchedule.ProjectScheduleId}', '{projectSchedule.Days}', '{projectSchedule.StaffMemberId}', '{projectSchedule.ProjectId}')
            SET IDENTITY_INSERT [dbo].[ProjectSchedule] OFF";
            await context.Database.ExecuteSqlRawAsync(sql);

            return projectSchedule;
        }

        private async static Task<TrackingLog> SeedTrackingLog(this SustainabilityProgramManagementContext context, TrackingLog trackingLog)
        {
            var staffMember = await context.StaffMember.FindAsync(trackingLog.StaffMemberId);
            var project = await context.Project.FindAsync(trackingLog.ProjectId);

            if (staffMember == null || project == null)
                return null;

            trackingLog.StaffMember = null;
            trackingLog.Project = null;

            // using SQL to seed because the foreign keys are very picky.

            var sql = $@"SET IDENTITY_INSERT [dbo].[TrackingLog] ON
            INSERT INTO [dbo].[TrackingLog] ([TrackingLogId], [StaffMemberId], [ProjectId], [Hours], [Date])
            VALUES ('{trackingLog.TrackingLogId}', '{trackingLog.StaffMemberId}', '{trackingLog.ProjectId}', '{trackingLog.Hours}', '{trackingLog.Date}')
            SET IDENTITY_INSERT [dbo].[TrackingLog] OFF";
            await context.Database.ExecuteSqlRawAsync(sql);

            return trackingLog;
        }
    }
}
