using Entities.DataTransferObjects;
using Entities.Models;
using AutoMapper;


namespace EasyFlow
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<company, CompanyDto>();

           
            CreateMap<company, CompanyRegistrationDto>();

            CreateMap<Worker, WorkerDto>();
            CreateMap<WorkerDto,Worker>();
            
            

            CreateMap<Admin, AdminDto>();
            CreateMap<CompanyForCreationDto, company>();

            CreateMap<AdminForRegistrationDto, Admin>();
            CreateMap<WorkerForRegistrationDto, Worker>();
           CreateMap< CompaniesRequestsDto, AdminCompany>();
           CreateMap< AdminCompany, CompaniesRequestsDto>();

            CreateMap<WorkerRequestToCompanyDto, AdminWorker>();
            CreateMap< AdminWorker , WorkerRequestToCompanyDto>();

            CreateMap<WorkerUpdateDto, Worker>();
             CreateMap<WorkerUpdateDto, Worker>().ReverseMap();

            CreateMap<CompanyUpdateDto, company>();
            CreateMap<CompanyUpdateDto, company>().ReverseMap();

            CreateMap<AdminUpdateDto, Admin>(); 
            CreateMap<AdminUpdateDto, Admin>().ReverseMap();
            CreateMap<OTPs,OTPs>();

           CreateMap<CompanyReq, CompanyReq>();


            CreateMap<CompanyReq,SuggestedWorkersForCompany>();

            CreateMap<Admin,ChangePasswordDto>();
            CreateMap<ChangePasswordDto, Admin>().ReverseMap();
            

        }
    }
}
