using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Entities.Models
{
    public class AdminCompany
    {
        //Company Sends requests to admin for worker
        [Column("Id")]
        public Guid Id { get; set; }
        [ForeignKey(nameof(company))]
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string WorkerType { get; set; }
        public string Vacancy { get; set; }
        public string RequestState { get; set; }
        public string CreatedOn { get; set; }

        public company Company { get; set; }

    }
}
