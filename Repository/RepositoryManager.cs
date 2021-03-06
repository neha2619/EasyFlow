using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IWorkerRepository _workerRepository;
        private IAdminCompanyRepository _admincompanyRepository;
        private IAdminWorkerRepository _adminworkerRepository;
        private IAdminRepository _adminRepository;
        private IOTPsRepository _otPsRepository;
        private ICompanyReqRepository _companyReq;
        private ITimestampsRepository _stampsRepository;
        private ITotalCountsRepository _totalCountsRepository;
        private IWorkerReqRepository _workerReqRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public ICompanyRepository company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;
            }
        }

        public IWorkerRepository Worker
        {
            get
            {
                if (_workerRepository == null)
                    _workerRepository = new WorkerRepository(_repositoryContext);
                return _workerRepository;
            }
        }
        public IAdminRepository Admin
        {
            get
            {
                if (_adminRepository == null)
                    _adminRepository = new AdminRepository(_repositoryContext);
                return _adminRepository;
            }
        }
        public IAdminWorkerRepository AdminWorker
        {
            get
            {
                if (_adminworkerRepository == null)
                    _adminworkerRepository = new AdminWorkerRepository(_repositoryContext);
                return _adminworkerRepository;
            }
        }
        public IAdminCompanyRepository AdminCompany
        {
            get
            {
                if (_admincompanyRepository == null)
                    _admincompanyRepository = new AdminCompanyRepository(_repositoryContext);
                return _admincompanyRepository;
            }
        }
        public IOTPsRepository oTPs
        {
            get
            {
                if (_otPsRepository == null)
                    _otPsRepository = new OTPsRepository(_repositoryContext);
                return _otPsRepository;
            }
        } 
        public ICompanyReqRepository CompanyReq
        {
            get
            {
                if (_companyReq == null)
                    _companyReq = new CompanyReqRepository(_repositoryContext);
                return _companyReq;
            }
        }
        public ITimestampsRepository Timestamps
        {
            get
            {
                if(_stampsRepository == null)
                    _stampsRepository = new TimestampsRepository(_repositoryContext);
                return _stampsRepository;

            }
        }

        public ITotalCountsRepository TotalCounts
        {
            get
            {
                if(_totalCountsRepository==null)
                    _totalCountsRepository = new TotalCountsRepository(_repositoryContext);
                return (_totalCountsRepository);
            }
        }

        public IWorkerReqRepository WorkerReq
        {
            get
            {
                if (_workerReqRepository == null)
                    _workerReqRepository = new WorkerReqRepository(_repositoryContext);
                return(_workerReqRepository);
            }
        }
        public void Save() => _repositoryContext.SaveChanges();

    }
}
