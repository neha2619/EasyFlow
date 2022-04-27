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

        }
    }
}
