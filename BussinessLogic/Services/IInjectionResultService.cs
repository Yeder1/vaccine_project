using Vaccination.BussinessLogic.DTOs.InjectionResultDTO;
using Vaccination.BussinessLogic.DTOs.VaccineTypeDTOs;


namespace Vaccination.BussinessLogic.Services
{
    public interface IInjectionResultService : IBaseService<InjectionResultDTO>
    {
        void DeleteMany(List<InjectionResultDTO> entities);
        List<VaccineTypeDTO> GetAllVaccineType();
        List<InjectionResultCustomerDTO> GetAllVaccineCustomer();
        void AddRequest(InjectionResultVaccineRequest entityRequest);
        void UpdateRequest(InjectionResultVaccineRequest entityRequest);
    }
}
