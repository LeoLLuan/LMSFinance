using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMSFinance.Models
{
    [Table("Receipt")]
    public class Receipt
    {
        [Key]
        public int NO { get; set; }

        public string Collecter { get; set; }

        public string StudentID { get; set; }

        public string StudentName { get; set; }

        public int TotalDuration { get; set; }

        public int FeePerUnit { get; set; }

        public int TotalSchoolFee { get; set; }

        public string CollectMethod { get; set; }

        public string DiscountObject { get; set; }

        public Boolean Sale { get; set; }

        public string SaleType { get; set; }

        public Boolean TotalSubFeeCheck { get; set; }

        public int TotalSubFee { get; set; }

        public string SubFeeDescription { get; set; }

        public DateTime CollectedDate { get; set; }

        public int Money { get; set; }
    }
}