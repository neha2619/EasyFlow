using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class DashBoardDto
    {
        
        public IEnumerable<TotalCounts> totalcounts { get; set; }
        public IEnumerable<int> workerbyMonth { get; set; }
        public IEnumerable<int> CompanybyMonth { get; set; }
        public IEnumerable< Worker> worker { get; set; }
    }
}
