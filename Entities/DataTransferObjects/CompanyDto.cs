using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyMobile { get; set; }
        public string CompanyMail { get; set; }
        public string CompanyCIN{ get; set; }
        public string CompanyGstin { get; set; }
        public string SiteLocation { get; set; }
    }
}
