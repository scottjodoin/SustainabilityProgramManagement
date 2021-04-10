using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SustainabilityProgramManagement.Models
{
    public class TrackingLog
    {
        public int ID { get; set; }
        public StaffMember StaffMember { get; set; }
        public Project Project { get; set; }
        public int Hours { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
