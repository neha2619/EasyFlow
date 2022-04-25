using Entities.Configuration;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdminCompanyConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new WorkerConfiguration());
            modelBuilder.ApplyConfiguration(new AdminWorkerConfiguration());
        }


        public DbSet<company> Companies { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AdminWorker> AdminWorkers { get; set; }
        public DbSet<AdminCompany> AdminCompanyies { get; set; }
    }
}
