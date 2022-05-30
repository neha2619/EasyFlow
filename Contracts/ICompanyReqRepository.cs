using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyReqRepository
    {

        void CreateCompanyRequest(CompanyReq companyReq);
        IEnumerable<CompanyReq> GetAllSuggestedCompany(Guid workerId, bool trackChanges);
        IEnumerable<CompanyReq> GetAllSuggestedWorkers(Guid companyId, bool trackChanges);
        IEnumerable<CompanyReq> GetLatestSuggestions(int count,bool  trackChanges);
        IEnumerable<CompanyReq> GetAllSuggestedWorkersByCompanyID(string companyId, bool trackChanges);
    }
}
