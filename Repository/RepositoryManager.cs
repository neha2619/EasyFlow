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
        public void Save() => _repositoryContext.SaveChanges();

    }
}
