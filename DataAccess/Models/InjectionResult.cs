using System.ComponentModel.DataAnnotations;

namespace Vaccination.DataAccess.Models
{
    public class InjectionResult : BaseEntity
    {
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? InjectionDate { get; set; }
        [MaxLength(255)]
        public string? InjectionPlace { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? NextInjectionDate { get; set; }

        [Required]
        public int NumberOfInjection { get; set; }
        [MaxLength(100)]
        public string? Prevention { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int VaccineId { get; set; }
        public virtual Vaccine Vaccine { get; set; }
    }

}
