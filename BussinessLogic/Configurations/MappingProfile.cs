using AutoMapper;
using Vaccination.BussinessLogic.DTOs.CustomerDTOs;
using Vaccination.BussinessLogic.DTOs.EmployeeDTOs;
using Vaccination.BussinessLogic.DTOs.ImageDTOs;
using Vaccination.BussinessLogic.DTOs.InjectionResultDTO;
using Vaccination.BussinessLogic.DTOs.InjectionScheduleDTOs;
using Vaccination.BussinessLogic.DTOs.NewsDTOs;
using Vaccination.BussinessLogic.DTOs.NewsTypeDTOs;
using Vaccination.BussinessLogic.DTOs.VaccineDTOs;
using Vaccination.BussinessLogic.DTOs.VaccineTypeDTOs;
using Vaccination.DataAccess.Models;
using Vaccination.BussinessLogic.DTOs.ReportDTOs;

namespace Vaccination.BussinessLogic.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Customer and CustomerDTOs

            // Employee and EmployeeDTOs
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Employee, EmployeeNoValidateDTO>().ReverseMap();

            // InjectionResult and InjectionResultDTOs
            // InjectionResult and InjectionResultDTOs
            CreateMap<InjectionResult, InjectionResultDTO>().ReverseMap();
            CreateMap<Customer, InjectionResultCustomerDTO>();
            CreateMap<InjectionResultVaccineRequest, InjectionResult>();
            CreateMap<Vaccine, InjectionResultVaccineDTO>()
                 .ForMember(des => des.VaccineTypeName, act => act.MapFrom(src => src.VaccineType.VaccineTypeName));


            // InjectionSchedule and InjectionDTOs
            CreateMap<InjectionSchedule, InjectionScheduleDTOs>()
    .ForMember(dest => dest.VaccineName, opt => opt.MapFrom(src => src.Vaccine.VaccineName))
    .ForMember(dest => dest.Vaccine, opt => opt.Ignore())
    .ReverseMap()
    .ForMember(dest => dest.Vaccine, opt => opt.Ignore());

            // News and NewDTOs
            CreateMap<News, NewDTO>().ReverseMap();

            // NewsType and NewTypeDTOs
            CreateMap<NewsType, NewTypesDTO>().ReverseMap();

            // Vaccine and VaccineDTOs
            CreateMap<Vaccine, VaccineDTO>()
                .ForMember(dest => dest.VaccineTypeDTO, opt => opt.MapFrom(src => src.VaccineType)).ReverseMap();

            // VaccineType and VaccineTypeDTOs
            CreateMap<VaccineType, VaccineTypeDTO>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember)
                => srcMember != null && (!srcMember.Equals(destMember))));
            CreateMap<VaccineTypeDTO, VaccineType>()
                .ForMember(dest => dest.Id, opt => opt.Condition(src => src.Id != 0))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember)
                => srcMember != null && (!srcMember.Equals(destMember))));
            //CreateMap<List<VaccineType>, List<VaccineTypeDTO>>();

            // Customer and CustomerDTO
            CreateMap<CustomerDTO, Customer>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToLower() == "true" ? true : false));

            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.Value ? "true" : "false"));
            CreateMap<Customer, CustomerDTO>().ReverseMap();

            CreateMap<ImageDTO, Image>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember, destMember)
                => srcMember != null && (!srcMember.Equals(destMember))));

            CreateMap<Image, ImageDTO>();
        }
    }
}
