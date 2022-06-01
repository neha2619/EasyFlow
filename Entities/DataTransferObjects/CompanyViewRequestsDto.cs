using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CompanyViewRequestsDto
    {
        public int Serial { get; set; }
        public string WorkerName { get; set; }
        public string email { get; set; }//this is workeremail
        public string Mobile { get; set; }//this is workermobile
        public string WorkerType { get; set; }
        public string Location { get; set; }
    }
}
