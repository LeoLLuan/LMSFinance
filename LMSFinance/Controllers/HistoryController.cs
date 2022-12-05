using LMSFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LMSFinance.Controllers
{
    public class HistoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: History
        public ActionResult Index()
        {
            var receipt = new List<Receipt>();
            foreach (var rec in db.Receipts)
            {
                receipt.Add(rec);
            }
            History history = new History();
            history.ReceiptList = receipt;

            return View(history);
        }

        public async Task<ActionResult> ReceiptDetails(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = await db.Receipts.FindAsync(id);

            if (receipt == null)
            {
                return HttpNotFound();
            }

            return View(receipt);

        }
    }
}