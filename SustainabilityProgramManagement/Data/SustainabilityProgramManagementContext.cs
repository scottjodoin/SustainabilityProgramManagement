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

        public DbSet<SustainabilityProgramManagement.Models.StaffMember> StaffMember { get; set; }
    }
}
