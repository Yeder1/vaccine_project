using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Vaccination.BussinessLogic.DTOs.VaccineTypeDTOs
{
    public class VaccineTypeRequestDTO
    {
        public string VaccineType { get; set; }

        public IFormFile? Image { get; set; }

        public VaccineTypeDTO GetVaccineTypeDTO()
        {
            return JsonConvert.DeserializeObject<VaccineTypeDTO>(VaccineType);
        }
    }
}