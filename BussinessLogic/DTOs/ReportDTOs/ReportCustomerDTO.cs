using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccination.BussinessLogic.DTOs.ReportDTOs
{
    public class ReportCustomerDTO
    {
        public int No { get; set; }
        public string? FullName { get; set; }
        public DateTime? DOB { get; set; }
        public string? Address { get; set; }
        public string? IdentityCard { get; set; }
        public int NumberOfInject { get; set; }
    }
}
