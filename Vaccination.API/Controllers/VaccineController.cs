using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Vaccination.BussinessLogic.Commons.Constants;
using Vaccination.BussinessLogic.DTOs.VaccineDTOs;
using Vaccination.BussinessLogic.Services;

namespace Vaccination.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : Controller
    {
        private readonly IVaccineService _vaccineService;
        private readonly IVaccineTypeService _vaccineTypeService;


        public VaccineController(IVaccineService vaccineService, IVaccineTypeService vaccineTypeService)
        {
            _vaccineService = vaccineService;
            _vaccineTypeService = vaccineTypeService;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var vaccines = _vaccineService.GetAll();
            if (!vaccines.Any())
            {
                return NotFound("No vaccines found.");
            }
            return Ok(vaccines);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var vaccine = _vaccineService.GetById(Id);
            if (vaccine == null)
            {
                return NotFound("Vaccine not found");
            }

            return Ok(vaccine);
        }

        [HttpPost("Add")]
        public IActionResult AddVaccine(VaccineDTO vaccineDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int vaccineTypeId = vaccineDTO.VaccineTypeId;
            var vaccineTypeDTO = _vaccineTypeService.GetById(vaccineTypeId);

            if (vaccineTypeDTO == null) return NotFound("vaccine type invalid");

            vaccineDTO.VaccineTypeDTO = vaccineTypeDTO;

            try
            {
                _vaccineService.Add(vaccineDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] VaccineDTO vaccineDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingVaccine = _vaccineService.GetById(id);
            if (existingVaccine == null)
            {
                return NotFound(ErrorMessage.NotFound);
            }

            int vaccineTypeId = vaccineDTO.VaccineTypeId;
            var vaccineTypeDTO = _vaccineTypeService.GetById(vaccineTypeId);

            if (vaccineTypeDTO == null) return NotFound("vaccine type invalid");

            try
            {
                _vaccineService.Update(vaccineDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("deactivate/{id}")]
        public IActionResult Deactivate(int id)
        {
            var vaccine = _vaccineService.GetById(id);
            if (vaccine == null) return NotFound(ErrorMessage.NotFound);

            try
            {
                _vaccineService.Deactivate(vaccine);
                return Ok("Vaccine deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("deactivate")]
        public IActionResult DeactivateMultiple([FromBody] List<int> vaccineIds)
        {
            if (vaccineIds == null || vaccineIds.Count == 0)
            {
                return BadRequest("No vaccine type IDs provided.");
            }

            // Assuming you have a method in your service to handle bulk deactivation
            var result = _vaccineService.DeactivateMultipleVaccines(vaccineIds);

            if (result)
            {
                return NoContent(); // Return 204 No Content if deactivation is successful
            }

            return StatusCode(500, "An error occurred while deactivating vaccines."); // Handle any errors
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using (var stream = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                await file.CopyToAsync(stream);
                stream.Position = 0;

                var result = _vaccineService.ImportVaccineData(stream);

                if (!result.success)
                {
                    return Json(new { success = false, message = result.errorMessage });
                }

                return Json(new { success = true, message = "Vaccines imported successfully." });
            }
        }
    }
}
