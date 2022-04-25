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
                    Id = new Guid("2D1ABE8A-A2DF-4394-A7C2-BE8DAD068519"),
                    WorkerType = "Carpenter",
                    Location = "Hazaratganj",
                    WorkerId = new Guid("76ED4298-C549-465B-AF8F-A80ABAE08616")
                }




                );
        }

    }
    }
