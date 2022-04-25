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
                    Id = new  Guid("A3347540-C60D-45F4-8ADE-AF0A189397A3"),
                    Location = "sector o",
                    WorkerType = "Plumber",
                    Vacancy = "5",
                    CompanyId = new Guid("2A5DDAF5-D15B-4976-93A1-41E8F79AECCC")
                },
                 new AdminCompany
                 {
                     Id = new Guid("76C00047-4526-48A9-9947-EA2BE4133E64"),
                     Location = "sector p",
                     WorkerType = "Carpenter",
                     Vacancy = "7",
                     CompanyId = new Guid("C86111FD-8827-4435-A601-404F9A213C23")
                 }

                );
        }
    }
}
