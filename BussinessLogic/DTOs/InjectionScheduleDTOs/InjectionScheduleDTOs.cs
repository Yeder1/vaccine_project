using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Vaccination.DataAccess.Models;

namespace Vaccination.BussinessLogic.DTOs.InjectionScheduleDTOs
{
    public class InjectionScheduleDTOs : IValidatableObject
    {
        public int Id { get; set; }
        [MaxLength(1000)]
        public string? Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? EndDate { get; set; }

        [MaxLength(255)]
        [Required]
        public string? Place { get; set; }

        [JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public ScheduleStatus Status { get; set; }

        [Required]
        public int VaccineId { get; set; }
        public string ?VaccineName { get; set; }

        public virtual Vaccine ?Vaccine { get; set; }

        public DateTime ?DateCreated { get; set; } = DateTime.Now;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (EndDate.HasValue && StartDate.HasValue && EndDate.Value < StartDate.Value)
            {
                yield return new ValidationResult(
                    "End date must be greater than or equal to the start date.",
                    new[] { nameof(StartDate), nameof(EndDate) });
            }
        }

    }
}
