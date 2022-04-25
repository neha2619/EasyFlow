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
                    Id = new Guid("46A32C93-1663-447F-8801-DB7DB9C42719"),
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
                     Id = new Guid("76ED4298-C549-465B-AF8F-A80ABAE08616") ,
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
