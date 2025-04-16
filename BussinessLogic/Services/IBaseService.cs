namespace Vaccination.BussinessLogic.Services
{
    public interface IBaseService<DTO>
    {
        IEnumerable<DTO> GetAll();
        IEnumerable<DTO> Search(string? keyword);
        DTO GetById(int id);
        void Add(DTO entity);
        void Update(DTO entity);
        void Delete(DTO entity);
        void DeleteRange(List<DTO> entities);
    }
}
