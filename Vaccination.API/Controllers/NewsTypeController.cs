using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaccination.BussinessLogic.DTOs.NewsTypeDTOs;
using Vaccination.BussinessLogic.Services;

namespace Vaccination.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NewsTypeController : ControllerBase
    {
        private readonly IBaseService<NewTypesDTO> _newTypesService;

        public NewsTypeController(IBaseService<NewTypesDTO> newTypesService)
        {
            _newTypesService = newTypesService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_newTypesService.GetAll());
        }
    }
}
