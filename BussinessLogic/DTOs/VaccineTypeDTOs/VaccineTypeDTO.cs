using System.ComponentModel.DataAnnotations;

namespace Vaccination.BussinessLogic.DTOs.VaccineTypeDTOs
{
    public class VaccineTypeDTO
    {        
        public int Id { get; set; }

        public string VaccineTypeCode { get; set; }

        [MaxLength(50)]
        public string? VaccineTypeName { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
