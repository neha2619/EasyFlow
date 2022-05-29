using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CompanyReq
    {
        [Column("Id")]
        public Guid Id { get; set; }
        public string CompanyId { get; set; }
       public string WorkerId { get; set; }
        public string WorkerName { get; set; }
        public string email { get; set; }//this is workeremail
        public string Mobile { get; set; }//this is workermobile
        public string WorkerType { get; set; }
        public string Location { get; set; }
        public string RequestStatus { get; set; }
        public string CreatedOn { get; set; }

    }
}
