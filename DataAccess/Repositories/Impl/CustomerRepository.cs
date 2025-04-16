using Vaccination.DataAccess.Models;

namespace Vaccination.DataAccess.Repositories.Impl
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly VaccinationManagementContext _context;

        public CustomerRepository(VaccinationManagementContext context) : base(context)
        {
            _context = context;
        }
    }
}