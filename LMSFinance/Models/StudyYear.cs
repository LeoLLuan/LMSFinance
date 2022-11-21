using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMSFinance.Models
{ 
    [Table("StudyYear")]
    public class StudyYear
    {
        [Key, Required]
        public string SchoolYear { get; set; }
    }
}