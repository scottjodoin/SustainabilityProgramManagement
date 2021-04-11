using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper.Configuration.Attributes;

namespace SustainabilityProgramManagement.Models.Reports
{
    public class ForecastMatrixReport
    {
        public int? Program1Id { get; set; }
        public Program Program1 { get; set; }

        public int? Program2Id { get; set; }
        public Program Program2 { get; set; }


    }
}
