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
    public class DeleteModel : PageModel
    {
        private readonly SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext _context;

        public DeleteModel(SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SustainabilityProgram = await _context.SustainabilityProgram.FindAsync(id);

            if (SustainabilityProgram != null)
            {
                _context.SustainabilityProgram.Remove(SustainabilityProgram);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
