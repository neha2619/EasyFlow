using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IWorkerRepository :IRepositoryBase<Worker> 
    {
        IEnumerable<Worker> GetAllWorkers(bool trackChanges);
        void AddWorker(Worker worker);
        Worker GetWorkerPasswordFromMobile(string WorkerMobile, bool trackChanges);
        IEnumerable<Worker> GetWorkerFromType(string WorkerType, bool trackChanges);
        Worker GetWorkerPasswordFromEmail(string WorkerEmail, bool trackChanges);
        Worker GetWorkerFromMobile(string WorkerMobile, bool trackChanges);
        Worker GetWorkerFromId(Guid workerId, bool trackChanges);
        int CountAllWorkers(bool trackChanges);
        Worker[] GetWorkerFromTypeAndLocation(string WorkerType, string Location, bool trackChanges);
        IEnumerable<Worker> GetTopRatedWorker( bool trackChanges);
        IEnumerable<Worker> GetWorkersByCreatedOn(bool trackChanges);

    }
}
