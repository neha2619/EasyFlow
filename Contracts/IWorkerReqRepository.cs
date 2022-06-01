using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IWorkerReqRepository
    {
        void CreateWorkerRequest(WorkerReq workerReq);

       IEnumerable<WorkerReq> GetWorkerRequestsByWorkerId(string workerId,bool trackChanges);
    }
}
