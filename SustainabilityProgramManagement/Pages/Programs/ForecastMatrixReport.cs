using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SustainabilityProgramManagement.Data;
using SustainabilityProgramManagement.Models;
using SustainabilityProgramManagement.Models.Reports;

namespace SustainabilityProgramManagement.Pages.Programs
{
    public class ForecastMatrixReportModel : PageModel
    {
        private readonly SustainabilityProgramManagement.Data.ReportContext _context;

        public ForecastMatrixReportModel(SustainabilityProgramManagement.Data.ReportContext context)
        {
            _context = context;
        }
        [BindProperty]
        public SustainabilityProgram Program1 { get; set; }
        [BindProperty]
        public SustainabilityProgram Program2 { get; set; }
        [BindProperty]
        public List<ForecastReportRow> ForecastReport1 { get; set; }
        [BindProperty]
        public List<ForecastReportRow> ForecastReport2 { get; set; }


        public async Task<IActionResult> OnGetAsync(int?[] programIds)
        {
            if (programIds == null || programIds.Length != 2)
            {
                return NotFound();
            }
            // Calculate program ids
            int programId1 = programIds[0] ?? 0;
            int programId2 = programIds[1] ?? 0;


            // Gather SustainabilityPrograms

            Program1 = await _context.SustainabilityProgram
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(s => s.SustainabilityProgramId == programId1);
            Program2 = await _context.SustainabilityProgram
                .Include(s => s.Staff)
                .FirstOrDefaultAsync(s => s.SustainabilityProgramId == programId2);

            if (Program1 == null || Program2 == null)
            {
                return NotFound();
            }

            // Get the range of TrackingLog records

            DateTime firstTrackedMonth = FirstOfMonth(_context.TrackingLog.Min(t => t.Date));
            DateTime lastTrackedDate = _context.TrackingLog.Max(t => t.Date);
            DateTime lastTrackedMonth = FirstOfMonth(lastTrackedDate);
            DateTime lastProjectEndDate = FetchLastEndDateOfPrograms(programId1, programId2).AddDays(-1);
            int forecastMonths = 8;

            // Extend forecast to include all projects, if necessary
            DateTime lastDisplayMonth = lastTrackedMonth.AddMonths(forecastMonths);
            if (lastDisplayMonth < lastProjectEndDate)
            {
                lastDisplayMonth = FirstOfMonth(lastProjectEndDate);
                forecastMonths = DifferenceInMonths(lastDisplayMonth, lastTrackedMonth);
            }
            
            // Initalize monthly buckets and some view header data
            var monthArray = InitializeEmptyMonthlyHours(firstTrackedMonth, lastDisplayMonth).Keys;
            ViewData["MonthArray"] = monthArray;
            ViewData["TrackedMonthsCount"] = DifferenceInMonths(lastTrackedMonth, firstTrackedMonth);
            ViewData["ForecastMonthsCount"] = monthArray.Count - forecastMonths;
            ViewData["MostRecentTrackedDate"] = lastTrackedDate;

            // populate the rows
            ForecastReport1 = GetProgramForecastMatrixRow(Program1, firstTrackedMonth, lastTrackedMonth, forecastMonths);
            ForecastReport1 = ForecastReport1 ?? new List<ForecastReportRow>();
            ForecastReport2 = GetProgramForecastMatrixRow(Program2, firstTrackedMonth, lastTrackedMonth, forecastMonths);
            ForecastReport2 = ForecastReport2 ?? new List<ForecastReportRow>();

            return Page();
        }

        private int DifferenceInMonths(DateTime date1, DateTime date2)
        {
            return ((date1.Year - date2.Year) * 12) + date1.Month - date2.Month;
        }

        private DateTime FetchLastEndDateOfPrograms(int programId1, int programId2)
        {
            return _context.ProjectSchedule
                .Include(p => p.StaffMember)
                .Include(p => p.Project)
                .Where(p =>
                   (p.StaffMember.SustainabilityProgramId == programId1 ||
                   p.StaffMember.SustainabilityProgramId == programId2) &&
                   (p.Project.SustainabilityProgramId == programId1 ||
                   p.Project.SustainabilityProgramId == programId2)
                ).Max(p => p.Project.ProjectEndDate);
        }

