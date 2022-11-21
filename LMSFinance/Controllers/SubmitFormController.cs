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
    public class SubmitFormController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubmitForm
        public async Task<ActionResult> Index()
        {
            return View(await db.SubmitForms.ToListAsync());
        }


        // GET: SubmitForm/Create
        public ActionResult Create()
        {
            List<SelectListItem> SubjectNames = new List<SelectListItem>();
            foreach (var sch in db.Subjects)
            {
                SubjectNames.Add(new SelectListItem() { Value = sch.SubjectName, Text = sch.SubjectName });
            }
            ViewBag.SubjectsNames = SubjectNames;

            List<SelectListItem> StudentIds = new List<SelectListItem>();
            foreach (var stu in db.Students)
            {
                StudentIds.Add(new SelectListItem() { Value = stu.StudentId, Text = stu.StudentId });
            }
            ViewBag.StudentIds = StudentIds;

            return View();
        }

        // POST: SubmitForm/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NO,StudentId,StudentName,SubjectName")] SubmitForm submitForm)
        {
            List<SelectListItem> SubjectNames = new List<SelectListItem>();
            foreach (var sch in db.Subjects)
            {
                SubjectNames.Add(new SelectListItem() { Value = sch.SubjectName, Text = sch.SubjectName });
            }
            ViewBag.SubjectsNames = SubjectNames;

            List<SelectListItem> StudentIds = new List<SelectListItem>();
            foreach (var stu in db.Students)
            {
                StudentIds.Add(new SelectListItem() { Value = stu.StudentId, Text = stu.StudentId });
            }
            ViewBag.StudentIds = StudentIds;

            foreach (var st in db.Students)
            {
                if (st.StudentId == submitForm.StudentId)
                {
                    submitForm.StudentName = st.StudentName;
                }
            }

            db.SubmitForms.Add(submitForm);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: SubmitForm/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            List<SelectListItem> SubjectNames = new List<SelectListItem>();
            foreach (var sch in db.Subjects)
            {
                SubjectNames.Add(new SelectListItem() { Value = sch.SubjectName, Text = sch.SubjectName });
            }
            ViewBag.SubjectsNames = SubjectNames;

            List<SelectListItem> StudentIds = new List<SelectListItem>();
            foreach (var stu in db.Students)
            {
                StudentIds.Add(new SelectListItem() { Value = stu.StudentId, Text = stu.StudentId });
            }
            ViewBag.StudentIds = StudentIds;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitForm submitForm = await db.SubmitForms.FindAsync(id);
            if (submitForm == null)
            {
                return HttpNotFound();
            }
            return View(submitForm);
        }

        // POST: SubmitForm/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "NO,StudentId,StudentName,SubjectName")] SubmitForm submitForm)
        {

            List<SelectListItem> SubjectNames = new List<SelectListItem>();
            foreach (var sch in db.Subjects)
            {
                SubjectNames.Add(new SelectListItem() { Value = sch.SubjectName, Text = sch.SubjectName });
            }
            ViewBag.SubjectsNames = SubjectNames;

            List<SelectListItem> StudentIds = new List<SelectListItem>();
            foreach (var stu in db.Students)
            {
                StudentIds.Add(new SelectListItem() { Value = stu.StudentId, Text = stu.StudentId });
            }
            ViewBag.StudentIds = StudentIds;

            db.Entry(submitForm).State = EntityState.Modified;

            foreach (var st in db.Students)
            {
                if (st.StudentId == submitForm.StudentId)
                {
                    submitForm.StudentName = st.StudentName;
                }
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: SubmitForm/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmitForm submitForm = await db.SubmitForms.FindAsync(id);
            if (submitForm == null)
            {
                return HttpNotFound();
            }
            return View(submitForm);
        }

        // POST: SubmitForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SubmitForm submitForm = await db.SubmitForms.FindAsync(id);
            db.SubmitForms.Remove(submitForm);
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
