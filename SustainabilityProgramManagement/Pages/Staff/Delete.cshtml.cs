//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using SustainabilityProgramManagement.Data;
//using SustainabilityProgramManagement.Models;

//namespace SustainabilityProgramManagement.Pages.Staff
//{
//    public class DeleteModel : PageModel
//    {
//        private readonly SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext _context;

//        public DeleteModel(SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext context)
//        {
//            _context = context;
//        }

//        [BindProperty]
//        public StaffMember StaffMember { get; set; }

//        public async Task<IActionResult> OnGetAsync(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            StaffMember = await _context.StaffMember
//                .Include(s => s.SustainabilityProgram).FirstOrDefaultAsync(m => m.StaffMemberId == id);

//            if (StaffMember == null)
//            {
//                return NotFound();
//            }
//            return Page();
//        }

//        public async Task<IActionResult> OnPostAsync(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            StaffMember = await _context.StaffMember.FindAsync(id);

//            if (StaffMember != null)
//            {
//                _context.StaffMember.Remove(StaffMember);
//                await _context.SaveChangesAsync();
//            }

//            return RedirectToPage("./Index");
//        }
//    }
//}
