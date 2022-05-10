using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAdminCompanyRepository
    {
       void CreateRequest(AdminCompany companiesRequest);
        IEnumerable<AdminCompany> GetAllRequest(string workerType,bool trackChanges);
        IEnumerable<AdminCompany> GetRequestsByCompanyId(Guid companyId, bool trackChanges);
        IEnumerable<AdminCompany> GetRequestsForWorkerTypeByCompanyId(Guid companyId, string WorkerType, bool trackChanges);
        IEnumerable<AdminCompany> GetRequestsForWorkerTypeLocation(string location, string workerType, bool trackChanges);
    }
}
