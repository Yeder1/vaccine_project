using System.Linq.Expressions;

namespace Vaccination.DataAccess.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        // Lấy tất cả các entity
        IQueryable<T> GetAll();

        // Lấy entity theo Id
        T GetById(int id);

        // Lấy entity theo expression
        T Find(Expression<Func<T, bool>> expression);

        // Lấy entity theo expression
        IQueryable<T> FindList(Expression<Func<T, bool>> expression);

        // Thêm mới một entity
        void Add(T entity);

        // Cập nhật một entity
        void Update(T entity);

        // Xóa một entity
        void Delete(T entity);

        // Xóa nhiều entity
        void DeleteRange(List<T> entities);


        // Lưu thay đổi
        void Save();

        void Detach(T entity);

        void UpdateState(T entity);
        IQueryable<T> GetByIdAsNoTracking(int id);

    }
}
