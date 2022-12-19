using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMSFinance.Models
{
    [Table("Absence")]
    public class Absence
    {
        [Key]
        public int NO { get; set; }

        public string TeacherId { get; set; }

        public string TName { get; set; }

        public string Faculty { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int TotalDays { get; set; }

        public string Reason { get; set; }

        public string Status { get; set; }
    }
}