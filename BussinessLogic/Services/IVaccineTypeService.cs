using Vaccination.BussinessLogic.DTOs.VaccineTypeDTOs;

namespace Vaccination.BussinessLogic.Services
{
    public interface IVaccineTypeService : IBaseService<VaccineTypeDTO>
    {
        IEnumerable<VaccineTypeDTO> FindByName(string name);
        string AddVaccineType(VaccineTypeDTO dto);
        void Delete(VaccineTypeDTO dto, bool isHardDelete);
        void DeleteRange(List<VaccineTypeDTO> dtos, bool isHardDelete);
        void Deactivate(VaccineTypeDTO request);
        void DeactivateRange(List<VaccineTypeDTO> request);
    }
}
