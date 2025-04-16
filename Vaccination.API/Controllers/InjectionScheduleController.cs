using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaccination.BussinessLogic.DTOs;
using Vaccination.BussinessLogic.DTOs.InjectionScheduleDTOs;
using Vaccination.BussinessLogic.Services;

namespace Vaccination.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InjectionScheduleController : ControllerBase
    {
        private readonly IBaseService<InjectionScheduleDTOs> _injectionScheduleService;

        public InjectionScheduleController(IBaseService<InjectionScheduleDTOs> injectionScheduleService)
        {
            _injectionScheduleService = injectionScheduleService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var schedules = _injectionScheduleService.GetAll().OrderByDescending(x => x.DateCreated);
            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var schedule = _injectionScheduleService.GetById(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return Ok(schedule);
        }

        [HttpPost]
        public IActionResult Create([FromBody] InjectionScheduleDTOs scheduleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _injectionScheduleService.Add(scheduleDto);
                return CreatedAtAction(nameof(GetById), new { id = scheduleDto.Id }, scheduleDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] InjectionScheduleDTOs scheduleDto)
        {
            if (id != scheduleDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _injectionScheduleService.Update(scheduleDto);
                return CreatedAtAction(nameof(GetById), new { id = scheduleDto.Id }, scheduleDto);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while updating the schedule");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var schedule = _injectionScheduleService.GetById(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _injectionScheduleService.Delete(schedule);
            return NoContent();
        }
    }
}
