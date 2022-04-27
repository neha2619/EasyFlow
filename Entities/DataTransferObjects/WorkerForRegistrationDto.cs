using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class WorkerForRegistrationDto
    {
        public Guid Id { get; set; }
        public string WorkerName { get; set; }
        public string WorkerMobile { get; set; }
        public string WorkerMail { get; set; }
        public string WorkerType { get; set; }
        public string WorkerPass { get; set; }
    }
}
