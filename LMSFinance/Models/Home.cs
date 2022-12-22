using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMSFinance.Models
{
    public class Home
    {         
        public int IdStuNum { get; set; }

        public int DisStuNum { get; set; }

        public int ClassNum { get; set; }

        public int D50 { get; set; }

        public int D70 { get; set; }

        public int D100 { get; set; }

        public double Dis50 { get; set; }

        public double Dis70 { get; set; }

        public double Dis100 { get; set; }

        public List<ChartFields> ChartData { get; set; }
    }
    public class ChartFields
    {
        public string Name { get; set; }
        public double Percentage { get; set; }
    }

}