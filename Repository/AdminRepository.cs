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
    public class AdminRepository : RepositoryBase<Admin>, IAdminRepository

    {
        public AdminRepository(RepositoryContext repositoryContext)
 : base(repositoryContext)
        {
        }
        //Ignore this message ..
        public void CreateAdmin(Admin admin)=> Create(admin);

        public IEnumerable<Admin> GetAllAdmin(bool trackChanges) =>
 FindAll(trackChanges)
 .OrderBy(c => c.Name)
 .ToList();

        public Admin GetAdminPasswordFromEmail(string AdminEmail, bool trackChanges) =>
FindByCondition(c => c.Email.Equals(AdminEmail), trackChanges)
.SingleOrDefault();
        public Admin GetAdminPasswordFromMobile(string AdminMobile, bool trackChanges) =>
FindByCondition(c => c.Mobile.Equals(AdminMobile), trackChanges)
.SingleOrDefault();
        public Admin GetAdminFromMobile(string AdminMobile, bool trackChanges) =>
FindByCondition(c => c.Mobile.Equals(AdminMobile), trackChanges)
.SingleOrDefault();


    }
}