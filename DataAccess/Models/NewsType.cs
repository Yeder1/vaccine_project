using System.ComponentModel.DataAnnotations;

namespace Vaccination.DataAccess.Models
{
    public class NewsType : BaseEntity
    {
        [MaxLength(1000)]
        public string? Description { get; set; }
        [MaxLength(50)]
        public string? NewsTypeName { get; set; }
    }
}
