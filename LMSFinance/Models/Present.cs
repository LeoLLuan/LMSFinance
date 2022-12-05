using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMSFinance.Models
{
    [Table("Present")]
    public class Present
    {
        [Key]
        public string Presents { get; set; }
    }
}