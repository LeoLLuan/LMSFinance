using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMSFinance.Models
{
    [Table("Subject")]
    public class Subject
    {
        [Key]
        [Display(Name = "Subject ID")]
        public string SubjectID { get; set; }

        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        [Display(Name = "Faculty Name")]
        public string FacultyName { get; set; }

        [Display(Name = "Duration")]
        public int Duration { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Per Unit")]
        public int PerUnit { get; set; }

        [Display(Name = "Total Fee")]
        public int TotalFee { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "School Year")]
        public string SchoolYear { get; set; }
    }
}