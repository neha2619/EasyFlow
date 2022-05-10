﻿using Contracts;
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
        public IEnumerable<AdminWorker> GetRequestsByWorkerId(Guid userId, bool trackChanges) => FindByCondition(c => c.WorkerId.Equals(userId), trackChanges).OrderBy(c => c.CreatedOn).ToList();
        public IEnumerable<AdminWorker> GetAllRequestByWorkerType(string workerType, bool trackChanges) =>
FindByCondition(c => c.WorkerType.Equals(workerType), trackChanges).OrderBy(c => c.WorkerType)
 .ToList();
        public AdminWorker GetAllRequestByCreatedOn( bool trackChanges) =>
FindAll(trackChanges).OrderBy(c => c.CreatedOn).First();
    }
}
