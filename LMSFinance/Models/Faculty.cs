using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMSFinance.Models
{
    [Table("Faculty")]
    public class Faculty
    {
        [Key]
        public string FacultyName { get; set; }
    }
}