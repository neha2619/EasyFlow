using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Worker
    {
        [Column("WorkerId")]
        public Guid Id { get; set; }
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string WorkerName { get; set; }
        [MaxLength(30, ErrorMessage = "Maximum length for the Worker's Mail is 30 characters")]
        public string WorkerMail { get; set; }
        [MaxLength(10, ErrorMessage = "Maximum length for the Worker's Mobile is 10 characters")]
        public string WorkerMobile { get; set; }
        [MaxLength(12, ErrorMessage = "Maximum length for the Aadhar is 12 characters")]
        public string WorkerAadhar { get; set; }
      
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string KYCStatus { get; set; }

        [MaxLength(30, ErrorMessage = "Maximum length for the Password is 60 characters")]
        public string WorkerPass { get; set; }
        [MaxLength(80, ErrorMessage = "Maximum length for the Worker Type is 80 characters")]
        public string WorkerType { get; set; }
        [MaxLength(180, ErrorMessage = "Maximum length for the Location Preference is 180 characters")]
        public string LocationPreference { get; set; }
        public int Ratings { get; set; }

    }
}
