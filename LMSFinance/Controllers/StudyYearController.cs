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
    public class StudyYearController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudyYear
        public async Task<ActionResult> Index()
        {
            return View(await db.StudyYears.ToListAsync());
        }

        // GET: StudyYear/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudyYear/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SchoolYear")] StudyYear studyYear)
        {
            if (ModelState.IsValid)
            {
                db.StudyYears.Add(studyYear);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(studyYear);
        }

        // GET: StudyYear/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyYear studyYear = await db.StudyYears.FindAsync(id);
            if (studyYear == null)
            {
                return HttpNotFound();
            }
            return View(studyYear);
        }

        // POST: StudyYear/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            StudyYear studyYear = await db.StudyYears.FindAsync(id);
            db.StudyYears.Remove(studyYear);
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
