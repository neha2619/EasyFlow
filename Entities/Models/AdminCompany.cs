using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Entities.Models
{
    public class AdminCompany
    {
        [Column("Id")]
        public Guid Id { get; set; }
        [ForeignKey(nameof(company))]
        public Guid CompanyId { get; set; }
        public string Location { get; set; }
        public string WorkerType { get; set; }
        public string Vacancy { get; set; }
        public company Company { get; set; }

    }
}
