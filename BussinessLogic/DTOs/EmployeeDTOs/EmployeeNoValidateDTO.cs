using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccination.BussinessLogic.DTOs.EmployeeDTOs
{
    public class EmployeeNoValidateDTO
    {
        public int Id { get; set; } 

        public string? EmployeeName { get; set; }

        public bool? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? WorkingPlace { get; set; }

        public string? Position { get; set; }

        public string? Image { get; set; }
    }
}
