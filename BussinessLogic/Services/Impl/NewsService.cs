using AutoMapper;
using Vaccination.BussinessLogic.DTOs.NewsDTOs;
using Vaccination.DataAccess.Models;
using Vaccination.DataAccess.Repositories;

namespace Vaccination.BussinessLogic.Services.Impl
{
    public class NewsService : INewsService
    {
        private readonly IBaseRepository<News> _newsRepository;
        private readonly IMapper _mapper;

        public NewsService(IBaseRepository<News> newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }
        public void Add(NewDTO entity)
        {
            News newEntity = _mapper.Map<News>(entity);
            _newsRepository.Add(newEntity);
            _newsRepository.Save();
        }

        public void Delete(NewDTO entity)
        {
            var newEntity = _mapper.Map<News>(entity);
            _newsRepository.Delete(newEntity);
            _newsRepository.Save();
        }

        public void DeleteRange(List<NewDTO> entities)
        {
            var ids = entities.Select(e => e.Id).ToList(); // Assuming NewDTO has an Id property
            var listMaps = _newsRepository.GetAll().Where(n => ids.Contains(n.Id)).ToList();

            if (listMaps.Any())
            {
                _newsRepository.DeleteRange(listMaps);
                _newsRepository.Save();
            }
        }

        public IEnumerable<NewDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<NewDTO>>(_newsRepository.GetAll().OrderByDescending(x => x.PostDate).ToList());
        }

        public NewDTO GetById(int id)
        {
            var newEntity = _newsRepository.GetById(id);
            return _mapper.Map<NewDTO>(newEntity);
        }

        public IEnumerable<NewDTO> Search(string? keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return GetAll();
            }

            var results = _newsRepository.FindList(
                    x => x.Title.ToLower().Contains(keyword.ToLower())
                    || x.Content.ToLower().Contains(keyword.ToLower())
            ).OrderByDescending(x => x.PostDate).ToList();
            return _mapper.Map<IEnumerable<NewDTO>>(results);
        }

        public void Update(NewDTO entity)
        {
            News newEntity = _mapper.Map<News>(entity);
            newEntity.PostDate = entity.PostDate;
            _newsRepository.Update(newEntity);
            _newsRepository.Save();
        }
    }
}