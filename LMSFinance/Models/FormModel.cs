using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMSFinance.Models
{
    
    public class FormModel
    {
        public List<Student> Studentss { get; set; }
        public List<Subject> Subjectss { get; set; }
        public Receipt Receipt { get; set; }

    }
}