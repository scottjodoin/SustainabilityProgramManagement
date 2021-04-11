using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SustainabilityProgramManagement.Models.Reports;


namespace SustainabilityProgramManagement.Data
{
    public class ReportContext : SustainabilityProgramManagementContext
    {
        public ReportContext (DbContextOptions<SustainabilityProgramManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<StaffProjectsReport> StaffProjectsReport { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Necessary, since our model isnt a EF model
            modelBuilder.Entity<StaffProjectsReport>(entity =>
            {
                entity.HasNoKey();
            });
        }
    }
}
