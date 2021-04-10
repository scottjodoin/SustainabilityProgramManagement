using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace SustainabilityProgramManagement.Models
{
    public class SustainabilityProgram
    {
        [Name("programid"), Key]
        public int SustainabilityProgramId { get; set; }
        [Name("ProgramName")]
        public string ProgramName { get; set; }
        [Name("Abbreviation")]
        public string Abbreviation { get; set; }

        public List<Project> Projects { get; set; }
        public List<StaffMember> Staff { get; set; }
    }
}
