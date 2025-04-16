using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaccination.BussinessLogic.Commons.Constants;
using Vaccination.BussinessLogic.DTOs.CustomerDTOs;
using Vaccination.BussinessLogic.DTOs.Responses;
using Vaccination.BussinessLogic.Services;

namespace Vaccination.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        // Lấy tất cả khách hàng
        [HttpGet("all")]
        public ActionResult<ApiResponse<IEnumerable<CustomerDTO>>> GetAllCustomers()
        {
           
            var customers = _customerService.GetAll().ToList();
            if (!customers.Any())
            {
                return Ok(ApiResponse<IEnumerable<CustomerDTO>>.Success(customers, MessageCustomer.CUSTOMERS_NOT_FOUND));
            }

            return Ok(ApiResponse<IEnumerable<CustomerDTO>>.Success(customers, MessageCustomer.CUSTOMER_RETRIEVED_SUCCESS));
        }
        [HttpGet]
        public ActionResult<ApiResponse<IEnumerable<CustomerDTO>>> GetAllCustomers(string? keyword)
        {
            List<CustomerDTO> list = new List<CustomerDTO>();
            if (string.IsNullOrEmpty(keyword))
            {
                list = _customerService.GetCustomerDTOsNotDeleted().ToList();
            }
            else
            {
                list = _customerService.Search(keyword).ToList();
            }

            if (!list.Any())
            {
                return Ok(ApiResponse<IEnumerable<CustomerDTO>>.Success(list, MessageCustomer.CUSTOMERS_NOT_FOUND));
            }
            return Ok(ApiResponse<IEnumerable<CustomerDTO>>.Success(list, MessageCustomer.CUSTOMER_RETRIEVED_SUCCESS));
        }

        // Lấy thông tin khách hàng theo Id
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<CustomerDTO>> GetCustomerById(int id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null)
            {
                return NotFound(ApiResponse<CustomerDTO>.Success(customer, MessageCustomer.CUSTOMER_NOT_FOUND));
            }
            else
            {
                return Ok(ApiResponse<CustomerDTO>.Success(customer, MessageCustomer.CUSTOMER_RETRIEVED_SUCCESS));
            }

        }

        // Thêm mới một khách hàng
        [HttpPost]
        public ActionResult<ApiResponse<CustomerDTO>> AddCustomer([FromBody] CustomerDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<CustomerDTO>.Failure(MessageCustomer.INVALID_CUSTOMER_DATA));
            }

            _customerService.Add(customer);
            return Ok(ApiResponse<CustomerDTO>.Success(customer, MessageCustomer.ADD_SUCCESS));
        }

        // Cập nhật thông tin khách hàng
        [HttpPut]
        public ActionResult<ApiResponse<CustomerDTO>> UpdateCustomer([FromBody] CustomerDTO customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<CustomerDTO>.Failure(MessageCustomer.INVALID_CUSTOMER_DATA));
                }
                _customerService.Update(customer);
                return Ok(ApiResponse<CustomerDTO>.Success(customer, MessageCustomer.UPDATE_SUCCESS));
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<CustomerDTO>.Failure(MessageCustomer.CUSTOMER_NOT_FOUND));
            }
        }

        // Xóa khách hàng
        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<string>> DeleteCustomer(int id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null)
            {
                return NotFound(ApiResponse<string>.Failure(string.Format(MessageCustomer.CUSTOMER_NOT_FOUND, id)));
            }

            _customerService.Delete(customer);
            return Ok(ApiResponse<string>.Success($"Customer with Id = {id} deleted successfully.", MessageCustomer.DELETE_SUCCESS));
        }
        [HttpDelete]
        public ActionResult<ApiResponse<string>> DeleteCustomer(List<int> ids)
        {
            if (ids.Count > 0)
            {
                foreach (int id in ids)
                {
                    var customer = _customerService.GetById(id);
                    if (customer != null)
                    {
                        _customerService.Delete(customer);
                    }
                }
            }

            return Ok(ApiResponse<string>.Success($"Customer with Id = {ids} deleted successfully.", MessageCustomer.DELETE_SUCCESS));
        }
        
    }
}
