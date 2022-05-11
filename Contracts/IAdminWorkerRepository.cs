using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAdminWorkerRepository
    {
        void CreateRequest(AdminWorker workersRequest);
        IEnumerable<AdminWorker> GetRequestsByWorkerId(Guid workerId, bool trackChanges);
        IEnumerable<AdminWorker> GetAllRequestByWorkerType(string workerType, bool trackChanges);
        AdminWorker GetAllRequestByCreatedOn(   bool trackChanges);
    }
}
