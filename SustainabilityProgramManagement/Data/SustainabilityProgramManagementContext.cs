using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SustainabilityProgramManagement.Models;


namespace SustainabilityProgramManagement.Data
{
    public class SustainabilityProgramManagementContext : DbContext
    {
        public SustainabilityProgramManagementContext (DbContextOptions<SustainabilityProgramManagementContext> options)
            : base(options)
        {
        }


        public DbSet<StaffMember> StaffMember { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<SustainabilityProgram> SustainabilityProgram { get; set; }

        public DbSet<ProjectSchedule> ProjectSchedule { get; set; }

        public DbSet<TrackingLog> TrackingLog { get; set; }

    }
}
