using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vaccination.BussinessLogic.DTOs.InjectionResultDTO;
using Vaccination.BussinessLogic.DTOs.VaccineTypeDTOs;
using Vaccination.DataAccess.Models;
using Vaccination.DataAccess.Repositories;

namespace Vaccination.BussinessLogic.Services.Impl
{
    public class InjectionResultService : IInjectionResultService
    {
        private readonly IBaseRepository<InjectionResult> _injectionResultRepository;
        private readonly IBaseRepository<VaccineType> _vaccineTypeRepository;
        private readonly IBaseRepository<Customer> _customerRepository;
        private readonly IBaseRepository<Vaccine> _vaccineRepository;

        private readonly IMapper _mapper;

        public InjectionResultService(IBaseRepository<Vaccine> vaccineRepository, IBaseRepository<Customer> customerRepository, IBaseRepository<InjectionResult> injectionRepo, IBaseRepository<VaccineType> vaccineTypeRepository, IMapper mapper)
        {
            _injectionResultRepository = injectionRepo;
            _vaccineTypeRepository = vaccineTypeRepository;
            _customerRepository = customerRepository;
            _vaccineRepository = vaccineRepository;
            _mapper = mapper;
        }

        public void Add(InjectionResultDTO entity)
        {
            var injectionResult = _mapper.Map<InjectionResult>(entity);
            _injectionResultRepository.Add(injectionResult);
        }

        public void Delete(InjectionResultDTO entity)
        {
            var injectionResult = _injectionResultRepository.GetById((int)entity.Id);
            injectionResult.IsDeleted = true;
            _injectionResultRepository.Update(injectionResult);
            _injectionResultRepository.Save();
        }
        public void DeleteMany(List<InjectionResultDTO> injectionResults)
        {
            foreach (var injectionResult in injectionResults)
            {
                var injectionResultFinding = _injectionResultRepository.GetById((int)injectionResult.Id);
                injectionResultFinding.IsDeleted = true;
                _injectionResultRepository.Update(injectionResultFinding);
            }

            _injectionResultRepository.Save();
        }

        public IEnumerable<InjectionResultDTO> GetAll()
        {
            List<InjectionResult> injectionResultsList = new List<InjectionResult>();

            var injectionResults = _injectionResultRepository.GetAll().Include(inRe => inRe.Customer)
                .Include(inRe => inRe.Vaccine)
                .Include(inRe => inRe.Vaccine.VaccineType)
                .Where(inre => inre.IsDeleted == false)
                .ToList();

            if (injectionResults.Count != 0)
            {
                injectionResultsList.AddRange(injectionResults);
            }

            return _mapper.Map<IEnumerable<InjectionResultDTO>>(injectionResultsList);
        }

        public InjectionResultDTO GetById(int id)
        {
            var injectionResult = _injectionResultRepository.GetByIdAsNoTracking(id)
            .Include(inRe => inRe.Customer)
            .Include(inRe => inRe.Vaccine)
            .Where(inRe => inRe.Id == id).FirstOrDefault();

            if (injectionResult == null)
            {
                return new InjectionResultDTO();
            }

            return _mapper.Map<InjectionResultDTO>(injectionResult);
        }

        public void Update(InjectionResultDTO entity)
        {
            var injectionResult = _mapper.Map<InjectionResult>(entity);
            _injectionResultRepository.Update(injectionResult);
        }

        public List<VaccineTypeDTO> GetAllVaccineType()
        {
            List<VaccineType> vaccineTypes = new List<VaccineType>();
            var vaccinTypesQuery = _vaccineTypeRepository.GetAll().ToList();

            if (vaccinTypesQuery.Count != 0)
            {
                vaccineTypes.AddRange(vaccinTypesQuery);
            }
            var vaccineTypesDTO = _mapper.Map<List<VaccineTypeDTO>>(vaccineTypes);
            return vaccineTypesDTO;
        }

        public List<InjectionResultCustomerDTO> GetAllVaccineCustomer()
        {
            List<Customer> customersEntity = new List<Customer>();
            var customers = _customerRepository.GetAll()
            .ToList();

            if (customers.Count != 0)
            {
                customersEntity.AddRange(customers);
            }
            var customersDTO = _mapper.Map<List<InjectionResultCustomerDTO>>(customersEntity);
            return customersDTO;
        }

        public void AddRequest(InjectionResultVaccineRequest entityRequest)
        {
            var injectionResult = _mapper.Map<InjectionResult>(entityRequest);
            var vaccine = _vaccineRepository.GetByIdAsNoTracking(injectionResult.VaccineId).FirstOrDefault();

            if (vaccine != null)
            {
                vaccine.NumberOfInjection = entityRequest.NumberOfInjection + vaccine.NumberOfInjection;

                _vaccineRepository.UpdateState(vaccine);
                _injectionResultRepository.Add(injectionResult);

                _injectionResultRepository.Save();
                _vaccineRepository.Save();
            }

        }

        public void UpdateRequest(InjectionResultVaccineRequest entityRequest)
        {
            var injectionResult = _mapper.Map<InjectionResult>(entityRequest);
            var injectionResultEntity = _injectionResultRepository.GetByIdAsNoTracking((int)entityRequest.Id).FirstOrDefault();
            var vaccine = _vaccineRepository.GetByIdAsNoTracking(injectionResult.VaccineId).FirstOrDefault();
            int numberOfInjectionGap;
            if (vaccine != null && injectionResultEntity != null)
            {
                if (injectionResultEntity.NumberOfInjection > injectionResult.NumberOfInjection)
                {
                    numberOfInjectionGap = injectionResultEntity.NumberOfInjection - injectionResult.NumberOfInjection;

                    if (numberOfInjectionGap > 0)
                    {
                        vaccine.NumberOfInjection -= numberOfInjectionGap;
                    }
                }
                else
                {
                    numberOfInjectionGap = -(injectionResultEntity.NumberOfInjection - injectionResult.NumberOfInjection);

                    if (numberOfInjectionGap > 0)
                    {
                        vaccine.NumberOfInjection -= numberOfInjectionGap;
                    }
                }

                _vaccineRepository.UpdateState(vaccine);
                _injectionResultRepository.UpdateState(injectionResult);

                _injectionResultRepository.Save();
            }
        }

        public IEnumerable<InjectionResultDTO> Search(string? keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return GetAll();
            }

            var results = _injectionResultRepository.GetAll().Include(inRe => inRe.Customer)
                .Include(inRe => inRe.Vaccine)
                .Include(inRe => inRe.Vaccine.VaccineType)
                .Where(x => x.IsDeleted == false &&
                x.Customer.FullName != null &&
                 (x.Customer.FullName.ToLower().Contains(keyword.ToLower())
                    || x.Vaccine.VaccineName.ToLower().Contains(keyword.ToLower()))

                ).ToList();


            return _mapper.Map<IEnumerable<InjectionResultDTO>>(results);

        }

        public void DeleteRange(List<InjectionResultDTO> entities)
        {
            throw new NotImplementedException();
        }

    }
}
