using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PreviousWorker
    {
        [Column("Id")]
        public Guid Id { get; set; }
        [ForeignKey(nameof(company))]
        public Guid CompanyId { get; set; }
        [ForeignKey(nameof(Worker))]
        public Guid WorkerId { get; set; }
    }
}
