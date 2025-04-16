using System.ComponentModel.DataAnnotations;

namespace Vaccination.DataAccess.Models
{
    public enum ScheduleStatus
    {
        NotYet,
        Open,
        Over
    }

    public class InjectionSchedule : BaseEntity
    {
        [MaxLength(1000)]
        public string? Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [MaxLength(255)]
        public string? Place { get; set; }
        
        public ScheduleStatus Status { get; set; }

        public DateTime ?DateCreated { get; set; }   

        public int VaccineId { get; set; }
        public virtual Vaccine Vaccine { get; set; }
    }
}
