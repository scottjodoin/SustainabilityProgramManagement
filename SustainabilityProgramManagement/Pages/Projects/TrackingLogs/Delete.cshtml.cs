using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SustainabilityProgramManagement.Data;
using SustainabilityProgramManagement.Models;

namespace SustainabilityProgramManagement.Pages.Projects.TrackingLogs
{
    public class DeleteModel : PageModel
    {
        private readonly SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext _context;

        public DeleteModel(SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TrackingLog = await _context.TrackingLog.FindAsync(id);

            if (TrackingLog != null)
            {
                _context.TrackingLog.Remove(TrackingLog);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
