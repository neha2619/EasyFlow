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
        IEnumerable<AdminWorker> GetAllRequest(bool trackChanges);
        IEnumerable<AdminWorker> GetRequestsByWorkerId(Guid workerId, bool trackChanges);
        IEnumerable<AdminWorker> GetAllRequestByWorkerType(string workerType, bool trackChanges);
        IEnumerable<AdminWorker> GetAllRequestByCreatedOn( string workerType,  bool trackChanges);
        AdminWorker GetWorkerByTimestamp(string timestamp, bool trackChanges);
        void DeleteWorker(AdminWorker adminWorker) ;
    }
}
