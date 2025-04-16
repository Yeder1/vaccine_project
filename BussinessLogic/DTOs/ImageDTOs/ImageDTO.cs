using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccination.BussinessLogic.DTOs.ImageDTOs
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public string RefId { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
