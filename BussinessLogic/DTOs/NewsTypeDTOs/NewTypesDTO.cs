using System.ComponentModel.DataAnnotations;

namespace Vaccination.BussinessLogic.DTOs.NewsTypeDTOs
{
    public class NewTypesDTO
    {
        public int Id { get; set; }
        [MaxLength(1000)]
        public string? Description { get; set; }
        [MaxLength(50)]
        public string? NewsTypeName { get; set; }
    }
}
