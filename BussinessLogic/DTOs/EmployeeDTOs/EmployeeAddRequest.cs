using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccination.BussinessLogic.DTOs.VaccineTypeDTOs;
using Vaccination.DataAccess.Models;

namespace Vaccination.BussinessLogic.DTOs.EmployeeDTOs
{
    public class EmployeeAddRequest
    {
        public string Employee { get; set; }

        public IFormFile? Image { get; set; }

        public EmployeeDTO GetEmployeeDTO()
        {
            return JsonConvert.DeserializeObject<EmployeeDTO>(Employee);
        }
    }
}
