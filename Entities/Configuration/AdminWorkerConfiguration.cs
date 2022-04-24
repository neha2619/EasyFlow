using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class AdminWorkerConfiguration : IEntityTypeConfiguration<AdminWorker>

    {
        public void Configure(EntityTypeBuilder<AdminWorker> builder)
        {
            builder.HasData
            (

                new AdminWorker
                {
                    Id = new Guid("80abbcz1-664d-4b20-b5de-024705497d4a"),
                    WorkerType = "Carpenter",
                    Location = "Hazaratganj",
                    WorkerId = new Guid("86dba8c7-d178-41e7-938c-ed49778fb52a")
                }




                );
        }

    }
    }
