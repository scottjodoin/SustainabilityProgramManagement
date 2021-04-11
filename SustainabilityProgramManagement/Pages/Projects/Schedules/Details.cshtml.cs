using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SustainabilityProgramManagement.Data;
using SustainabilityProgramManagement.Models;

namespace SustainabilityProgramManagement.Pages.Projects.Schedules
{
    public class DetailsModel : PageModel
    {
        private readonly SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext _context;

        public DetailsModel(SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext context)
        {
            _context = context;
        }

        public ProjectSchedule ProjectSchedule { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProjectSchedule = await _context.ProjectSchedule
                .Include(p => p.Project)
                .Include(p => p.StaffMember).FirstOrDefaultAsync(m => m.ProjectScheduleId == id);

            if (ProjectSchedule == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
