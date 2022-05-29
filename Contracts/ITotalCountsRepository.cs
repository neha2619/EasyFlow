using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITotalCountsRepository :IRepositoryBase<TotalCounts>
    {
        void CreateCounts(TotalCounts counts);
        IEnumerable<TotalCounts> GetAllCounts(bool trackChanges);
    }
}
