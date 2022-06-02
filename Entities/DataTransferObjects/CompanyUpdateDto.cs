using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CompanyUpdateDto
    {
        public string CompanyName { get; set; }
        public string CompanyMobile { get; set; }
        //public string CompanyMail { get; set; }
        public string CompanyCin { get; set; }
        public string CompanyGstin { get; set; }
        public string CompanyPass { get; set; }
        public string UpdatedOn { get; set; }
        //public string KYCStatus { get; set; }
        //public string CompanyDistrict { get; set; }
        //public string CompanyState { get; set; }
        //public string CompanyArea { get; set; }
        //public string CompanySubArea { get; set; }
    }
}
