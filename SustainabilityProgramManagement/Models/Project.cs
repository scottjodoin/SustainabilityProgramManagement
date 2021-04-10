using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper.Configuration.Attributes;

namespace SustainabilityProgramManagement.Models
{
    public class Project
    {

        [Name("projectid")]
        public int ProjectId { get; set; }
        [Name("projectcode")]
        public string ProjectCode { get; set; }
        [Name("projectname")]
        public string ProjectName { get; set; }

        [DataType(DataType.Date),Name("projectenddate")]
        public DateTime ProjectEndDate { get; set; }

        [Name("ProgramID")]
        public int? SustainabilityProgramId { get; set; }
        public SustainabilityProgram SustainabilityProgram { get; set; }


        public ICollection<TrackingLog> TrackingLogs { get; set; }
        public ICollection<ProjectSchedule> ProjectSchedules { get; set; }
    }
}
