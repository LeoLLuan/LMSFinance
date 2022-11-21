using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMSFinance.Models
{
    [Table("SubmitForm")]
    public class SubmitForm
    {
        [Key, Required]
        public int NO { get; set; }

        [Required]
        public string StudentId { get; set; }

        [Required]
        public string StudentName { get; set; }

        [Required]
        public string SubjectName { get; set; }


    }
}