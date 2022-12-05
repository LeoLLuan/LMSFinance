using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMSFinance.Models
{
    [Table("Teacher")]
    public class Teacher
    {
        [Key, Required]
        public string TeacherId { get; set; }

        public string TeacherName { get; set; }

        public string Code { get; set; }

        public string Role { get; set; }

        public string ContractType { get; set; }

        public DateTime StartingDate { get; set; }

        public string LeaveDay { get; set; }

        public string Status { get; set; }


        public string Gender { get; set; }

        public string Birthday { get; set; }

        public string IdentityNo { get; set; }

        public string IssuedBy { get; set; }

         public string Address { get; set; }









    }
}