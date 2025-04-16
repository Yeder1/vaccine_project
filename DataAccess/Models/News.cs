using System.ComponentModel.DataAnnotations;

namespace Vaccination.DataAccess.Models
{
    public class News : BaseEntity
    {
        [MaxLength(4000)]
        public string? Content { get; set; }
        [MaxLength(1000)]
        public string? Preview { get; set; }
        [MaxLength(300)]
        public string? Title { get; set; }
        public DateTime? PostDate { get; set; }
        public int NewsTypeId { get; set; }
        public virtual NewsType NewsType { get; set; }
    }
}
