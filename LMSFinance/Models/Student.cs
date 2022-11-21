using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMSFinance.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public string StudentId { get; set; }

        [StringLength(50)]
        public string StudentName { get; set; }

        [Display(Name = "Class")]
        public string ClassName {get; set; }

        [Display(Name = "Grade")]
        public string GradeName { get; set; }

        public DateTime DoB { get; set; }

        public string PhoneNumber { get; set; }

        public int SchoolFee { get; set; }

        public string Object {get; set; }

        public DateTime AdmissionDate { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        public int RealMoney { get; set; }

        public DateTime PaymentDate { get; set; }

    }
}

