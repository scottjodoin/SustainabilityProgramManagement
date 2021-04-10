using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SustainabilityProgramManagement.Models
{
    public class StaffMember
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Program Program { get; set; }

        public ICollection<TrackingLog> TrackingLogs { get; set; }
        public ICollection<ProjectSchedule> ProjectSchedules { get; set; }
    }
}
