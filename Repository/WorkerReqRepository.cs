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
        public IEnumerable<WorkerReq> GetWorkerRequestsByWorkerId(string workerId, bool trackChanges)=>FindByCondition(c=>c.WorkerId.Equals(workerId), trackChanges).OrderBy(c=>c.CompanyName).ToList();


    }
}
