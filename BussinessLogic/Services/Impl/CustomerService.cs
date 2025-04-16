using AutoMapper;
using Vaccination.BussinessLogic.DTOs.CustomerDTOs;
using Vaccination.DataAccess.Models;
using Vaccination.DataAccess.Repositories;

namespace Vaccination.BussinessLogic.Services.Impl
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public void Add(CustomerDTO entity)
        {
            entity.ID = 0;
            // Ánh xạ từ DTO sang entity
            var customer = _mapper.Map<Customer>(entity);

            // Thêm vào repository và lưu thay đổi
            _customerRepository.Add(customer);
            _customerRepository.Save();
        }

        public void Delete(CustomerDTO entity)
        {
            var customer = _mapper.Map<Customer>(entity);
            if (customer.IsDeleted != true)
            {
                customer.IsDeleted = true;
            }
            // Xóa khỏi repository và lưu thay đổi
            _customerRepository.Update(customer);
            _customerRepository.Save();
        }

        public void DeleteRange(List<CustomerDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            // Lấy danh sách khách hàng từ repository
            var customers = _customerRepository.GetAll();

            // Ánh xạ từ danh sách entity sang danh sách DTO
            return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
        }

        public CustomerDTO GetById(int id)
        {
            var customer = _customerRepository.GetById(id);

            // Nếu không tìm thấy, trả về null
            if (customer == null)
            {
                return null;
            }

            // Ánh xạ từ entity sang DTO và trả về
            return _mapper.Map<CustomerDTO>(customer);
        }

        public IEnumerable<CustomerDTO> GetCustomerDTOsNotDeleted()
        {
            var result = _customerRepository.GetAll().Where(x => x.IsDeleted != true).ToList();
            return _mapper.Map<IEnumerable<CustomerDTO>>(result);
        }

        public IEnumerable<CustomerDTO> Search(string? keyword)
        {
            var result = _customerRepository.GetAll().Where(x => x.FullName.ToLower().Contains(keyword.ToLower()) && x.IsDeleted != true);
            return _mapper.Map<IEnumerable<CustomerDTO>>(result);
        }

        public void Update(CustomerDTO entity)
        {
            var existingCustomer = GetById(entity.ID);
            if (existingCustomer == null)
            {
                throw new ArgumentException($"Customer with Id = {entity.ID} not found.");
            }
            // Ánh xạ từ DTO sang entity

            var customer = _mapper.Map<Customer>(entity);

            // Cập nhật thông tin trong repository và lưu thay đổi
            _customerRepository.Update(customer);
            _customerRepository.Save();
        }
    }
}