        private DateTime FirstOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        private List<ForecastReportRow> GetProgramForecastMatrixRow(SustainabilityProgram program, DateTime firstMonth, DateTime lastMonth, int forecastMonths)
        {
            List<ForecastReportRow> rows = new List<ForecastReportRow>();


            foreach (StaffMember staff in program.Staff)
            {
                Dictionary<DateTime, decimal> monthlyHours = InitializeEmptyMonthlyHours(firstMonth, lastMonth.AddMonths(forecastMonths));
                var staffProjectSchedules = FetchAllStaffProjectSchedules(staff.StaffMemberId)
                    .Where(p => p.Project.SustainabilityProgramId == program.SustainabilityProgramId);

                // get staff project rows
                List<Dictionary<DateTime,decimal>> staffProjectMonthyHourRows = FetchStaffProjectMonthlyHourRows(monthlyHours,staffProjectSchedules);

                // sum up months
                List<decimal> hours = CalculateSumOfAllMonthlyHourRows(staffProjectMonthyHourRows).Values.ToList();
                ForecastReportRow row = new ForecastReportRow();
                rows.Add(new ForecastReportRow {
                    SustainabilityProgram = program,
                    StaffMember = staff,
                    HoursList=hours
                });
            }
            return rows;
        }
        public class ForecastReportRow
        {
            public SustainabilityProgram SustainabilityProgram { get; set; }
            public StaffMember StaffMember { get; set; }
            public List<decimal> HoursList { get; internal set; }
        }

        /// <summary>
        /// Initializes an empty dictionary with month keys containing values of the hours worked.
        /// </summary>
        /// <param name="firstMonth">The first month</param>
        /// <param name="lastMonth">The last month, inclusive</param>
        /// <returns>Initialized for every month with 0 hours</returns>
        private Dictionary<DateTime, decimal> InitializeEmptyMonthlyHours(DateTime firstMonth, DateTime lastMonth)
        {
            firstMonth = FirstOfMonth(firstMonth);
            lastMonth = FirstOfMonth(lastMonth);

            Dictionary<DateTime, decimal> monthlyHours = new Dictionary<DateTime, decimal>();
            DateTime currentMonth = firstMonth;
            while (currentMonth <= lastMonth)
            {
                monthlyHours.Add(currentMonth, 0M);
                currentMonth = currentMonth.AddMonths(1);
            }
            return monthlyHours;
        }

        private Dictionary<DateTime, decimal> InitializeEmptyMonthlyHours(Dictionary<DateTime, decimal> source) {
            DateTime firstMonth = FirstOfMonth(source.Keys.Min());
            DateTime lastMonth = FirstOfMonth(source.Keys.Max());
            Dictionary<DateTime, decimal> emptyMonthlyHours = InitializeEmptyMonthlyHours(
              firstMonth,
              lastMonth
              );
            return emptyMonthlyHours;
        }
        private IQueryable<ProjectSchedule> FetchAllStaffProjectSchedules(int staffMemberId)
        {
            return _context.ProjectSchedule
                .Where(p => p.StaffMemberId == staffMemberId)
                .Include(p => p.Project);
        }

        private List<Dictionary<DateTime, decimal>> FetchStaffProjectMonthlyHourRows(Dictionary<DateTime, decimal> monthlyHours, IQueryable<ProjectSchedule> staffProjectSchedules)
        {
            List<Dictionary<DateTime, decimal>> staffProjectMonthyHourRows = new List<Dictionary<DateTime, decimal>>();
            var query = staffProjectSchedules
                .Include(p => p.StaffMember)
                .Include(p => p.Project)
                .AsEnumerable();
            foreach (var ps in 
                query.GroupBy(p => p.ProjectId))
            {
                ProjectSchedule projectSchedule = ps.First(); // usually only one project schedule applied between staff and project
                StaffMember staff = projectSchedule.StaffMember;
                Dictionary<DateTime, decimal> trackedHoursByMonth = FetchTrackedHoursByMonth(projectSchedule);

                Dictionary<DateTime, decimal> forecastedHoursByMonth = ForecastHoursByMonthLinear(projectSchedule, trackedHoursByMonth, monthlyHours);

                Dictionary<DateTime, decimal> projectHoursByMonth = CalculateSumOfAllMonthlyHourRows(new List<Dictionary<DateTime, decimal>>(){
                    trackedHoursByMonth,
                    forecastedHoursByMonth });

                staffProjectMonthyHourRows.Add(projectHoursByMonth);
            }
            return staffProjectMonthyHourRows;
        }

