using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper.Configuration.Attributes;

namespace SustainabilityProgramManagement.Models
{
    public class TrackingLog
    {
        [Name("logid")]
        public int TrackingLogId { get; set; }
        
        [Name("staffnumber")]
        public int? StaffMemberId { get; set; }
        public StaffMember StaffMember { get; set; }
        
        [Name("projectid")]
        public int? ProjectId { get; set; }
        public Project Project { get; set; }


        [Name("hours")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Hours { get; set; }

        [DataType(DataType.Date), Name("date")]
        public DateTime Date { get; set; }
    }
}
