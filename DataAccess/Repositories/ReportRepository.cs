using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vaccination.DataAccess.Models;

namespace Vaccination.DataAccess.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly VaccinationManagementContext _context;
        public ReportRepository(VaccinationManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<InjectionResult> GetInjectionResults(DateTime? fromDate, DateTime? toDate, string? prevention, int? vaccineTypeId)
        {
            var query = _context.InjectionResults
                                .Include(ir => ir.Vaccine)
                                .Include(ir => ir.Customer)
                                .AsQueryable();

            // Lọc theo ngày tiêm
            if (fromDate.HasValue)
                query = query.Where(ir => ir.InjectionDate >= fromDate.Value);
            if (toDate.HasValue)
                query = query.Where(ir => ir.InjectionDate <= toDate.Value);

            // Lọc theo phương pháp phòng ngừa
            if (!string.IsNullOrEmpty(prevention))
                query = query.Where(ir => ir.Prevention.Contains(prevention));

            // Lọc theo loại vắc xin
            if (vaccineTypeId.HasValue)
                query = query.Where(ir => ir.Vaccine.VaccineTypeId == vaccineTypeId);

            return query.ToList();
        }

        public IEnumerable<Customer> GetReportCustomers(DateTime? dateOfBirthFrom, DateTime? dateOfBirthTo, string? fullName, string? address)
        {
            var query = _context.Customers.AsQueryable();

            // Lọc theo DateOfBirthFrom
            if (dateOfBirthFrom.HasValue)
            {
                query = query.Where(c => c.DateOfBirth >= dateOfBirthFrom.Value);
            }

            // Lọc theo DateOfBirthTo
            if (dateOfBirthTo.HasValue)
            {
                query = query.Where(c => c.DateOfBirth <= dateOfBirthTo.Value);
            }

            // Lọc theo FullName nếu không null hoặc rỗng
            if (!string.IsNullOrEmpty(fullName))
            {
                query = query.Where(c => c.FullName.Contains(fullName));
            }

            // Lọc theo Address nếu không null hoặc rỗng
            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(c => c.Address.Contains(address));
            }

            return query.ToList();
        }
        public IEnumerable<Vaccine> GetReportVaccine(DateTime? injectionDateBegin, DateTime? injectionDateEnd, string? vaccineType, string? origin)
        {
            var query = _context.Vaccines.Include(c => c.VaccineType).AsQueryable();
            // Lọc theo DateOfBirthFrom
            if (injectionDateBegin.HasValue)
            {
                query = query.Where(c => c.NextBeginNextInjection >= injectionDateBegin.Value);
            }

            // Lọc theo DateOfBirthTo
            if (injectionDateEnd.HasValue)
            {
                query = query.Where(c => c.NextEndNextInjection <= injectionDateEnd.Value);
            }

            // Lọc theo FullName nếu không null hoặc rỗng
            if (!string.IsNullOrEmpty(vaccineType))
            {
                query = query.Where(c => c.VaccineType.VaccineTypeName.Contains(vaccineType));
            }

            // Lọc theo Address nếu không null hoặc rỗng
            if (!string.IsNullOrEmpty(origin))
            {
                query = query.Where(c => c.Origin.Contains(origin));
            }

            return query.ToList();
        }
         public IEnumerable<Customer> GetCustomersByYear(int year)
       {
            var customerIds = _context.InjectionResults
                               .Where(ir => ir.InjectionDate.HasValue && ir.InjectionDate.Value.Year == year)
                               .Select(ir => ir.CustomerId)
                               .Distinct()
                               .ToList();

            // Truy vấn các khách hàng dựa trên danh sách ID
            return _context.Customers
                           .Where(c => customerIds.Contains(c.Id))
                           .ToList();
        }
        public IEnumerable<InjectionResult> GetInjectionResultsByYear(int year)
        {
            return _context.InjectionResults
                           .Where(ir => ir.InjectionDate.HasValue && ir.InjectionDate.Value.Year == year)
                           .ToList();
        }
        public IEnumerable<Vaccine> GetVaccinesByYear(int year)
        {
            var vaccinesIds = _context.InjectionResults
                              .Where(ir => ir.InjectionDate.HasValue && ir.InjectionDate.Value.Year == year)
                              .Select(ir => ir.VaccineId)
                              .Distinct()
                              .ToList();

            return _context.Vaccines
                           .Where(c => vaccinesIds.Contains(c.Id))
                           .ToList();
        }
    }
}
