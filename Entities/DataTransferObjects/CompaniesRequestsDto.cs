using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CompaniesRequestsDto
    {
        public Guid CompanyID {get ;set;}
        public string WorkerType { get; set; }
        public string Location { get; set; }
        public string Vacancy { get; set; }
        public string RequestState { get; set; }
        public string CreatedOn { get; set; }
    }
}
