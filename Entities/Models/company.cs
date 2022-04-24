using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class company
    {
        [Column("CompanyId")]
        public Guid Id { get; set; }
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string CompanyName { get; set; }
        [MaxLength(30, ErrorMessage = "Maximum length for the ComapnyMail is 60 characters")]
        public string CompanyMail { get; set; }
        [MaxLength(10, ErrorMessage = "Maximum length for the CompanyMobile is 60 characters")]
        public string CompanyMobile { get; set; }
        [MaxLength(21, ErrorMessage = "Maximum length for the CompanyCin is 60 characters")]
        public string CompanyCin { get; set; }
        [MaxLength(15, ErrorMessage = "Maximum length for the CompanyGstin is 60 characters")]
        public string CompanyGstin { get; set; }
        [MaxLength(30, ErrorMessage = "Maximum length for the Password is 60 characters")]
        public string CompanyPass { get; set; }
        [MaxLength(25, ErrorMessage = "Maximum length for the CompanyType is 60 characters")]
        public string CompanyType { get; set; }
        [MaxLength(30, ErrorMessage = "Maximum length for the State is 60 characters")]
        public string CompanyState { get; set; }

        [MaxLength(30, ErrorMessage = "Maximum length for the District is 60 characters")]
        public string CompanyDistrict { get; set; }
        [MaxLength(30, ErrorMessage = "Maximum length for the Area is 60 characters")]
        public string CompanyArea { get; set; }
        [MaxLength(30, ErrorMessage = "Maximum length for the SubArea is 60 characters")]
        public string CompanySubArea { get; set; }
        [MaxLength(30, ErrorMessage = "Maximum length for the Number of worker is 60 characters")]
        public string WorkerNumber { get; set; }
        [MaxLength(120, ErrorMessage = "Maximum length for the Site Locations is 60 characters")]
        public string SiteLocation { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string KYCStatus { get; set; }

         public ICollection<Worker> Worker { get; set; }
    }
}
