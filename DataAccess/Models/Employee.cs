using System.ComponentModel.DataAnnotations;

namespace Vaccination.DataAccess.Models
{
    public class Employee : BaseEntity
    {
        [MaxLength(255)]
        public string? Address { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }
        [MaxLength(100)]
        public string? Email { get; set; }
        [MaxLength(200)]
        public string? EmployeeName { get; set; }
        public bool? Gender { get; set; }
        [MaxLength(255)]
        public string? Image { get; set; }
        [MaxLength(255)]
        public string? Password { get; set; }
        [MaxLength(20)]
        public string? Phone { get; set; }
        [MaxLength(100)]
        public string? Position { get; set; }
        [MaxLength(255)]
        public string? Username { get; set; }
        [MaxLength(255)]
        public string? WorkingPlace { get; set; }
    }
}
