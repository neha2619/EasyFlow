using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class LatestRequestsForDashboardDto
    {
        public int Serial { get; set; }
        public string CompanyName { get; set; }
        public string WorkerType { get; set; }
        public string Vacancy { get; set; }
        public string RequestState { get; set; }

    }
}
