using Vaccination.BussinessLogic.DTOs.CustomerDTOs;

namespace Vaccination.BussinessLogic.Services
{
    public interface ICustomerService : IBaseService<CustomerDTO>
    {
        public IEnumerable<CustomerDTO> GetCustomerDTOsNotDeleted();

    }
}
