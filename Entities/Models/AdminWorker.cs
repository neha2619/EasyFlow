using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class AdminWorker
    {
        [Column("Id")]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Worker))]
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }
        public string WorkerType { get; set; }
        public string Location { get; set; }
        
    }
}
