using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaccination.BussinessLogic.Commons.Constants;
using Vaccination.BussinessLogic.DTOs.VaccineTypeDTOs;
using Vaccination.BussinessLogic.Services;

namespace Vaccination.API.Controllers
{
    [Authorize]
    [Route("api/vaccine-type")]
    [ApiController]
    public class VaccineTypeController : ControllerBase
    {
        private readonly IVaccineTypeService _vaccineTypeService;
        private readonly IImageService _imageService;

        public VaccineTypeController(IVaccineTypeService vaccineTypeService, IImageService imageService)
        {
            _vaccineTypeService = vaccineTypeService;
            _imageService = imageService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_vaccineTypeService.GetAll());
        }

        [HttpGet("search/{keyword}")]
        public IActionResult GetList(string? keyword)
        {
            try
            {
                var response = _vaccineTypeService.Search(keyword);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] VaccineTypeRequestDTO request)
        {
            try
            {
                var vaccineTypeDTO = request.GetVaccineTypeDTO();
                string id = _vaccineTypeService.AddVaccineType(vaccineTypeDTO);
                if (request.Image != null)
                {
                    _imageService.AddImage(Reference.VaccineType, id, request.Image);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm] VaccineTypeRequestDTO request)
        {
            try
            {
                var vaccineTypeDTO = request.GetVaccineTypeDTO();
                _vaccineTypeService.Update(vaccineTypeDTO);
                if (request.Image != null)
                {
                    _imageService.UpdateImage(Reference.VaccineType, vaccineTypeDTO.Id.ToString(), request.Image);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("deactivate")]
        public IActionResult Deactivate([FromBody] VaccineTypeDTO request)
        {
            try
            {
                _vaccineTypeService.Deactivate(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("deactivate-list")]
        public IActionResult DeactivateList([FromBody] List<VaccineTypeDTO> request)
        {
            try
            {
                _vaccineTypeService.DeactivateRange(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                return Ok(_vaccineTypeService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] VaccineTypeDTO request)
        {
            try
            {
                _vaccineTypeService.Delete(request, false);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}