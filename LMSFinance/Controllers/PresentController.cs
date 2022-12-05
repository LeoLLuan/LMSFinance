using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMSFinance.Models;

namespace LMSFinance.Controllers
{
    public class PresentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Present
        public async Task<ActionResult> Index()
        {
            return View(await db.Presents.ToListAsync());
        }

        // GET: Present/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Present/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Presents")] Present present)
        {
            if (ModelState.IsValid)
            {
                db.Presents.Add(present);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
             return View(present);
        }

        // GET: Present/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Present present = await db.Presents.FindAsync(id);
            if (present == null)
            {
                return HttpNotFound();
            }
            return View(present);
        }

        // POST: Present/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Presents")] Present present)
        {
            if (ModelState.IsValid)
            {
                db.Entry(present).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(present);
        }

        // GET: Present/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Present present = await db.Presents.FindAsync(id);
            if (present == null)
            {
                return HttpNotFound();
            }
            return View(present);
        }

        // POST: Present/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Present present = await db.Presents.FindAsync(id);
            db.Presents.Remove(present);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
