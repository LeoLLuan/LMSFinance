using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMSFinance.Models
{
    [Table("TeaFaculty")]
    public class TeacherFaculty
    {
        [Key]
        public string TeaFaculty { get; set; }
    }
}