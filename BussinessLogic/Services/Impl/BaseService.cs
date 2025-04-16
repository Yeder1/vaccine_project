using Vaccination.DataAccess.Repositories;
using AutoMapper;

namespace Vaccination.BussinessLogic.Services
{
    public class BaseService<DTO, T> : IBaseService<DTO> where T : class
    {
        protected readonly IBaseRepository<T> _repository;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual IEnumerable<DTO> GetAll()
        {
            var entities = _repository.GetAll();
            return _mapper.Map<IEnumerable<DTO>>(entities);
        }

        public virtual DTO GetById(int id)
        {
            var entity = _repository.GetById(id);
            return _mapper.Map<DTO>(entity);
        }

        public virtual void Add(DTO dto)
        {
            var entity = _mapper.Map<T>(dto);
            _repository.Add(entity);
            _repository.Save();
        }

        public virtual void Update(DTO dto)
        {
            var entity = _mapper.Map<T>(dto);
            _repository.Update(entity);
            _repository.Save();
        }

        public virtual void Delete(DTO dto)
        {
            var entity = _mapper.Map<T>(dto);
            _repository.Delete(entity);
            _repository.Save();
        }

        public IEnumerable<DTO> Search(string? keyword)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(List<DTO> entities)
        {
            throw new NotImplementedException();
        }
    }
}
