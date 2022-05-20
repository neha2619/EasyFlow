using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class GetRequestDetailsFromCompanyDto
    {
        //GetRequestDetailsFromCompanyForWorkerDto
        public Guid CompanyId { get; set; }
        public string  workerType { get; set; }
        public string location { get; set; }
        public string vacancy { get; set; }
        public string createdOn { get; set; }
    }
}
