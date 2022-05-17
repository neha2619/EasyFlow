using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Timestamps
    {
        [Column("Id")]
        public Guid id { get; set; }
        public Guid RecipientID { get; set; }
        public string TimeStamp { get; set; }
    }
}
