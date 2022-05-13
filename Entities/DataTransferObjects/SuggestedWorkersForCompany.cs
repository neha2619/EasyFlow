using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class SuggestedWorkersForCompany
    {
        public Guid CompanyId { get; set; }
        public Guid WorkerId { get; set; }
        public string RequestStatus { get; set; }
        public string CreatedOn { get; set; }
    }
}
