using Microsoft.Extensions.DependencyInjection;
using Vaccination.BussinessLogic.DTOs.InjectionScheduleDTOs;
using Vaccination.BussinessLogic.DTOs.NewsDTOs;
using Vaccination.BussinessLogic.DTOs.VaccineDTOs;
using Vaccination.BussinessLogic.Services;
using Vaccination.BussinessLogic.Services.Impl;
using Vaccination.DataAccess.Models;
using Vaccination.DataAccess.Repositories;
using Vaccination.DataAccess.Repositories.Impl;
using Vaccination.DataAccess.Repositories.VaccineRepository;

namespace Vaccination.BussinessLogic.Configurations
{
    public static class Register
    {
        public static void ServiceRegisters(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IBaseService<VaccineDTO>, VaccineService>();
            services.AddScoped<IVaccineTypeService, VaccineTypeService>();
            services.AddScoped<IBaseService<NewDTO>, NewsService>();
            services.AddScoped<IVaccineService, VaccineService>();
            services.AddScoped<IInjectionResultService, InjectionResultService>();
            services.AddScoped<IBaseService<InjectionScheduleDTOs>, InjectionScheduleService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IImageService, ImageService>();

        }
        public static void RepoRegisters(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IVaccineTypeRepository, VaccineTypeRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(DataAccess.Repositories.BaseRepository<>));
            services.AddScoped<IVaccineRepository, VaccineRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IInjectionScheduleRepository, InjectionScheduleRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IImageRepository,ImageRepository>();
        }
    }
}
