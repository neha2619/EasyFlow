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
    public class AdminCompanyConfiguration : IEntityTypeConfiguration<AdminCompany>
    {
        public void Configure(EntityTypeBuilder<AdminCompany> builder)
        {
            builder.HasData
            (
                new AdminCompany
                {
                    Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                    Location = "sector o",
                    WorkerType = "Plumber",
                    Vacancy = "5",
                    CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                },
                 new AdminCompany
                 {
                     Id = new Guid("80abbca9-664d-4b20-b5de-024705497d4a"),
                     Location = "sector p",
                     WorkerType = "Carpenter",
                     Vacancy = "7",
                     CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                 }

                );
        }
    }
}
