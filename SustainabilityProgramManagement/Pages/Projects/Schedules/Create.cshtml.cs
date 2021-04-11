using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SustainabilityProgramManagement.Data;
using SustainabilityProgramManagement.Models;

namespace SustainabilityProgramManagement.Pages.Projects.Schedules
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
        ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectId");
        ViewData["StaffMemberId"] = new SelectList(_context.StaffMember, "StaffMemberId", "StaffMemberId");
            return Page();
        }

        [BindProperty]
        public ProjectSchedule ProjectSchedule { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProjectSchedule.Add(ProjectSchedule);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
