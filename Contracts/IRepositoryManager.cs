using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository company { get; }
        IWorkerRepository Worker { get; }
        IAdminRepository Admin { get; }
        IWorkerReqRepository WorkerReq { get; }
        IAdminWorkerRepository AdminWorker { get; }
        IAdminCompanyRepository AdminCompany { get; }
        ICompanyReqRepository CompanyReq { get; }
        IOTPsRepository oTPs { get; }
        ITimestampsRepository Timestamps { get; }
        ITotalCountsRepository TotalCounts { get; }
        
        void Save();

    }
}
