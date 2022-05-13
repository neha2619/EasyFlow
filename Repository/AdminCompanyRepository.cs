using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AdminCompanyRepository : RepositoryBase<AdminCompany>, IAdminCompanyRepository

    {
        public AdminCompanyRepository(RepositoryContext repositoryContext)
 : base(repositoryContext)
        {
        }

        public void CreateRequest(AdminCompany adminrequest) => Create(adminrequest);
        public void DeleteRequest(AdminCompany companiesRequest) => Delete(companiesRequest);

        public IEnumerable<AdminCompany> GetAllRequest(string workerType, bool trackChanges) =>
FindByCondition(c => c.WorkerType.Equals(workerType), trackChanges).OrderBy(c => c.WorkerType)
 .ToList();
        public IEnumerable<AdminCompany> GetRequestsForWorkerTypeByCompanyId(Guid companyId, string workerType, bool trackChanges) => FindByCondition(c => c.CompanyId.Equals(companyId) && c.WorkerType.Equals(workerType), trackChanges).OrderBy(c => c.WorkerType).ToList(); 
        public IEnumerable<AdminCompany> GetRequestsForWorkerTypeLocation(string location, string workerType, bool trackChanges) => FindByCondition(c => c.Location.Equals(location) && c.WorkerType.Equals(workerType), trackChanges).OrderBy(c => c.WorkerType).ToList();
        public IEnumerable<AdminCompany>GetRequestsByCompanyId(Guid userId,bool trackChanges) =>FindByCondition(c =>c.CompanyId.Equals(userId)  ,trackChanges).OrderBy(c => c.WorkerType).ToList();


    }
}
