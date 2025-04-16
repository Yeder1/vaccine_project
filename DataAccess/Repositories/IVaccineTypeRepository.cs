using Vaccination.DataAccess.Models;

namespace Vaccination.DataAccess.Repositories
{
    public interface IVaccineTypeRepository : IBaseRepository<VaccineType>
    {
        string Add(VaccineType vaccineType);
    }
}
