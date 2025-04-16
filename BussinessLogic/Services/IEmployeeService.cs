using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccination.BussinessLogic.DTOs.CustomerDTOs;
using Vaccination.BussinessLogic.DTOs.EmployeeDTOs;

namespace Vaccination.BussinessLogic.Services
{
    public interface IEmployeeService : IBaseService<EmployeeDTO>
    {
        public IEnumerable<EmployeeDTO> GetEmployeeDTOsNotDeleted();
        void DeleteNoValidateModel(EmployeeNoValidateDTO entity);
    }
}
