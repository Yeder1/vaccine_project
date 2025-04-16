using Microsoft.AspNetCore.Mvc;
using Vaccination.BussinessLogic.Commons.Constants;
using Vaccination.BussinessLogic.DTOs.InjectionResultDTO;
using Vaccination.BussinessLogic.Services;

namespace Vaccination.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InjectionResultController : ControllerBase
    {
        private readonly IInjectionResultService _injectionResultService;

        public InjectionResultController(IInjectionResultService injectionResultService)
        {
            _injectionResultService = injectionResultService;
        }



        [HttpPost]
        public IActionResult Add([FromBody] InjectionResultVaccineRequest injectionResultDTO)
        {
            try
            {
                _injectionResultService.AddRequest(injectionResultDTO);
                return Ok(new { message = InjectionResultMessages.InjectionResultAdded });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] InjectionResultDTO injectionResultDTO)
        {
            try
            {
                _injectionResultService.Delete(injectionResultDTO);
                return Ok(new { message = InjectionResultMessages.InjectionResultDeleted });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("DeleteMany")]
        public IActionResult DeleteMany([FromBody] List<InjectionResultDTO> injectionResultDTOs)
        {
            try
            {
                _injectionResultService.DeleteMany(injectionResultDTOs);
                return Ok(new { message = InjectionResultMessages.InjectionResultsDeleted });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var results = _injectionResultService.GetAll();
                return Ok(new { message = InjectionResultMessages.InjectionResultsRetrieved, data = results });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _injectionResultService.GetById(id);
                if (result == null)
                {
                    return NotFound(new { message = InjectionResultMessages.InjectionResultNotFound });
                }
                return Ok(new { message = InjectionResultMessages.InjectionResultRetrieved, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("get-all-vaccine-type")]
        public IActionResult GetAllVaccineType()
        {
            try
            {
                var result = _injectionResultService.GetAllVaccineType();
                if (result == null)
                {
                    return NotFound(new { message = InjectionResultMessages.VaccineTypesNotFound });
                }
                return Ok(new { message = InjectionResultMessages.VaccineTypesRetrieved, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] InjectionResultVaccineRequest injectionResultDTO)
        {
            try
            {
                _injectionResultService.UpdateRequest(injectionResultDTO);
                return Ok(new { message = InjectionResultMessages.InjectionResultUpdated });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("get-all-vaccine-customer")]
        public IActionResult GetAllVaccineCustomer()
        {
            try
            {
                var result = _injectionResultService.GetAllVaccineCustomer();
                if (result == null)
                {
                    return NotFound(new { message = InjectionResultMessages.VaccineCustomersNotFound });
                }
                return Ok(new { message = InjectionResultMessages.VaccineCustomersRetrieved, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string? keyword)
        {
            try
            {
                var result = _injectionResultService.Search(keyword);
                if (result == null || !result.Any())
                {
                    return NotFound(new { message = InjectionResultMessages.VaccineCustomersNotFound });
                }
                return Ok(new { message = InjectionResultMessages.VaccineCustomersRetrieved, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