        /// <summary>
        /// Return the sums of each month for this project schedule
        /// </summary>
        /// <param name="projectSchedule"></param>
        /// <returns>A dictionary containing the months and the total hours worked in those months.</returns>
        private Dictionary<DateTime, decimal> FetchTrackedHoursByMonth(ProjectSchedule projectSchedule)
        {
            Dictionary<DateTime, decimal> trackedHoursByMonth = new Dictionary<DateTime, decimal>();
            var trackingLogByMonth = _context.TrackingLog
                .Where(t => t.StaffMemberId == projectSchedule.StaffMemberId)
                .Where(t => t.ProjectId == projectSchedule.ProjectId)
                .AsEnumerable()
                .GroupBy(t => FirstOfMonth(t.Date));
            foreach (var month in trackingLogByMonth)
            {
                trackedHoursByMonth.Add(
                    month.Key,
                    month.Sum(m => m.Hours)
                    );
            }
            return trackedHoursByMonth;
       }

        /// <summary>
        /// Performs a daily linear interpolation based on the remaining number of hours
        /// Could add a type that adds different types of search
        /// </summary>
        /// <param name="projectSchedule">The project schedule from which to gather the end date</param>
        /// <param name="trackedHoursByMonth">A dictionary containing the total number of hours worked on this project by month</param>
        /// <param name="monthlyHours">Reference for the start and end months</param>
        /// <returns>Months with the number of forecasted hours to add.</returns>
        private Dictionary<DateTime, decimal> ForecastHoursByMonthLinear(ProjectSchedule projectSchedule, Dictionary<DateTime, decimal> trackedHoursByMonth, Dictionary<DateTime, decimal> monthlyHours)
        {
            // clone the monthly hours
            Dictionary<DateTime, decimal> forecastHoursByMonth = InitializeEmptyMonthlyHours(monthlyHours);

            DateTime startForecastDate = FirstOfMonth(trackedHoursByMonth.Keys.Max()).AddMonths(1);
            DateTime endProjectDate = FirstOfMonth(projectSchedule.Project.ProjectEndDate).AddMonths(1).AddDays(-1); // need to subtract one day to avoid the due date.
            decimal delta = (endProjectDate - startForecastDate).Days;
            if (delta <= 0) return forecastHoursByMonth; // project is already done!

            decimal totalTrackedHours = trackedHoursByMonth.Values.Sum();
            decimal hoursPerDay = 7.5M;
            decimal totalScheduledHours = projectSchedule.Days * hoursPerDay;
            decimal remainingWork = totalScheduledHours - totalTrackedHours;

            if (remainingWork <= 0M) return forecastHoursByMonth; // project is already overdue!

            decimal dailyWork = remainingWork / delta;
            DateTime currentDay = startForecastDate;
            int i = 0;
            while (i <= delta)
            {
                // could include logic to avoid weeknds etc.

                // add the little bit to the monthly value
                DateTime key = FirstOfMonth(currentDay);
                if (forecastHoursByMonth.ContainsKey(key))
                    forecastHoursByMonth[key] = forecastHoursByMonth[key] + dailyWork;
                else
                    break;
                
                currentDay = currentDay.AddDays(1);
                i++;
            }

            return forecastHoursByMonth;
        }
        private Dictionary<DateTime, decimal> CalculateSumOfAllMonthlyHourRows(List<Dictionary<DateTime, decimal>> monthlyHourRows)
        {
            Dictionary<DateTime, decimal> sum = new Dictionary<DateTime, decimal>();
            foreach (var row in monthlyHourRows)
                foreach (var d in row)
                    sum[d.Key] = (sum.ContainsKey(d.Key)) ? sum[d.Key] + d.Value : d.Value;
            return sum;
        }
    }
}
