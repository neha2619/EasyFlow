using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CompanyForCreationDto
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        //public string CompanyType { get; set; }
        //public string CompanyCin { get; set; }
        //public string CompanyGstin { get; set; }
        public string CompanyMobile { get; set; }
        public string CompanyMail { get; set; }
        public string CompanyPass { get; set; }
    }
}
