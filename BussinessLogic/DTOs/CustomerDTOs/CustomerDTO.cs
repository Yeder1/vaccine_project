using System.ComponentModel.DataAnnotations;

namespace Vaccination.BussinessLogic.DTOs.CustomerDTOs
{
    public class CustomerDTO
    {

        public int ID { get; set; } // Alphanumeric(6) Auto Increment

        [Required]
        [StringLength(100, ErrorMessage = "Full name must not exceed 100 characters.")]
        public string FullName { get; set; } // String(100)

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime? DateOfBirth { get; set; } // Date

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; } // Boolean (Checkbox)

        [Required]
        [RegularExpression(@"\d{10}", ErrorMessage = "Identity Card must be exactly 10 numeric digits.")]
        public string IdentityCard { get; set; } // Numeric(10)

        [Required]
        [StringLength(100, ErrorMessage = "Native place must not exceed 100 characters.")]
        public string Address { get; set; } // String(100)

        [Required]
        [StringLength(10, ErrorMessage = "Username must not exceed 10 characters.")]
        public string Username { get; set; } // String(10)

        [Required]
        [StringLength(20, ErrorMessage = "Password must not exceed 20 characters.")]
        public string Password { get; set; } // String(20)

        [Required]
        [Compare("Password", ErrorMessage = "Confirm password does not match the password.")]
        [StringLength(20, ErrorMessage = "Confirm password must not exceed 20 characters.")]
        public string ConfirmPassword { get; set; } // String(20)

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(25, ErrorMessage = "Email must not exceed 25 characters.")]
        public string Email { get; set; } // String(25)

        [Required]
        [RegularExpression(@"\d{11}", ErrorMessage = "Phone number must be exactly 11 numeric digits.")]
        public string Phone { get; set; } // Numeric(11)
        
    }
}
