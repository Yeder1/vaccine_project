using Vaccination.DataAccess.Models;

namespace Vaccination.DataAccess.Repositories.Impl
{
    public class VaccineTypeRepository : BaseRepository<VaccineType>, IVaccineTypeRepository
    {
        private readonly VaccinationManagementContext _context;

        public VaccineTypeRepository(VaccinationManagementContext context) : base(context)
        {
            _context = context;
        }

        string IVaccineTypeRepository.Add(VaccineType vaccineType)
        {
            _context.VaccineTypes.Add(vaccineType);
            _context.SaveChanges();
            return vaccineType.Id.ToString();
        }
    }
}