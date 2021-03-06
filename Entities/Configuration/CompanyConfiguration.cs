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
     Id = new Guid("2A5DDAF5-D15B-4976-93A1-41E8F79AECCC"),
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
     Id = new Guid("C86111FD-8827-4435-A601-404F9A213C23"),
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
