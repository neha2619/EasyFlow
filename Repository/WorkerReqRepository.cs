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
    public class WorkerReqRepository : RepositoryBase<WorkerReq> ,  IWorkerReqRepository
    {
        public WorkerReqRepository(RepositoryContext repositoryContext):base(repositoryContext)
        {

        }
        public void CreateWorkerRequest(WorkerReq workerReq) =>Create(workerReq);
    }
}
