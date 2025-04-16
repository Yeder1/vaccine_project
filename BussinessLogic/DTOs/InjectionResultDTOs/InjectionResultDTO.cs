using System.ComponentModel.DataAnnotations;

namespace Vaccination.BussinessLogic.DTOs.InjectionResultDTO
{
    public class InjectionResultDTO
    {
        public int? Id { get; set; }
        public string Prevention { get; set; }
        public InjectionResultVaccineDTO? Vaccine { get; set; }
        public int? NumberOfInjection { get; set; }
        public DateTime InjectionDate { get; set; }
        public DateTime? NextInjectionDate { get; set; }
        public string InjectionPlace { get; set; }
        // Additional customer-related information
        public InjectionResultCustomerDTO? Customer { get; set; }
        public bool IsDelete { get; set; } = false;
    }

    public class InjectionResultCustomerDTO
    {
        public int? Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string IdentityCard { get; set; }
        public int? Age { get; set; }
        public int? Year { get; set; }
    }

    public class InjectionResultVaccineDTO
    {
        public int? Id { get; set; }
        public string VaccineTypeName { get; set; }

    }
    public class InjectionResultVaccineRequest : IValidatableObject
    {
        public int? Id { get; set; }
        public int CustomerId { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? InjectionDate { get; set; }
        public string InjectionPlace { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? NextInjectionDate { get; set; }
        public int NumberOfInjection { get; set; }
        public string Prevention { get; set; }
        public int VaccineId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (InjectionDate.HasValue && NextInjectionDate.HasValue && InjectionDate.Value > NextInjectionDate.Value)
            {
                yield return new ValidationResult("From date must be less than to date", [nameof(InjectionDate), nameof(NextInjectionDate)]);
            }
        }
    }

}
