using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Vaccination.BussinessLogic.Commons.Constants;
using Vaccination.BussinessLogic.DTOs.EmployeeDTOs;
using Vaccination.BussinessLogic.DTOs.NewsDTOs;
using Vaccination.BussinessLogic.DTOs.Responses;
using Vaccination.BussinessLogic.Services;
using Vaccination.BussinessLogic.Services.Impl;

namespace Vaccination.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IImageService _imageService;
        public EmployeeController(IEmployeeService employeeService, IImageService imageService)
        {
            _employeeService = employeeService;
            _imageService = imageService;
        }

        // Lấy tất cả khách hàng
        [HttpGet("getAll")]
        public ActionResult<ApiResponse<IEnumerable<EmployeeDTO>>> GetAllEmployees()
        {
            var employees = _employeeService.GetEmployeeDTOsNotDeleted().ToList();
            if (!employees.Any())
            {
                return Ok(ApiResponse<IEnumerable<EmployeeDTO>>.Success(employees, MessageEmployee.EMPLOYEES_NOT_FOUND));
            }

            return Ok(ApiResponse<IEnumerable<EmployeeDTO>>.Success(employees, MessageEmployee.EMPLOYEE_RETRIEVED_SUCCESS));
        }
        [HttpGet("{keyword}")]
        public ActionResult<ApiResponse<IEnumerable<EmployeeDTO>>> GetAllEmployees(string? keyword)
        {
            List<EmployeeDTO> list = new List<EmployeeDTO>();
            if (string.IsNullOrEmpty(keyword))
            {
                list = _employeeService.GetEmployeeDTOsNotDeleted().ToList();
            }
            else
            {
                list = _employeeService.Search(keyword).ToList();
            }

            if (!list.Any())
            {
                return Ok(ApiResponse<IEnumerable<EmployeeDTO>>.Success(list, MessageEmployee.EMPLOYEES_NOT_FOUND));
            }
            return Ok(ApiResponse<IEnumerable<EmployeeDTO>>.Success(list, MessageEmployee.EMPLOYEE_RETRIEVED_SUCCESS));
        }

        // Lấy thông tin khách hàng theo Id
        [HttpGet("GetEmployeeDetail/{id}")]
        public ActionResult<ApiResponse<EmployeeDTO>> GetEmployeeById(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound(ApiResponse<EmployeeDTO>.Success(employee, MessageEmployee.EMPLOYEE_NOT_FOUND));
            }
            else
            {
                return Ok(ApiResponse<EmployeeDTO>.Success(employee, MessageEmployee.EMPLOYEE_RETRIEVED_SUCCESS));
            }

        }

        // Thêm mới một khách hàng
        [HttpPost]
        public ActionResult<ApiResponse<EmployeeDTO>> AddEmployee([FromForm] EmployeeAddRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<EmployeeDTO>.Failure(MessageEmployee.INVALID_EMPLOYEE_DATA));
            }

            var employeeDTO = request.GetEmployeeDTO();
            _employeeService.Add(employeeDTO);

            if (request.Image != null)
            {
                EmployeeDTO createdEmployee = _employeeService.GetAll().OrderByDescending(e => e.Id).FirstOrDefault();
                _imageService.AddImage(Reference.Employee, createdEmployee.Id.ToString(), request.Image);
            }

            return Ok(ApiResponse<EmployeeDTO>.Success(employeeDTO, MessageEmployee.ADD_SUCCESS));
        }

        // Cập nhật thông tin khách hàng
        [HttpPut]
        public ActionResult<ApiResponse<EmployeeDTO>> UpdateEmployee([FromForm] EmployeeAddRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<EmployeeDTO>.Failure(MessageEmployee.INVALID_EMPLOYEE_DATA));
                }

                var employeeDTO = request.GetEmployeeDTO();
                _employeeService.Update(employeeDTO);

                if (request.Image != null)
                {
                    _imageService.UpdateImage(Reference.Employee, employeeDTO.Id.ToString(), request.Image);
                }

                return Ok(ApiResponse<EmployeeDTO>.Success(employeeDTO, MessageEmployee.UPDATE_SUCCESS));
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<EmployeeDTO>.Failure(MessageEmployee.EMPLOYEE_NOT_FOUND));
            }

        }

        // Xóa khách hàng
        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<string>> DeleteEmployee(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound(ApiResponse<string>.Failure(string.Format(MessageEmployee.EMPLOYEE_NOT_FOUND, id)));
            }

            _employeeService.Delete(employee);
            return Ok(ApiResponse<string>.Success($"Employee with Id = {id} deleted successfully.", MessageEmployee.DELETE_SUCCESS));
        }

        [HttpDelete("List")]
        public IActionResult DeleteList([FromBody] List<EmployeeNoValidateDTO> request)
        {
            try
            {
                foreach (var item in request)
                {
                    _employeeService.DeleteNoValidateModel(item);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
