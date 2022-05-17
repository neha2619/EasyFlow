﻿using System;
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
        IAdminWorkerRepository AdminWorker { get; }
        IAdminCompanyRepository AdminCompany { get; }
        ICompanyReqRepository CompanyReq { get; }
        IOTPsRepository oTPs { get; }
        ITimestampsRepository Timestamps { get; }
        
        void Save();

    }
}
