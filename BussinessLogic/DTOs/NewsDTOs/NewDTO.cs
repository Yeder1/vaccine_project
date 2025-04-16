using System.ComponentModel.DataAnnotations;
using Vaccination.DataAccess.Models;

namespace Vaccination.BussinessLogic.DTOs.NewsDTOs
{
    public class NewDTO
    {
        public int? Id { get; set; }
        [MaxLength(4000)]
        public string? Content { get; set; }
        [MaxLength(1000)]
        public string? Preview { get; set; }
        [MaxLength(300)]
        public string? Title { get; set; }
        public DateTime? PostDate { get; set; }
        public int NewsTypeId { get; set; }
        public virtual NewsType? NewsType { get; set; }
    }
}