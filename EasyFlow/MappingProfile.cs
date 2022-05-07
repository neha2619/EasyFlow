using Entities.DataTransferObjects;
using Entities.Models;
using AutoMapper;


namespace EasyFlow
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<company, CompanyDto>()
   .ForMember(c => c.CompanyFullAddress,
   opt => opt.MapFrom(x => string.Join(' ', x.CompanyDistrict, x.CompanyState)));

            CreateMap<company, CompanyRegistrationDto>();

            CreateMap<Worker, WorkerDto>();
            CreateMap<Admin, AdminDto>();
            CreateMap<CompanyForCreationDto, company>();

            CreateMap<AdminForRegistrationDto, Admin>();
            CreateMap<WorkerForRegistrationDto, Worker>();
           CreateMap< CompaniesRequestsDto, AdminCompany>();
           CreateMap< AdminCompany, CompaniesRequestsDto>();

            CreateMap<WorkerUpdateDto, Worker>();
             CreateMap<WorkerUpdateDto, Worker>().ReverseMap();

            CreateMap<CompanyUpdateDto, company>();
            CreateMap<CompanyUpdateDto, company>().ReverseMap();

            CreateMap<AdminUpdateDto, Admin>(); 
            CreateMap<AdminUpdateDto, Admin>().ReverseMap();

        }
    }
}
