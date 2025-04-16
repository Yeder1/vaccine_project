using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccination.BussinessLogic.DTOs.ReportDTOs
{
    public class ReportInjectionDTO
    {
        public int? No { get; set; }
        public string? VaccineName { get; set; }
        public string? Prevention { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? InjectDate { get; set; }
        public int? NumberOfInject { get; set; }
    }
}
