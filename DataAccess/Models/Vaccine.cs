using System.ComponentModel.DataAnnotations;

namespace Vaccination.DataAccess.Models
{
    public class Vaccine : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string VaccineName { get; set; }

        [MaxLength(200)]
        public string? Contraindication { get; set; }
        [MaxLength(200)]
        public string? Indication { get; set; }
        [Required]
        public int NumberOfInjection { get; set; }
        [MaxLength(50)]
        public string? Origin { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? NextBeginNextInjection { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? NextEndNextInjection { get; set; }

        [MaxLength(200)]
        public string? Usage { get; set; }

        public int VaccineTypeId { get; set; }
        public virtual VaccineType VaccineType { get; set; }
        public bool Status { get; set; } = true;
    }
}
