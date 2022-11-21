using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMSFinance.Models
{
    [Table("Grade")]
    public class Grade
    {
        [Key]
        public string GradeName { get; set; }
    }
}