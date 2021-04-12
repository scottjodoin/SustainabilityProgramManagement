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
                            // Need to make sure record.SustainabilityProgram is null so that there are no foreign object conflicts.
                            record.SustainabilityProgram = null;
                            context.StaffMember.Add(record);
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
                            // Need to make sure foreign objects are null (keep the ids!) so that there are no foreign object conflicts.
                            record.SustainabilityProgram = null;
                            context.Project.Add(record);
                        }

                    }
                }

                //Special functions here because assigning ids is usually not allowed.
                using var transaction = context.Database.BeginTransaction();

                await context.EnableIdentityInsert<SustainabilityProgram>();
                await context.EnableIdentityInsert<StaffMember>();
                await context.EnableIdentityInsert<Project>();
                context.SaveChanges();
                await context.DisableIdentityInsert<SustainabilityProgram>();
                await context.DisableIdentityInsert<StaffMember>();
                await context.DisableIdentityInsert<Project>();
                transaction.Commit();

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
