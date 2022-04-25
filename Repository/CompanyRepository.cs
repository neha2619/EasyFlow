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
    }

}
