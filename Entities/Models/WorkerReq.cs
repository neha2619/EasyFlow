using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class WorkerReq
    {
        //table when admin requests any work to workers
        [Column("Id")]
        public Guid Id { get; set; }
        public string WorkerId { get; set; }
        //add forignkey of companyid
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyMail { get; set; }
        public string CompanyMobile { get; set; }
        public string WorkerType { get; set; }  
        public string Location { get; set; }
        public string RequestStatus { get; set; }
        public string CreatedOn { get; set; }

    }
}
