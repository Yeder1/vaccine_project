using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccination.BussinessLogic.DTOs.EmployeeDTOs;
using Vaccination.DataAccess.Models;
using Vaccination.DataAccess.Repositories;

namespace Vaccination.BussinessLogic.Services.Impl
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public void Add(EmployeeDTO entity)
        {
            entity.Id = 0;
            // Ánh xạ từ DTO sang entity
            var employee = _mapper.Map<Employee>(entity);

            // Thêm vào repository và lưu thay đổi
            _employeeRepository.Add(employee);
            _employeeRepository.Save();
        }

        public void Delete(EmployeeDTO entity)
        {
            var employee = _mapper.Map<Employee>(entity);
            if (employee.IsDeleted != true)
            {
                employee.IsDeleted = true;
            }
            // Xóa khỏi repository và lưu thay đổi
            _employeeRepository.Update(employee);
            _employeeRepository.Save();
        }

        public void DeleteNoValidateModel(EmployeeNoValidateDTO entity)
        {
            var employee = _mapper.Map<Employee>(entity);
            if (employee.IsDeleted != true)
            {
                employee.IsDeleted = true;
            }
            // Xóa khỏi repository và lưu thay đổi
            _employeeRepository.Update(employee);
            _employeeRepository.Save();
        }

        public void DeleteRange(List<EmployeeDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            // Lấy danh sách khách hàng từ repository
            var employees = _employeeRepository.GetAll().ToList();

            // Ánh xạ từ danh sách entity sang danh sách DTO
            return _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
        }

        public EmployeeDTO GetById(int id)
        {
            var employee = _employeeRepository.GetById(id);

            // Nếu không tìm thấy, trả về null
            if (employee == null)
            {
                return null;
            }

            // Ánh xạ từ entity sang DTO và trả về
            return _mapper.Map<EmployeeDTO>(employee);
        }

        public IEnumerable<EmployeeDTO> GetEmployeeDTOsNotDeleted()
        {
            var result = _employeeRepository.GetAll().Where(x => x.IsDeleted != true).ToList();
            return _mapper.Map<IEnumerable<EmployeeDTO>>(result);
        }

        public IEnumerable<EmployeeDTO> Search(string? keyword)
        {
            var result = _employeeRepository
                .GetAll()
                .Where(x => x.EmployeeName.ToLower().Contains(keyword.ToLower()) && x.IsDeleted != true)
                .ToList();
            return _mapper.Map<IEnumerable<EmployeeDTO>>(result);
        }

        public void Update(EmployeeDTO entity)
        {
            var existingEmployee = GetById(entity.Id);
            if (existingEmployee == null)
            {
                throw new ArgumentException($"Employee with Id = {entity.Id} not found.");
            }
            // Ánh xạ từ DTO sang entity

            var employee = _mapper.Map<Employee>(entity);

            // Cập nhật thông tin trong repository và lưu thay đổi
            _employeeRepository.Update(employee);
            _employeeRepository.Save();
        }
    }
}
