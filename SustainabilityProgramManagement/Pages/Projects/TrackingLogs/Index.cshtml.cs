using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SustainabilityProgramManagement.Data;
using SustainabilityProgramManagement.Models;

namespace SustainabilityProgramManagement.Pages.Projects.TrackingLogs
{
    public class IndexModel : PageModel
    {
        private readonly SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext _context;

        public IndexModel(SustainabilityProgramManagement.Data.SustainabilityProgramManagementContext context)
        {
            _context = context;
        }

        public IList<TrackingLog> TrackingLog{ get;set; }


        public async Task OnGetAsync([FromQuery(Name = "page")] int? page)
        {
            // Paginate this long list

            int pageNum = page ?? 0;
            int rowCount = 50;
            TrackingLog = await _context.TrackingLog
                .OrderByDescending(t => t.ProjectId)
                .Include(t => t.Project)
                .Include(t => t.StaffMember)
                .Skip(pageNum * rowCount)
                .Take(rowCount)
                .ToListAsync();
            ViewData["StartIndex"] = pageNum * rowCount + 1;
        }
    }
}
