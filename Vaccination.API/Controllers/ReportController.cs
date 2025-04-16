using Microsoft.AspNetCore.Mvc;
using System.Net;
using Vaccination.BussinessLogic.Services;
using Vaccination.BussinessLogic.Services.Impl;

namespace Vaccination.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _injectionResultService;

        public ReportController(IReportService injectionResultService)
        {
            _injectionResultService = injectionResultService;
        }

        [HttpGet("ReportInjection")]
        public IActionResult GetInjectionResults([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] string? prevention, [FromQuery] int? vaccineTypeId)
        {
            var results = _injectionResultService.GetInjectionResults(fromDate, toDate, prevention, vaccineTypeId);
            return Ok(results);
        }

        [HttpGet("ReportCustomer")]
        public IActionResult GetReportCustomers([FromQuery] DateTime? dateOfBirthFrom,
        [FromQuery] DateTime? dateOfBirthTo,
        [FromQuery] string? fullName,
        [FromQuery] string? address)
        {
            var reportCustomers = _injectionResultService.GetReportCustomers(dateOfBirthFrom, dateOfBirthTo, fullName, address);
            return Ok(reportCustomers);
        }
        [HttpGet("ReportVaccine")]
        public IActionResult GetReportVaccines([FromQuery] DateTime? injectionDateBegin,
       [FromQuery] DateTime? injectionDateEnd,
       [FromQuery] string? vaccineType,
       [FromQuery] string? origin)
        {
            var reportVaccines = _injectionResultService.GetReportVaccine(injectionDateBegin, injectionDateEnd, vaccineType, origin);
            return Ok(reportVaccines);
        }
        [HttpGet("ReportCustomerByYear")]
        public IActionResult GetReportCustomerByYear([FromQuery] int year)
        {
            var report = _injectionResultService.GetReportCustomerByYear(year);
            return Ok(report);
        }
        [HttpGet("ReportInjectionByYear")]
        public IActionResult ReportInjectionByYear(int year)
        {
            var report = _injectionResultService.GetReportInjectionResultByYear(year);
            return Ok(report);
        }
        [HttpGet("GetVaccineReportByYear")]
        public IActionResult GetVaccineReportByYear(int year)
        {
            var report = _injectionResultService.GetReportVaccineByYear(year);
            return Ok(report);
        }
    }
}
