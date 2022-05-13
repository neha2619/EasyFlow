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
    public class CompanyReqRepository : RepositoryBase<CompanyReq>, ICompanyReqRepository
    {
        public CompanyReqRepository(RepositoryContext repositoryContext)  : base(repositoryContext)
        {

        }
        public void CreateCompanyRequest(CompanyReq companyReq)=> Create(companyReq);
        public IEnumerable<CompanyReq> GetAllSuggestedWorkers(Guid companyId, bool trackChanges) =>FindAll(trackChanges).OrderBy(x => x.CompanyId).ToList();
    }
}
