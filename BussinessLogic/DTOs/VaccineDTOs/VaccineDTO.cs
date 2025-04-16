using System.ComponentModel.DataAnnotations;
using Vaccination.BussinessLogic.DTOs.VaccineTypeDTOs;
using Vaccination.DataAccess.Models;

namespace Vaccination.BussinessLogic.DTOs.VaccineDTOs
{
    public class VaccineDTO
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string? Contraindication { get; set; }
        [MaxLength(200)]
        public string? Indication { get; set; }
        public int? NumberOfInjection { get; set; }
        [MaxLength(50)]
        public string? Origin { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? NextBeginNextInjection { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? NextEndNextInjection { get; set; }

        [MaxLength(200)]
        public string? Usage { get; set; }
        [Required]
        [MaxLength(100)]
        public string VaccineName { get; set; }
        public bool Status { get; set; }

        public int VaccineTypeId { get; set; }
        public virtual VaccineTypeDTO? VaccineTypeDTO { get; set; }
    }
}
