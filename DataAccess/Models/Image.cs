using System.ComponentModel.DataAnnotations.Schema;

namespace Vaccination.DataAccess.Models
{
    [Table("Images")]
    public class Image : BaseEntity
    {
        public string RefId { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}