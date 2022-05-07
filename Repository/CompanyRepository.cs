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
    public class CompanyRepository : RepositoryBase<company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        public IEnumerable<company> GetAllCompanies(bool trackChanges) =>
 FindAll(trackChanges)
 .OrderBy(c => c.CompanyName)
 .ToList();

        public void CreateCompany(company company) => Create(company);


        public company GetCompanyFromId(Guid CompanyID, bool trackChanges) =>
 FindByCondition(c => c.Id.Equals(CompanyID), trackChanges)
 .SingleOrDefault();

        public company GetCompanyFromName(string CompanyName, bool trackChanges) =>
 FindByCondition(c => c.CompanyName.Equals(CompanyName), trackChanges)
 .SingleOrDefault();
        public company GetCompanyFromEmail(string CompanyEmail, bool trackChanges) =>
 FindByCondition(c => c.CompanyMail.Equals(CompanyEmail), trackChanges)
 .SingleOrDefault();
        public company GetCompanyPasswordFromEmail(string CompanyEmail, bool trackChanges) =>
 FindByCondition(c => c.CompanyMail.Equals(CompanyEmail), trackChanges)
 .SingleOrDefault();  
        public company GetCompanyPasswordFromMobile(string CompanyMobile, bool trackChanges) =>
 FindByCondition(c => c.CompanyMobile.Equals(CompanyMobile), trackChanges)
 .SingleOrDefault();

        public company GetCompanyFromMobile(string CompanyMobile, bool trackChanges) =>
 FindByCondition(c => c.CompanyMobile.Equals(CompanyMobile), trackChanges)
 .SingleOrDefault();
    }
}
