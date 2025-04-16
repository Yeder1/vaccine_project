using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccination.BussinessLogic.DTOs.ReportDTOs
{
    public class ReportVaccineDTOs
    {
        public int No { get; set; }
        public string? VaccineName { get; set; }
        public string? VaccineType { get; set; }
        public DateTime? BeginNextInjectDate { get; set; }
        public DateTime? EndNextInjectDate { get; set; }
        public string? Origin { get; set; }
        public int NumOfInject { get; set; }
    }
}
