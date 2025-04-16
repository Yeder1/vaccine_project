using Vaccination.BussinessLogic.DTOs.VaccineDTOs;

namespace Vaccination.BussinessLogic.Services
{
    public interface IVaccineService : IBaseService<VaccineDTO>
    {
        IEnumerable<VaccineDTO> GetVaccinesByTypes(int typeId);
        bool Deactivate(VaccineDTO vaccine);
        bool DeactivateMultipleVaccines(List<int> vaccineIds);

        (bool success, string errorMessage) ImportVaccineData(Stream fileStream);

    }
}
