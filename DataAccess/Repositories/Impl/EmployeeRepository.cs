using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccination.DataAccess.Models;

namespace Vaccination.DataAccess.Repositories.Impl
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly VaccinationManagementContext _context;

        public EmployeeRepository(VaccinationManagementContext context) : base(context)
        {
            _context = context;
        }
    }
}
