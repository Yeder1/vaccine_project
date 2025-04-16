using AutoMapper;
using Vaccination.BussinessLogic.DTOs.NewsTypeDTOs;
using Vaccination.DataAccess.Models;
using Vaccination.DataAccess.Repositories;

namespace Vaccination.BussinessLogic.Services.Impl
{
    public class NewTypesService : INewTypesService
    {
        private readonly IBaseRepository<NewsType> _newTypesRepository;
        private readonly IMapper _mapper;

        public NewTypesService(IBaseRepository<NewsType> newTypesRepository, IMapper mapper)
        {
            _newTypesRepository = newTypesRepository;
            _mapper = mapper;
        }
        public void Add(NewTypesDTO entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(NewTypesDTO entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(List<NewTypesDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewTypesDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<NewTypesDTO>>(_newTypesRepository.GetAll().ToList());
        }

        public NewTypesDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewTypesDTO> Search(string? keyword)
        {
            throw new NotImplementedException();
        }

        public void Update(NewTypesDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
