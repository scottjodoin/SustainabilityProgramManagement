using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SustainabilityProgramManagement.Models
{
    public class Project
    {
        public int ID { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public Program Program { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public ICollection<TrackingLog> TrackingLogs { get; set; }
        public ICollection<StaffMember> Staff { get; set; }
    }
}
