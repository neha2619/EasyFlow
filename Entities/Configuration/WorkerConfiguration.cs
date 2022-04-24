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
    public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.HasData
            (
                new Worker
                {
                    Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                    WorkerName = "Sam Raiden",
                    WorkerMail = "Samraiden@gmail.com",
                    WorkerMobile = "8976543209",
                    WorkerAadhar = "123456789012",
                    CreatedOn = "24.04.2022",
                    UpdatedOn = "12.05.2022",
                    KYCStatus = "Yes",
                    WorkerPass ="hjhfdjgjgg",
                    WorkerType="Plumber",
                    LocationPreference="Alambag,charbag"




                },
                 new Worker
                 {
                     Id = new Guid("86dba8c7-d178-41e7-938c-ed49778fb52a"),
                     WorkerName = "Ron Raiden",
                     WorkerMail = "Ronraiden@gmail.com",
                     WorkerMobile = "8976786209",
                     WorkerAadhar = "120000789012",
                     CreatedOn = "21.04.2022",
                     UpdatedOn = "11.05.2022",
                     KYCStatus = "No",
                     WorkerPass = "hjhfdjsdsdgjgg",
                     WorkerType = "Carpenter",
                     LocationPreference = "Hazaratganj,Aashiyana"
                 }



                );

        }
    }
    }
