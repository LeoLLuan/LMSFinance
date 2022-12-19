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
    public class TeacherFacultyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TeacherFaculty
        public async Task<ActionResult> Index()
        {
            return View(await db.TeacherFaculties.ToListAsync());
        }

        // GET: TeacherFaculty/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherFaculty teacherFaculty = await db.TeacherFaculties.FindAsync(id);
            if (teacherFaculty == null)
            {
                return HttpNotFound();
            }
            return View(teacherFaculty);
        }

        // GET: TeacherFaculty/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeacherFaculty/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TeaFaculty")] TeacherFaculty teacherFaculty)
        {
            if (ModelState.IsValid)
            {
                db.TeacherFaculties.Add(teacherFaculty);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(teacherFaculty);
        }


        // GET: TeacherFaculty/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherFaculty teacherFaculty = await db.TeacherFaculties.FindAsync(id);
            if (teacherFaculty == null)
            {
                return HttpNotFound();
            }
            return View(teacherFaculty);
        }

        // POST: TeacherFaculty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            TeacherFaculty teacherFaculty = await db.TeacherFaculties.FindAsync(id);
            db.TeacherFaculties.Remove(teacherFaculty);
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
