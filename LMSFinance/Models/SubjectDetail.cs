using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMSFinance.Models
{
    [Table("SubjectDetail")]
    public class SubjectDetail
    {
        [Key]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }
    }
}