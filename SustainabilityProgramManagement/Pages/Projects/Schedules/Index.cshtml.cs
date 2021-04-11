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
    public class IndexModel : PageModel
    {
        private readonly SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext _context;

        public IndexModel(SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext context)
        {
            _context = context;
        }

        public IList<ProjectSchedule> ProjectSchedule { get; set; }
        [BindProperty]
        public IEnumerable<IGrouping<int?, ProjectSchedule>> ProjectScheduleGroups { get; set; }
        public int PSGroups { get; set; }
        public async Task OnGetAsync()
        {
            ProjectSchedule = await _context.ProjectSchedule
                .OrderByDescending(p=>p.ProjectId)
                .Include(p => p.Project)
                .Include(p => p.StaffMember).ToListAsync();
             ProjectScheduleGroups = ProjectSchedule.GroupBy(p => p.ProjectId);
        }
    }
}
