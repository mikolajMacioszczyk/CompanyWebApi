using AutoMapper;
using Company.ASP.Dtos;
using Company.ASP.Models;

namespace Company.ASP.Profiles
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Employee, EmployeeReadDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<Models.Company, CompanyReadDto>()
                .ForMember(dest => dest.Employees,
                    opt => opt.MapFrom(src => src.Employees));
            CreateMap<CompanyCreateDto, Models.Company>()
                .ForMember(dest => dest.Employees,
                    opt => opt.MapFrom(src => src.Employees));
        }
    }
}