using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccination.DataAccess.Models;

namespace Vaccination.DataAccess.Repositories
{
    public interface IReportRepository
    {
        public IEnumerable<InjectionResult> GetInjectionResults(DateTime? fromDate, DateTime? toDate, string? prevention, int? vaccineTypeId);
        IEnumerable<Customer> GetReportCustomers(DateTime? dateOfBirthFrom, DateTime? dateOfBirthTo, string? fullName, string? address);

        IEnumerable<Vaccine> GetReportVaccine(DateTime? injectionDateBegin, DateTime? injectionDateEnd, string? vaccineType, string? origin);

        IEnumerable<Customer> GetCustomersByYear(int year);
        IEnumerable<InjectionResult> GetInjectionResultsByYear(int year);
        public IEnumerable<Vaccine> GetVaccinesByYear(int year);
    }
}

