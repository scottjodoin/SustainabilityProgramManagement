using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SustainabilityProgramManagement.Models
{
    public class Program
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string Abbreviation { get; set; }

        public List<Project> Projects { get; set; }
        public List<StaffMember> Staff { get; set; }
    }
}
