using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class WorkerUpdateDto
    {
        public string WorkerName { get; set; }
        //public int WorkerMail { get; set; }
        public string WorkerMobile { get; set; }
        //public string KYCStatus { get; set; }
        //public string WorkerPass { get; set; }
        public string WorkerType { get; set; }
        public string LocationPrefrence { get; set; }
    }
}
