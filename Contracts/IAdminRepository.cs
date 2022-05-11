using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAdminRepository : IRepositoryBase<Admin>
    {
        IEnumerable<Admin> GetAllAdmin(bool trackChanges);
        Admin GetAdminPasswordFromEmail(string AdminEmail, bool trackChanges);
        Admin GetAdminPasswordFromMobile(string AdminMobile, bool trackChanges);


        void CreateAdmin(Admin admin);

        Admin GetAdminFromMobile(string Mobile, bool trackChanges);
    }
}