using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace SustainabilityProgramManagement.Models
{
    public class StaffMember
    {
        [Name("staffnumber")]
        public int StaffMemberId { get; set; }

        [Name("firstname")]
        public string FirstName { get; set; }
        [Name("lastname")]
        public string LastName { get; set; }

        [Name("programid")]
        public int? SustainabilityProgramId { get; set; }
        public SustainabilityProgram SustainabilityProgram { get; set; }

        public ICollection<TrackingLog> TrackingLogs { get; set; }
        public ICollection<ProjectSchedule> ProjectSchedules { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
