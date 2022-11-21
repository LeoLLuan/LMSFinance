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
    public class SubjectDetailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubjectDetail
        public async Task<ActionResult> Index()
        {
            return View(await db.SubjectDetails.ToListAsync());
        }

        // GET: SubjectDetail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubjectDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SubjectID,SubjectName")] SubjectDetail subjectDetail)
        {
            if (ModelState.IsValid)
            {
                db.SubjectDetails.Add(subjectDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(subjectDetail);
        }

        // GET: SubjectDetail/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectDetail subjectDetail = await db.SubjectDetails.FindAsync(id);
            if (subjectDetail == null)
            {
                return HttpNotFound();
            }
            return View(subjectDetail);
        }

        // POST: SubjectDetail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SubjectID,SubjectName")] SubjectDetail subjectDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjectDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(subjectDetail);
        }

        // GET: SubjectDetail/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectDetail subjectDetail = await db.SubjectDetails.FindAsync(id);
            if (subjectDetail == null)
            {
                return HttpNotFound();
            }
            return View(subjectDetail);
        }

        // POST: SubjectDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            SubjectDetail subjectDetail = await db.SubjectDetails.FindAsync(id);
            db.SubjectDetails.Remove(subjectDetail);
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
