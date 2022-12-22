using LMSFinance.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace LMSFinance.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public static double d50;
        public static double d70;
        public static double d100;

        public ActionResult Index(Home model)
        {
            foreach (var stu in db.Students)
            {
                model.IdStuNum ++;

                if (stu.Object != "None")
                {
                    if(stu.Object == "50%")
                    {
                        model.D50 ++;
                    }

                    if (stu.Object == "70%")
                    {
                        model.D70 ++;

                    }

                    if (stu.Object == "100%")
                    {
                        model.D100 ++;
                    }

                    model.DisStuNum ++;
                }
            }

            d50 = model.Dis50 = Math.Round((double)(model.D50 * 100) / model.DisStuNum);
            d70 = model.Dis70 = Math.Round((double)(model.D70 * 100) / model.DisStuNum);
            d100 = model.Dis100 = Math.Round((double)(model.D100 * 100) / model.DisStuNum);

            return View(model);
        }

        public ActionResult PieTypeChart(Home model)
        {
            List<ChartFields> chartdata = new List<ChartFields>();
            chartdata = ChartDataList(model);
            var chart = new Chart(width: 500, height: 400)
                .AddSeries(chartType: "pie",
                    xValue: chartdata, xField: "Name",
                    yValues: chartdata, yFields: "Percentage")
                    .AddTitle("Discount Percentage")
                    .GetBytes("png");
            return File(chart, "image/bytes");
        }

        public List<ChartFields> ChartDataList(Home model)
        {
            List<ChartFields> _chart = new List<ChartFields>();
            _chart.Add(new ChartFields { Name = "Discount 50%", Percentage = d50});
            _chart.Add(new ChartFields { Name = "Disount 70%", Percentage = d70});
            _chart.Add(new ChartFields { Name = "Disount 100%", Percentage = d100});
            return _chart;
        }
    }
}