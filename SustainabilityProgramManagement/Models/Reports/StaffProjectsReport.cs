using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper.Configuration.Attributes;

namespace SustainabilityProgramManagement.Models.Reports
{
    public class StaffProjectsReport
    {
        public int? StaffMemberId { get; set; }
        public StaffMember StaffMember { get; set; }
        public int? ProjectId { get; set; }
        public Project Project { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TrackedHours { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal RemainingTime
        {
            get
            {
                return (TotalTime ?? 0) - (TrackedHours ?? 0);
            }
        }
    }
}
