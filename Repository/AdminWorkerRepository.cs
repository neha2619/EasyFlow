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
    public class AdminWorkerRepository : RepositoryBase<AdminWorker>, IAdminWorkerRepository

    {
        public AdminWorkerRepository(RepositoryContext repositoryContext)
 : base(repositoryContext)
        {
        }
        public void CreateRequest(AdminWorker workersRequest) => Create(workersRequest);

    }
}
