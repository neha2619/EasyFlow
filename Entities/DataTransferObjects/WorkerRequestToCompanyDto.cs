using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class WorkerRequestToCompanyDto
    {
        public Guid WorkerId { get; set; }
        public string WorkerName { get; set; }
        public string WorkerType { get; set; }
        public string Location { get; set; }
        public string requestState { get; set; }
        public string CreatedOn { get; set; }
    }
}
