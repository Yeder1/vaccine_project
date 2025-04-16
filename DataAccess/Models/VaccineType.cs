using System.ComponentModel.DataAnnotations;

namespace Vaccination.DataAccess.Models
{
    public class VaccineType : BaseEntity
    {
        [MaxLength(50)]
        public string VaccineTypeCode { get; set; }

        [MaxLength(50)]
        public string? VaccineTypeName { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
}
