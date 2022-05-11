using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CheckRequestsDto
    {
        public Guid userID { get; set; }
        public string WorkerType { get; set; }
        public string Location { get; set; }
        public string TimeStamp { get; set; }
    }
}
