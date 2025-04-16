using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccination.BussinessLogic.DTOs.ReportDTOs;
using Vaccination.DataAccess.Models;

namespace Vaccination.BussinessLogic.Services
{
    public interface IReportService
    {
        public IEnumerable<ReportInjectionDTO> GetInjectionResults(DateTime? fromDate, DateTime? toDate, string? prevention, int? vaccineTypeId);
        IEnumerable<ReportCustomerDTO> GetReportCustomers(DateTime? dateOfBirthFrom, DateTime? dateOfBirthTo, string? fullName, string? address);
        IEnumerable<ReportVaccineDTOs> GetReportVaccine(DateTime? injectionDateBegin, DateTime? injectionDateEnd, string? vaccineType, string? origin);

        IEnumerable<ReportCustomerChartDTO> GetReportCustomerByYear(int year);
        public IEnumerable<ReportInjectionChartDTO> GetReportInjectionResultByYear(int year);
        public IEnumerable<ReportVaccineChartDTO> GetReportVaccineByYear(int year);
    }
}
