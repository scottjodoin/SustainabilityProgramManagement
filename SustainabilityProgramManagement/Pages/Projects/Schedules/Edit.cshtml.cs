using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SustainabilityProgramManagement.Data;
using SustainabilityProgramManagement.Models;

namespace SustainabilityProgramManagement.Pages.Projects.Schedules
{
    public class EditModel : PageModel
    {
        private readonly SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext _context;

        public EditModel(SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectId");
           ViewData["StaffMemberId"] = new SelectList(_context.StaffMember, "StaffMemberId", "StaffMemberId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProjectSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectScheduleExists(ProjectSchedule.ProjectScheduleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProjectScheduleExists(int id)
        {
            return _context.ProjectSchedule.Any(e => e.ProjectScheduleId == id);
        }
    }
}
