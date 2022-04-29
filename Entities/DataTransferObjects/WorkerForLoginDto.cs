using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class WorkerForLoginDto
    {
        public string WorkerEmail { get; set; }
        public string WorkerMobile { get; set; }
        public string WorkerPass { get; set; }
    }
}
