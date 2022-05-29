using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TotalCounts
    {
        [Column("Id")]

        public Guid id { get; set; }

       
        public string count { get; set; }
        public string title { get; set; }
    }
}
