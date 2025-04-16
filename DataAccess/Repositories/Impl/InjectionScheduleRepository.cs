using Vaccination.DataAccess.Models;

namespace Vaccination.DataAccess.Repositories.Impl
{
    public class InjectionScheduleRepository : BaseRepository<InjectionSchedule>, IInjectionScheduleRepository
    {
        public InjectionScheduleRepository(VaccinationManagementContext context) : base(context)
        {

        }
    }
}
