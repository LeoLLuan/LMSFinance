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
    public class AbsenceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Absence
        public async Task<ActionResult> Index()
        {
            return View(await db.Absences.ToListAsync());
        }

        // GET: Absence/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Absence absence = await db.Absences.FindAsync(id);
            if (absence == null)
            {
                return HttpNotFound();
            }
            return View(absence);
        }

        // GET: Absence/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Absence/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NO,TeacherId,TName,Faculty,FromDate,TotalDays,ToDate,Reason,Status")] Absence absence)
        {
            if (ModelState.IsValid)
            {
                var time = (absence.ToDate - absence.FromDate);
                absence.TotalDays = time.Days;
                db.Absences.Add(absence);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(absence);
        }

        // GET: Absence/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Absence absence = await db.Absences.FindAsync(id);
            if (absence == null)
            {
                return HttpNotFound();
            }
            return View(absence);
        }

        // POST: Absence/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "NO,TeacherId,TName,Faculty,FromDate,ToDate,TotalDays,Reason,Status")] Absence absence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(absence).State = EntityState.Modified;
                var time = (absence.ToDate - absence.FromDate);
                absence.TotalDays = time.Days;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(absence);
        }

        // GET: Absence/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Absence absence = await db.Absences.FindAsync(id);
            if (absence == null)
            {
                return HttpNotFound();
            }
            return View(absence);
        }

        // POST: Absence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Absence absence = await db.Absences.FindAsync(id);
            db.Absences.Remove(absence);
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
