using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccination.BussinessLogic.DTOs.InjectionResultDTO;
using Vaccination.BussinessLogic.DTOs.ReportDTOs;
using Vaccination.DataAccess.Models;
using Vaccination.DataAccess.Repositories;
using Vaccination.DataAccess.Repositories.VaccineRepository;

namespace Vaccination.BussinessLogic.Services.Impl
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _injectionResultRepository;
        private readonly IMapper _mapper;
        private readonly VaccinationManagementContext _context;

        public ReportService(IReportRepository injectionResultRepository, IMapper mapper, VaccinationManagementContext context)
        {
            _injectionResultRepository = injectionResultRepository;
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<ReportInjectionDTO> GetInjectionResults(DateTime? fromDate, DateTime? toDate, string? prevention, int? vaccineTypeId)
        {
            var injectionResults = _injectionResultRepository.GetInjectionResults(fromDate, toDate, prevention, vaccineTypeId);

            var resultDTOs = injectionResults.Select(ir => new ReportInjectionDTO
            {
                No = ir.Id,
                VaccineName = ir.Vaccine.VaccineName,
                Prevention = ir.Prevention,
                CustomerName = ir.Customer.FullName,
                InjectDate = ir.InjectionDate.Value,
                NumberOfInject = ir.NumberOfInjection
            }).ToList();

            return resultDTOs;
        }

        public IEnumerable<ReportCustomerDTO> GetReportCustomers(DateTime? dateOfBirthFrom, DateTime? dateOfBirthTo, string? fullName, string? address)
        {
            var customers = _injectionResultRepository.GetReportCustomers(dateOfBirthFrom, dateOfBirthTo, fullName, address);

            // Ánh xạ Customer sang ReportCustomerDTO và tính toán số lần tiêm
            var customerReport = customers.Select(customer => new ReportCustomerDTO
            {
                FullName = customer.FullName,
                DOB = customer.DateOfBirth,
                Address = customer.Address,
                IdentityCard = customer.IdentityCard,
                NumberOfInject = _context.InjectionResults.Count(ir => ir.CustomerId == customer.Id)
            });

            return customerReport.ToList();
        }

        public IEnumerable<ReportVaccineDTOs> GetReportVaccine(DateTime? injectionDateBegin, DateTime? injectionDateEnd, string? vaccineType, string? origin)
        {
            var vaccines = _injectionResultRepository.GetReportVaccine(injectionDateBegin, injectionDateEnd, vaccineType, origin);

           
            var vaccineReport = vaccines.Select(vaccines => new ReportVaccineDTOs
            {
                VaccineName = vaccines.VaccineName,
                VaccineType = vaccines.VaccineType.VaccineTypeName,
                BeginNextInjectDate = vaccines.NextBeginNextInjection,
                EndNextInjectDate = vaccines.NextEndNextInjection,
                Origin = vaccines.Origin,
                NumOfInject = _context.InjectionResults.Count(ir => ir.VaccineId == vaccines.Id)
            });

            return vaccineReport.ToList();
        }
        public IEnumerable<ReportCustomerChartDTO> GetReportCustomerByYear(int year)
        {
            // Lấy danh sách khách hàng đã tiêm trong năm (chỉ trả về Customer từ repository)
            var customers = _injectionResultRepository.GetCustomersByYear(year);

            // Lấy danh sách InjectionResults từ context dựa trên danh sách CustomerId đã lấy được
            var customerIds = customers.Select(c => c.Id).ToList();
            var injectionsInYear = _context.InjectionResults
                                           .Where(ir => customerIds.Contains(ir.CustomerId) && ir.InjectionDate.HasValue && ir.InjectionDate.Value.Year == year)
                                           .ToList();

            // Nhóm InjectionResults theo tháng và đếm số lượng khách hàng
            var report = injectionsInYear
                .GroupBy(ir => ir.InjectionDate.Value.Month)
                .Select(g => new ReportCustomerChartDTO
                {
                    Month = g.Key,
                    NumberOfCustomers = g.Select(ir => ir.CustomerId).Distinct().Count()
                })
                .ToList();

            // Đảm bảo có dữ liệu cho tất cả các tháng
            var reportWithAllMonths = Enumerable.Range(1, 12)
                .Select(month => report.FirstOrDefault(r => r.Month == month) ?? new ReportCustomerChartDTO { Month = month, NumberOfCustomers = 0 })
                .OrderBy(r => r.Month)
                .ToList();

            return reportWithAllMonths;
        }
        public IEnumerable<ReportInjectionChartDTO> GetReportInjectionResultByYear(int year)
        {
            // Lấy danh sách InjectionResult của năm
            var injectionResults = _injectionResultRepository.GetInjectionResultsByYear(year);

            // Nhóm InjectionResults theo tháng và đếm số lượng kết quả tiêm cho mỗi tháng
            var report = injectionResults
                .GroupBy(ir => ir.InjectionDate.Value.Month)
                .Select(g => new ReportInjectionChartDTO
                {
                    Month = g.Key,
                    NumberOfInjections = g.Count() // Số lượng kết quả tiêm trong tháng
                })
                .ToList();

            // Đảm bảo có dữ liệu cho tất cả các tháng
            var reportWithAllMonths = Enumerable.Range(1, 12)
                .Select(month => report.FirstOrDefault(r => r.Month == month) ?? new ReportInjectionChartDTO { Month = month, NumberOfInjections = 0 })
                .OrderBy(r => r.Month)
                .ToList();

            return reportWithAllMonths;
        }
        public IEnumerable<ReportVaccineChartDTO> GetReportVaccineByYear(int year)
        {
            // Lấy danh sách vaccine đã tiêm trong năm
            var vaccines = _injectionResultRepository.GetVaccinesByYear(year);

            // Lấy danh sách InjectionResults từ context dựa trên VaccineId đã lọc được
            var vaccineIds = vaccines.Select(v => v.Id).ToList();
            var injectionsInYear = _context.InjectionResults
                                           .Where(ir => vaccineIds.Contains(ir.VaccineId) && ir.InjectionDate.HasValue && ir.InjectionDate.Value.Year == year)
                                           .ToList();

            // Nhóm InjectionResults theo tháng và đếm số lượng vaccine cho mỗi tháng
            var report = injectionsInYear
                .GroupBy(ir => ir.InjectionDate.Value.Month)
                .Select(g => new ReportVaccineChartDTO
                {
                    Month = g.Key,
                    NumberOfVaccines = g.Count() // Số lần tiêm vaccine trong tháng
                })
                .ToList();

            // Đảm bảo có dữ liệu cho tất cả các tháng
            var reportWithAllMonths = Enumerable.Range(1, 12)
                .Select(month => report.FirstOrDefault(r => r.Month == month) ?? new ReportVaccineChartDTO { Month = month, NumberOfVaccines = 0 })
                .OrderBy(r => r.Month)
                .ToList();

            return reportWithAllMonths;
        }
    }
}
