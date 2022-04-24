using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
 : base(options)
        {
        }
        public DbSet<company> Companies { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AdminWorker> AdminWorkers { get; set; }
        public DbSet<AdminCompany> AdminCompanyies { get; set; }
    }
}
