using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Vaccination.BussinessLogic.Commons.Constants;
using Vaccination.BussinessLogic.DTOs.VaccineTypeDTOs;
using Vaccination.DataAccess.Models;
using Vaccination.DataAccess.Repositories;

namespace Vaccination.BussinessLogic.Services.Impl
{
    public class VaccineTypeService : IVaccineTypeService
    {
        private readonly IVaccineTypeRepository _repository;
        private readonly IMapper _mapper;

        public VaccineTypeService(IVaccineTypeRepository vaccineTypeRepository, IMapper mapper)
        {
            _repository = vaccineTypeRepository;
            _mapper = mapper;
        }

        public void Add(VaccineTypeDTO dto)
        {
            var vaccineType = _mapper.Map<VaccineType>(dto);
            _repository.Add(vaccineType);
            _repository.Save();
        }

        public string AddVaccineType(VaccineTypeDTO dto)
        {
            var vaccineType = _mapper.Map<VaccineType>(dto);
            return _repository.Add(vaccineType);
        }

        public void Delete(VaccineTypeDTO dto)
        {
            var currentVaccineType = _repository.GetById(dto.Id);

            if (currentVaccineType != null)
            {
                _repository.Delete(currentVaccineType);
            }
            else
            {
                throw new ArgumentException(ErrorMessage.NotFound);
            }

            _repository.Save();
        }

        public void Delete(VaccineTypeDTO dto, bool isHardDelete)
        {
            var currentVaccineType = _repository.GetById(dto.Id);

            if (currentVaccineType != null)
            {
                if (isHardDelete)
                {
                    _repository.Delete(currentVaccineType);
                }
                else
                {
                    currentVaccineType.IsDeleted = true;
                    _repository.Update(currentVaccineType);
                }
            }
            else
            {
                throw new ArgumentException(ErrorMessage.NotFound);
            }

            _repository.Save();
        }

        public IEnumerable<VaccineTypeDTO> FindByName(string name)
        {
            var vaccineTypes = _repository
                .FindList(vt => vt.VaccineTypeName.Contains(name))
                .Select(vt => vt.IsDeleted == false)
                .ToList();
            var dtos = vaccineTypes.Count > 0 ? _mapper.Map<List<VaccineTypeDTO>>(vaccineTypes) : null;
            return dtos;
        }

        public void DeleteRange(List<VaccineTypeDTO> dtos)
        {
            var vaccineTypes = _mapper.Map<List<VaccineType>>(dtos);
            _repository.DeleteRange(vaccineTypes);

            _repository.Save();
        }

        public void DeleteRange(List<VaccineTypeDTO> dtos, bool isHardDelete)
        {
            foreach (var dto in dtos)
            {
                var vaccineType = _repository.GetById(dto.Id);

                if (vaccineType != null)
                {
                    if (isHardDelete)
                    {
                        _repository.Delete(vaccineType);
                    }
                    else
                    {
                        vaccineType.IsDeleted = true;
                        _repository.Update(vaccineType);
                    }
                }
            }

            _repository.Save();
        }

        public IEnumerable<VaccineTypeDTO> GetAll()
        {
            var vaccineTypes = _repository
                .GetAll()
                .Where(vt => vt.IsDeleted == false)
                .ToList();
            var dtos = _mapper.Map<List<VaccineTypeDTO>>(vaccineTypes);

            return dtos;
        }

        public VaccineTypeDTO GetById(int id)
        {
            var vaccineType = _repository.GetById(id);
            var dto = _mapper.Map<VaccineTypeDTO>(vaccineType);

            return dto;
        }

        public void Update(VaccineTypeDTO dto)
        {
            var vaccineType = _repository.GetById(dto.Id);

            if (vaccineType != null)
            {
                _mapper.Map(dto, vaccineType);
                _repository.Update(vaccineType);
            }
            else
            {
                throw new ArgumentException(ErrorMessage.NotFound);
            }

            _repository.Save();
        }

        public IEnumerable<VaccineTypeDTO> Search(string? keyword)
        {
            keyword = keyword.IsNullOrEmpty() ? string.Empty : keyword;
            var vaccineTypes = _repository
                .FindList(vt => vt.VaccineTypeName.Contains(keyword) || vt.VaccineTypeCode.Contains(keyword))
                .Where(vt => vt.IsDeleted == false)
                .ToList();
            var dtos = vaccineTypes.Count > 0 ? _mapper.Map<List<VaccineTypeDTO>>(vaccineTypes) : null;
            return dtos;
        }

        public void Deactivate(VaccineTypeDTO dto)
        {
            var vaccineType = _repository.GetById(dto.Id);

            if (vaccineType != null)
            {
                vaccineType.IsActive = false;
                _repository.Update(vaccineType);
            }
            //else
            //{
            //    throw new ArgumentException(ErrorMessage.NotFound);
            //}

            _repository.Save();
        }

        public void DeactivateRange(List<VaccineTypeDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                var vaccineType = _repository.GetById(dto.Id);

                if (vaccineType != null)
                {
                    vaccineType.IsActive = false;
                    _repository.Update(vaccineType);
                }
            }

            _repository.Save();
        }
    }
}