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

namespace SustainabilityProgramManagement.Pages.Projects.TrackingLogs
{
    public class EditModel : PageModel
    {
        private readonly SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext _context;

        public EditModel(SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TrackingLog TrackingLog { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TrackingLog = await _context.TrackingLog
                .Include(t => t.Project)
                .Include(t => t.StaffMember).FirstOrDefaultAsync(m => m.TrackingLogId == id);

            if (TrackingLog == null)
            {
                return NotFound();
            }
           ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectName");
           ViewData["StaffMemberId"] = new SelectList(_context.StaffMember, "StaffMemberId", "FullName");
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

            _context.Attach(TrackingLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackingLogExists(TrackingLog.TrackingLogId))
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

        private bool TrackingLogExists(int id)
        {
            return _context.TrackingLog.Any(e => e.TrackingLogId == id);
        }
    }
}
