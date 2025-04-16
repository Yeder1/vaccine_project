using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaccination.BussinessLogic.DTOs.NewsDTOs;
using Vaccination.BussinessLogic.Services;

namespace Vaccination.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly IBaseService<NewDTO> _newsService;

        public NewsController(IBaseService<NewDTO> newService)
        {
            _newsService = newService;
        }

        [HttpGet]
        public IActionResult Search([FromQuery] string? keyword)
        {
            return Ok(_newsService.Search(keyword));
        }

        [HttpPost]
        public IActionResult Add([FromBody] NewDTO request)
        {
            try
            {
                _newsService.Add(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] NewDTO request)
        {
            try
            {
                _newsService.Update(request);
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
                return Ok(_newsService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] NewDTO request)
        {
            try
            {
                _newsService.Delete(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("List")]
        public IActionResult DeleteList([FromBody] List<NewDTO> request)
        {
            try
            {
                _newsService.DeleteRange(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}