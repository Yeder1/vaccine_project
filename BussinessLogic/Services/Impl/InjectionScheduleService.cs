using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vaccination.BussinessLogic.DTOs.InjectionScheduleDTOs;
using Vaccination.DataAccess.Models;
using Vaccination.DataAccess.Repositories;

namespace Vaccination.BussinessLogic.Services
{
    public class InjectionScheduleService : BaseService<InjectionScheduleDTOs, InjectionSchedule>, IInjectionScheduleService
    {
        private readonly IInjectionScheduleRepository _injectionScheduleRepository;
        private readonly IBaseRepository<Vaccine> _vaccineRepository;
        private readonly IMapper _mapper;

        public InjectionScheduleService(
            IInjectionScheduleRepository injectionScheduleRepository,
            IBaseRepository<Vaccine> vaccineRepository,
            IMapper mapper) : base(injectionScheduleRepository, mapper)
        {
            _injectionScheduleRepository = injectionScheduleRepository;
            _vaccineRepository = vaccineRepository;
            _mapper = mapper;
        }

        public override IEnumerable<InjectionScheduleDTOs> GetAll()
        {
            var injectionSchedules = _injectionScheduleRepository.GetAll().Include(i => i.Vaccine).ToList();
            return _mapper.Map<IEnumerable<InjectionScheduleDTOs>>(injectionSchedules);
        }

        public override void Update(InjectionScheduleDTOs dto)
        {
            var existingEntity = _injectionScheduleRepository.GetById(dto.Id);
            if (existingEntity == null)
            {
                throw new ArgumentException($"InjectionSchedule with id {dto.Id} not found");
            }

            // Update the existing entity with the new values
            _mapper.Map(dto, existingEntity);

            // Update the entity in the repository
            _injectionScheduleRepository.Update(existingEntity);
            _injectionScheduleRepository.Save();
        }

        public override InjectionScheduleDTOs GetById(int id)
        {
            var injectionSchedule = _injectionScheduleRepository.GetAll()
                .Include(i => i.Vaccine)
                .FirstOrDefault(i => i.Id == id);

            if (injectionSchedule == null)
            {
                return null;
            }

            var dto = _mapper.Map<InjectionScheduleDTOs>(injectionSchedule);
            dto.VaccineName = injectionSchedule.Vaccine?.VaccineName;

            return dto;
        }

    }
}
