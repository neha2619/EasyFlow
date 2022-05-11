using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class OTPs
    {
        [Column("Id")]
        public Guid id { get; set; }

        [MaxLength(30, ErrorMessage = "Maximum length for the RecipientMail is 60 characters")]
        public string recipientEmail { get; set; }
        public string timestamp { get; set; }

    }
}
