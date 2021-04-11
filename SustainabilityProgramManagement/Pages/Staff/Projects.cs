using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SustainabilityProgramManagement.Data;
using SustainabilityProgramManagement.Models;
using SustainabilityProgramManagement.Models.Reports;

namespace SustainabilityProgramManagement.Pages.Staff
{
    public class ProjectsModel : PageModel
    {
        private readonly ReportContext _context;

        public ProjectsModel(ReportContext context)
        {
            _context = context;
        }
        [BindProperty]
        public StaffMember StaffMember { get; set; }

        [BindProperty]
        public List<StaffProjectsReport> StaffProjectsReport { get; set; }
        public async Task<IActionResult> OnGetAsync(int? staffid)
        {
            if (staffid == null)
            {
                return NotFound();
            }
            // Get the StaffMember
            StaffMember = await _context.StaffMember
                .FirstOrDefaultAsync(m => m.StaffMemberId == staffid);

            if (StaffMember == null)
                return NotFound();

            // Collect the data for the reports
            StaffProjectsReport = await _context.StaffProjectsReport.FromSqlRaw(@"
            SELECT a.StaffMemberId AS StaffMemberId, a.ProjectId as ProjectId,
                    SUM(a.Hours) AS TrackedHours, c.Days*7.5 AS TotalTime, b.ProjectEndDate AS EndDate
                FROM dbo.TrackingLog a
                LEFT JOIN dbo.Project b
                    ON a.ProjectId=b.ProjectId
                LEFT JOIN dbo.ProjectSchedule c
                    ON a.ProjectId=c.ProjectId
                WHERE
                    a.StaffMemberId={0}
                GROUP BY a.StaffMemberId, a.ProjectId, c.Days, b.ProjectEndDate
            ", staffid)
                .Include(spr => spr.Project)
                    .ThenInclude(p => p.SustainabilityProgram)
                .OrderByDescending(spr => spr.EndDate)
                .ToListAsync();
            return Page();
        }
    }
}
