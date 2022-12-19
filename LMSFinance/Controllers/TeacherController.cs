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
using System.Data.SqlClient;
using LMSFinance.Migrations;
using PagedList;

namespace LMSFinance.Controllers
{
    public class TeacherController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Student Sort, Search, Pagination
        public async Task<ActionResult> Index(string currentFilter, int? page, string sortOrder, string searchString)
        {
            var teachers = from t in db.Teachers select t;

            //Pagination
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            //Sort
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "Id_desc" : "";
            ViewBag.IdDesSortParm = sortOrder == "IdDes" ? "IdDes" : "IdDes";
            ViewBag.NameAsSortParm = sortOrder == "NameAs" ? "NameAs" : "NameAs";
            ViewBag.NameDesSortParm = sortOrder == "NameDes" ? "NameDes" : "NameDes";
            ViewBag.CodeAsSortParm = sortOrder == "CodeAs" ? "CodeAs" : "CodeAs";
            ViewBag.CodeDesSortParm = sortOrder == "CodeDes" ? "CodeDes" : "CodeDes";
            ViewBag.RoleAsSortParm = sortOrder == "RoleAs" ? "RoleAs" : "RoleAs";
            ViewBag.RoleDesSortParm = sortOrder == "RoleDes" ? "RoleDes" : "RoleDes";
            ViewBag.ContractAsSortParm = sortOrder == "ContractAs" ? "ContractAs" : "ContractAs";
            ViewBag.ContractDesSortParm = sortOrder == "ContractDes" ? "ContractDes" : "ContractDes";
            ViewBag.StDateAsSortParm = sortOrder == "StDateAs" ? "StDateAs" : "StDateAs";
            ViewBag.StDateDesSortParm = sortOrder == "StDateDes" ? "StDateDes" : "StDateDes";
            ViewBag.AbsenceAsSortParm = sortOrder == "AbsenceAs" ? "AbsenceAs" : "AbsenceAs";
            ViewBag.AbsenceDesSortParm = sortOrder == "AbsenceDes" ? "AbsenceDes" : "AbsenceDes";
            ViewBag.StatusAsSortParm = sortOrder == "StatusAs" ? "StatusAs" : "StatusAs";
            ViewBag.StatusDesSortParm = sortOrder == "StatusDes" ? "StatusDes" : "StatusDes";


            switch (sortOrder)
            {
                case "IdDes":
                    teachers = teachers.OrderByDescending(s => s.TeacherId);
                    break;
                case "NameDes":
                    teachers = teachers.OrderByDescending(s => s.TeacherName);
                    break;
                case "NameAs":
                    teachers = teachers.OrderBy(s => s.TeacherName);
                    break;
                case "CodeDes":
                    teachers = teachers.OrderByDescending(s => s.Code);
                    break;
                case "CodeAs":
                    teachers = teachers.OrderBy(s => s.Code);
                    break;
                case "RoleDes":
                    teachers = teachers.OrderByDescending(s => s.Role);
                    break;
                case "RoleAs":
                    teachers = teachers.OrderBy(s => s.Role);
                    break;
                case "ContractDes":
                    teachers = teachers.OrderByDescending(s => s.ContractType);
                    break;
                case "ContractAs":
                    teachers = teachers.OrderBy(s => s.ContractType);
                    break;
                case "StDateDes":
                    teachers = teachers.OrderByDescending(s => s.StartingDate);
                    break;
                case "StDateAs":
                    teachers = teachers.OrderBy(s => s.StartingDate);
                    break;
                case "AbsenceDes":
                    teachers = teachers.OrderByDescending(s => s.LeaveDay);
                    break;
                case "AbsenceAs":
                    teachers = teachers.OrderBy(s => s.LeaveDay);
                    break;
                case "StatusDes":
                    teachers = teachers.OrderByDescending(s => s.Status);
                    break;
                case "StatusAs":
                    teachers = teachers.OrderBy(s => s.Status);
                    break;
                default:
                    teachers = teachers.OrderBy(s => s.TeacherId);
                    break;
            }

            return View(teachers.ToPagedList(pageNumber, pageSize));
        }

        // GET: Teacher/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teacher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teacher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TeacherId,TeacherName,Code,Role,ContractType,StartingDate,LeaveDay,Status,Gender,Birthday,IdentityNo,IssuedBy,Address")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                List<Absence> Absence = new List<Absence>();
                foreach(var ab in db.Absences)
                {
                    Absence.Add(ab);
                }
                foreach (var ab in Absence)
                {
                    if(ab.TeacherId == teacher.TeacherId)
                    {
                        teacher.LeaveDay += ab.TotalDays;
                    }
                }

                db.Teachers.Add(teacher);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Teacher/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TeacherId,TeacherName,Code,Role,ContractType,StartingDate,LeaveDay,Status,Gender,Birthday,IdentityNo,IssuedBy,Address")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;

                List<Absence> Absence = new List<Absence>();
                foreach (var ab in db.Absences)
                {
                    Absence.Add(ab);
                }
                foreach (var ab in Absence)
                {
                    if (ab.TeacherId == teacher.TeacherId)
                    {
                        teacher.LeaveDay += ab.TotalDays;
                    }
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: Teacher/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Teacher teacher = await db.Teachers.FindAsync(id);
            db.Teachers.Remove(teacher);
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
