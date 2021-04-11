using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SustainabilityProgramManagement.Data;
using SustainabilityProgramManagement.Models;

namespace SustainabilityProgramManagement.Pages.Programs
{
    public class DetailsModel : PageModel
    {
        private readonly SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext _context;

        public DetailsModel(SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext context)
        {
            _context = context;
        }

        public SustainabilityProgram SustainabilityProgram { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SustainabilityProgram = await _context.SustainabilityProgram.FirstOrDefaultAsync(m => m.SustainabilityProgramId == id);

            if (SustainabilityProgram == null)
            {
                return NotFound();
            }

            ViewData["AssociatedStaffMembers"] =  await _context.StaffMember
                .Where(s => s.SustainabilityProgramId == id)
                .ToListAsync();

            ViewData["AssociatedProjects"] = await _context.Project
                .Where(p => p.SustainabilityProgramId == id)
                .ToListAsync();
            return Page();
        }
    }
}
