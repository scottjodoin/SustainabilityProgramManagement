using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SustainabilityProgramManagement.Models;
using SustainabilityProgramManagement.Models.Reports;


namespace SustainabilityProgramManagement.Data
{
    public class SustainabilityProgramManagementContext : DbContext
    {
        public SustainabilityProgramManagementContext (DbContextOptions<SustainabilityProgramManagementContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }


        public DbSet<StaffMember> StaffMember { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<SustainabilityProgram> SustainabilityProgram { get; set; }

        public DbSet<ProjectSchedule> ProjectSchedule { get; set; }

        public DbSet<TrackingLog> TrackingLog { get; set; }


        public virtual DbSet<StaffProjectsReport> StaffProjectsReport { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StaffProjectsReport>(entity =>
            {
                entity.HasNoKey();
            });
        }
    }
}
