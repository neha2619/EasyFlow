using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class WorkerDto
    {

        public Guid Id { get; set; }
        public string WorkerName { get; set; }
        public string WorkerMobile { get; set; }
        public string WorkerMail { get; set; }
        public string WorkerType { get; set; }
        public string LocationPreference { get; set; }
        public string ratings { get; set; }
    }
}
