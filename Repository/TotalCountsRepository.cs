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
    public class TotalCountsRepository : RepositoryBase<TotalCounts>, ITotalCountsRepository
    {
        public TotalCountsRepository(RepositoryContext repositoryContext):base(repositoryContext)
        {

        }
        public  void CreateCounts(TotalCounts counts) =>Create(counts);
        public IEnumerable<TotalCounts> GetAllCounts(bool trackChanges)=>FindAll(trackChanges).ToList();
    }
}
