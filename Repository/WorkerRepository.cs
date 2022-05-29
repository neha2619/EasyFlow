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
    public class WorkerRepository : RepositoryBase<Worker>, IWorkerRepository

    {
        public WorkerRepository(RepositoryContext repositoryContext)
 : base(repositoryContext)
        {
        }
        public IEnumerable<Worker> GetAllWorkers(bool trackChanges) =>
 FindAll(trackChanges)
 .OrderBy(c => c.WorkerName)
 .ToList();

        public int CountAllWorkers(bool trackChanges) =>
FindAll(trackChanges).Count();

        public Worker GetWorkerPasswordFromMobile(string WorkerMobile, bool trackChanges) =>
 FindByCondition(c => c.WorkerMobile.Equals(WorkerMobile), trackChanges)
 .SingleOrDefault();
        public Worker GetWorkerFromID(Guid ID, bool trackChanges) =>
 FindByCondition(c => c.Id.Equals(ID), trackChanges)
 .SingleOrDefault();
        public IEnumerable<Worker> GetWorkerFromType(string WorkerType, bool trackChanges) =>
 FindByCondition(c => c.WorkerType.Equals(WorkerType), trackChanges).OrderBy(c=>c.WorkerType).ToList();
        public Worker[] GetWorkerFromTypeAndLocation(string WorkerType, string Location, bool trackChanges) =>
         FindByCondition(c => c.WorkerType.Equals(WorkerType) && c.LocationPreference.Equals(Location), trackChanges).OrderBy(c => c.WorkerType).ToArray();

        public Worker GetWorkerPasswordFromEmail(string WorkerEmail, bool trackChanges) =>
 FindByCondition(c => c.WorkerMail.Equals(WorkerEmail), trackChanges)
 .SingleOrDefault();

        public Worker GetWorkerFromMobile(string WorkerMobile, bool trackChanges) =>
 FindByCondition(c => c.WorkerMobile.Equals(WorkerMobile), trackChanges)
 .SingleOrDefault();

        public Worker GetWorkerFromId(Guid WorkerId, bool trackChanges)=>
            FindByCondition(c =>c.Id.Equals(WorkerId), trackChanges).SingleOrDefault();

        public IEnumerable<Worker> GetTopRatedWorker(bool trackChanges)=>FindAll(trackChanges).OrderByDescending(c=>c.Ratings).ToList();

        public IEnumerable<Worker>GetWorkersByCreatedOn(bool trackChanges) =>FindAll(trackChanges).OrderBy(c=>c.CreatedOn).ToList();

        public void AddWorker(Worker  worker) => Create(worker);
        
    }
}
