using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SustainabilityProgramManagement.Data;
using SustainabilityProgramManagement.Models;

namespace SustainabilityProgramManagement.Pages.Projects.TrackingLogs
{
    public class CreateModel : PageModel
    {
        private readonly SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext _context;

        public CreateModel(SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectName");
        ViewData["StaffMemberId"] = new SelectList(_context.StaffMember, "StaffMemberId", "FullName");
            return Page();
        }

        [BindProperty]
        public TrackingLog TrackingLog { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TrackingLog.Add(TrackingLog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
