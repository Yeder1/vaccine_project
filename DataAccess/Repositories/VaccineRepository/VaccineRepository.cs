using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vaccination.DataAccess.Models;

namespace Vaccination.DataAccess.Repositories.VaccineRepository
{
    public class VaccineRepository : BaseRepository<Vaccine>, IVaccineRepository
    {
        private readonly VaccinationManagementContext _context;
        private readonly DbSet<Vaccine> _dbSet;
        public VaccineRepository(VaccinationManagementContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Vaccine>();
        }

        public override IQueryable<Vaccine> GetAll()
        {
            return _dbSet.Include(v => v.VaccineType);
        }

        public override Vaccine Find(Expression<Func<Vaccine, bool>> expression)
        {
            return _dbSet.Include(v => v.VaccineType).FirstOrDefault(expression);
        }

        public override void Delete(Vaccine vaccine)
        {

            if (_context.Entry(vaccine).State == EntityState.Detached)
            {
                _dbSet.Attach(vaccine);
            }
            vaccine.IsDeleted = true;
        }

        public void Deactivate(Vaccine vaccine)
        {

            if (_context.Entry(vaccine).State == EntityState.Detached)
            {
                _dbSet.Attach(vaccine);
            }
            vaccine.Status = false;
        }

    }
}
