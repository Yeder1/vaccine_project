using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Vaccination.BussinessLogic.DTOs.EmployeeDTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; } // Alphanumeric(6) Auto Increment

        [MaxLength(100, ErrorMessage = "Employee name must not exceed 100 characters.")]
        public string? EmployeeName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public bool? Gender { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [RegularExpression(@"\d{10}", ErrorMessage = "Phone number must be exactly 11 numeric digits.")]
        public string? Phone { get; set; }

        [MaxLength(100, ErrorMessage = "Address must not exceed 100 characters.")]
        public string? Address { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email must not exceed 100 characters.")]
        public string? Email { get; set; }

        [MaxLength(100, ErrorMessage = "WorkingPlace must not exceed 100 characters.")]
        public string? WorkingPlace { get; set; }

        public string? Position { get; set; }

        [JsonIgnore]
        public IFormFile? Image { get; set; }
    }
}