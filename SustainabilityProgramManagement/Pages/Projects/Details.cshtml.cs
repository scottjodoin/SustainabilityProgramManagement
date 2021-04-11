using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SustainabilityProgramManagement.Data;
using SustainabilityProgramManagement.Models;

namespace SustainabilityProgramManagement.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext _context;

        public DetailsModel(SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext context)
        {
            _context = context;
        }

        public Project Project { get; set; }
        [BindProperty]
        public List<StaffMember> Staff { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Project
                .Include(p => p.SustainabilityProgram).FirstOrDefaultAsync(m => m.ProjectId == id);

            if (Project == null)
            {
                return NotFound();
            }

            // Unfortunately EF6 does not yet have robust support for many-to-many relationships
            Staff = await _context.StaffMember
                .FromSqlRaw(@"
                SELECT DISTINCT c.* FROM dbo.TrackingLog a
                    LEFT JOIN dbo.Project b
                        ON a.ProjectId=b.ProjectId
                    LEFT JOIN dbo.StaffMember c
                        ON a.StaffMemberId=c.StaffMemberId
                    WHERE a.ProjectId={0};", id).ToListAsync();


            ViewData["SustainabilityProgramId"] = Project.SustainabilityProgramId;

            return Page();
        }
    }
}
