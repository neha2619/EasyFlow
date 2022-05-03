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
        Worker GetWorkerPasswordFromEmail(string WorkerEmail, bool trackChanges);
        Worker GetWorkerFromMobile(string WorkerMobile, bool trackChanges);


    }
}
