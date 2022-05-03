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
        IEnumerable<company> GetAllCompanies(bool trackChanges);
        company GetCompanyFromId(string CompanyId, bool trackChanges);
        company GetCompanyFromName(string CompanyName, bool trackChanges);
       
        company GetCompanyPasswordFromEmail(string CompanyEmail, bool trackChanges);
        company GetCompanyPasswordFromMobile(string CompanyMobile, bool trackChanges);
        void CreateCompany(company company);
        company GetCompanyFromMobile(string CompanyMobile, bool trackChanges);
    }
}
