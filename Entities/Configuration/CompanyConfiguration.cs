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
    public class CompanyConfiguration : IEntityTypeConfiguration<company>

    {
        public void Configure(EntityTypeBuilder<company> builder)
        {
            builder.HasData
 (
 new company
 {
     Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
     CompanyName = "IT_Solutions Ltd",
     CompanyMail = "ITsolution@gmail.com",
     CompanyMobile = "8318692776",
     CompanyCin="123456789012345678901",
     CompanyGstin="123456789012345",
     CompanyPass="fsdgvdsg",
     CompanyType="Hotel",
     CompanyState="Uttar Pradesh",
     CompanyDistrict="Lucknow",
     CompanyArea="Ashiyana",
     CompanySubArea="Sector o",
     WorkerNumber="90",
     SiteLocation="sector o, sector p, sector d,sector f",
     CreatedOn="24.04.2022",
     UpdatedOn="12.05.2022",
     KYCStatus="Yes"

 },
 new company
 {
     Id = new Guid("3d490n70-94ce-4d15-9494-5248280c2ce3"),
     CompanyName = "Admin_Solutions Ltd",
     CompanyMail = "Adminsolution@gmail.com",
     CompanyMobile = "8318692776",
     CompanyCin = "123456789012345672341",
     CompanyGstin = "123456788792345",
     CompanyPass = "fsdedgvdsg",
     CompanyType = "Construction",
     CompanyState = "Uttar Pradesh",
     CompanyDistrict = "Lucknow",
     CompanyArea = "HHazratganj",
     CompanySubArea = "Rana Pratap Marg",
     WorkerNumber = "90",
     SiteLocation = "Rana Pratap Marg, sector p, sector d,sector f",
     CreatedOn = "14.04.2022",
     UpdatedOn = "2.05.2022",
     KYCStatus = "No"
 }
 );
        }
    }
}
