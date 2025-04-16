using System.ComponentModel.DataAnnotations;

namespace Vaccination.DataAccess.Models
{
    public class Customer : BaseEntity
    {
        [MaxLength(255)]
        public string? Address { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }
        [MaxLength(100)]
        public string? Email { get; set; }
        [MaxLength(100)]
        public string? FullName { get; set; }
        public bool? Gender { get; set; }
        [MaxLength(12)]
        public string? IdentityCard { get; set; }
        [MaxLength(255)]
        public string? Password { get; set; }
        [MaxLength(20)]
        public string? Phone { get; set; }
        [MaxLength(255)]
        public string? Username { get; set; }
    }
}
