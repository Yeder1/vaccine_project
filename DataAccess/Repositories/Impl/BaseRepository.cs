using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vaccination.DataAccess.Models;

namespace Vaccination.DataAccess.Repositories.Impl
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly VaccinationManagementContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(VaccinationManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual T Find(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

        public IQueryable<T> FindList(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void DeleteRange(List<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Detach(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetByIdAsNoTracking(int id)
        {
            return _dbSet.AsNoTracking().Where(t => t.Id == id);
        }
        public void UpdateState(T entity)
        {
            var entry = _context.Entry(entity);
            var key = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties
            .Select(p => p.Name).Single();
            var keyValue = entry.Property(key).CurrentValue;
            var existingEntity = _dbSet.Local
            .FirstOrDefault(e => _context.Entry(e).Property(key).CurrentValue.Equals(keyValue));

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            if (entry.State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
            }
            entry.State = EntityState.Modified;
            _context.Update(entity);
        }


    }
}
