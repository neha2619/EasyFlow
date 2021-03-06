using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository
    {
        void CreateCompany(company company);
        void UpdateCompany(company company);
        void DeleteCompany(company company);

        IEnumerable<company> GetAllCompanies(bool trackChanges);
        company GetCompanyFromId(Guid CompanyId, bool trackChanges);
        company GetCompanyFromName(string CompanyName, bool trackChanges);
        company GetCompanyFromEmail(string CompanyEmail, bool trackChanges);
        company GetCompanyPasswordFromEmail(string CompanyEmail, bool trackChanges);
        company GetCompanyPasswordFromMobile(string CompanyMobile, bool trackChanges);
        
        company GetCompanyFromMobile(string CompanyMobile, bool trackChanges);
        int CountAllCompanies(bool trackChanges);

        IEnumerable<company> GetCompaniesByCreatedOn(bool trackChanges);


    }
}
