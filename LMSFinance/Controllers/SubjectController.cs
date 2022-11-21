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
using PagedList;
using ClosedXML.Excel;
using System.IO;
using System.Text;
using DocumentFormat.OpenXml.EMMA;
using LMSFinance.Migrations;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections;
using Microsoft.AspNet.Identity;
using System.Security.Cryptography.X509Certificates;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Security.Cryptography;

namespace LMSFinance.Controllers
{
    public class SubjectController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Subject
        public ActionResult Index(string SubjectName, string subjectName, string currentFilter, int? page, string sortOrder, string searchString, string searchFaculty,
                    string searchSubject, string searchStatus, string searchYear)
        {
            //Declare Database as students
            var subjects = from s in db.Subjects select s;

            //Declare Subject Name
            List<SelectListItem> detailsName = new List<SelectListItem>();
            var detail = from d in db.SubjectDetails select d;
            foreach (var det in detail)
            {
                detailsName.Add(new SelectListItem() { Value = det.SubjectName, Text = det.SubjectName });
            }

            ViewBag.DetailName = detailsName;

            //Declare Faculty
            List<SelectListItem> faculty = new List<SelectListItem>();
            var faculties = from f in db.Faculties select f;

            foreach (var fac in faculties)
            {
                faculty.Add(new SelectListItem() { Value = fac.FacultyName, Text = fac.FacultyName });
            }

            ViewBag.Faculties = faculty;

            //Declare School Year
            List<SelectListItem> schoolYear = new List<SelectListItem>();
            var schools = from s in db.StudyYears select s;

            foreach (var sch in schools)
            {
                schoolYear.Add(new SelectListItem() { Value = sch.SchoolYear, Text = sch.SchoolYear });
            }

            ViewBag.SchoolYears = schoolYear;

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

            //Search Subject ID
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                subjects = subjects.Where(s => s.SubjectID.Contains(searchString));
            }

            //Search Faculty
            ViewBag.SearchFaculty = searchFaculty;
            if (!String.IsNullOrEmpty(searchFaculty))
            {
                subjects = subjects.Where(s => s.FacultyName.Contains(searchFaculty));
            }

            //Search Subject
            ViewBag.SearchSubject = searchSubject;
            if (!String.IsNullOrEmpty(searchSubject))
            {
                subjects = subjects.Where(s => s.SubjectName.Contains(searchSubject));
            }

            //Search Subject Status
            ViewBag.SearchStatus = searchStatus;
            if (!String.IsNullOrEmpty(searchStatus))
            {
                subjects = subjects.Where(s => s.Status.Contains(searchStatus));
            }

            //Search School Year
            ViewBag.SearchYear = searchYear;
            if (!String.IsNullOrEmpty(searchYear))
            {
                subjects = subjects.Where(s => s.SchoolYear.Contains(searchYear));
            }

            //Sort
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "Id_desc" : "";
            ViewBag.SubIdDesSortParm = sortOrder == "SubIdAs" ? "SubIdAs" : "SubIdAs";
            ViewBag.SubIdDesSortParm = sortOrder == "SubIdDes" ? "SubIdDes" : "SubIdDes";
            ViewBag.SubNameAsSortParm = sortOrder == "SubNameAs" ? "SubNameAs" : "SubNameAs";
            ViewBag.SubNameDesSortParm = sortOrder == "SubNameDes" ? "SubNameDes" : "SubNameDes";
            ViewBag.FacultyAsSortParm = sortOrder == "FacultyAs" ? "FacultyAs" : "FacultyAs";
            ViewBag.FacultyDesSortParm = sortOrder == "FacultyDes" ? "FacultyDes" : "FacultyDes";
            ViewBag.DurAsSortParm = sortOrder == "DurAs" ? "DurAs" : "DurAs";
            ViewBag.DurDesSortParm = sortOrder == "DurDes" ? "DurDes" : "DurDes";
            ViewBag.UnitAsSortParm = sortOrder == "UnitAs" ? "UnitAs" : "UnitAs";
            ViewBag.UnitDesSortParm = sortOrder == "UnitDes" ? "UnitDes" : "UnitDes";
            ViewBag.PerUnitAsSortParm = sortOrder == "PerUnitAs" ? "PerUnitAs" : "PerUnitAs";
            ViewBag.PerUnitDesSortParm = sortOrder == "PerUnitDes" ? "PerUnitDes" : "PerUnitDes";
            ViewBag.TotalAsSortParm = sortOrder == "TotalAs" ? "TotalAs" : "TotalAs";
            ViewBag.TotalDesSortParm = sortOrder == "TotalDes" ? "TotalDes" : "TotalDes";
            ViewBag.StatusAsSortParm = sortOrder == "StatusAs" ? "StatusAs" : "StatusAs";
            ViewBag.StatusDesSortParm = sortOrder == "StatusDes" ? "StatusDes" : "StatusDes";

            switch (sortOrder)
            {
                case "SubIdAs":
                    subjects = subjects.OrderBy(s => s.SubjectID);
                    break;
                case "SubIdDes":
                    subjects = subjects.OrderByDescending(s => s.SubjectID);
                    break;
                case "SubNameDes":
                    subjects = subjects.OrderByDescending(s => s.SubjectName);
                    break;
                case "SubNameAs":
                    subjects = subjects.OrderBy(s => s.SubjectName);
                    break;
                case "FacultyDes":
                    subjects = subjects.OrderByDescending(s => s.FacultyName);
                    break;
                case "FacultyAs":
                    subjects = subjects.OrderBy(s => s.FacultyName);
                    break;
                case "DurDes":
                    subjects = subjects.OrderByDescending(s => s.Duration);
                    break;
                case "DurAs":
                    subjects = subjects.OrderBy(s => s.Duration);
                    break;
                case "UnitDes":
                    subjects = subjects.OrderByDescending(s => s.Unit);
                    break;
                case "UnitAs":
                    subjects = subjects.OrderBy(s => s.Unit);
                    break;
                case "PerUnitDes":
                    subjects = subjects.OrderByDescending(s => s.PerUnit);
                    break;
                case "PerUnitAs":
                    subjects = subjects.OrderBy(s => s.PerUnit);
                    break;
                case "TotalDes":
                    subjects = subjects.OrderByDescending(s => s.TotalFee);
                    break;
                case "TotalAs":
                    subjects = subjects.OrderBy(s => s.TotalFee);
                    break;
                case "StatusDes":
                    subjects = subjects.OrderByDescending(s => s.Status);
                    break;
                case "StatusAs":
                    subjects = subjects.OrderBy(s => s.Status);
                    break;
                default:
                    subjects = subjects.OrderByDescending(s => s.SchoolYear);
                    break;
            }

            return View(subjects.ToPagedList(pageNumber, pageSize));
        }

        // GET: Subject/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: Subject/Create
        public ActionResult Create()
        {
            //Create Subject Name
            List<SelectListItem> detailsName = new List<SelectListItem>();
            var detailName = from f in db.SubjectDetails select f;

            foreach (var det in detailName)
            {
                detailsName.Add(new SelectListItem() { Value = det.SubjectName, Text = det.SubjectName });
            }

            ViewBag.DetailName = detailsName;

            //Create Faculty 
            List<SelectListItem> faculty = new List<SelectListItem>();
            var faculties = from f in db.Faculties select f;

            foreach (var fac in faculties)
            {
                faculty.Add(new SelectListItem() { Value = fac.FacultyName, Text = fac.FacultyName });
            }

            ViewBag.Faculties = faculty;

            //Create School Year
            List<SelectListItem> school = new List<SelectListItem>();
            List<SelectListItem> schoolYear = new List<SelectListItem>();
            var schools = from s in db.StudyYears select s;
            int count = 0;

            foreach(var sch in schools)
            {
                school.Add(new SelectListItem() { Value = sch.SchoolYear, Text = sch.SchoolYear });
            }

            for(int i = 0; i < school.Count; i++)
            {
                count++;
                if(count == school.Count)
                {
                    schoolYear.Add(school[i]);
                }
            }

            ViewBag.SchoolYears = schoolYear;

            return View();
        }

        // POST: Subject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SubjectID,SubjectName,FacultyName,Duration,Unit,PerUnit,TotalFee,Status,SchoolYear")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                subject.TotalFee = subject.Duration * subject.PerUnit;
                db.Subjects.Add(subject);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(subject);
        }

        // GET: Subject/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            //Edit Subject Name
            List<SelectListItem> detailName = new List<SelectListItem>();
            var detailsName = from f in db.SubjectDetails select f;

            foreach (var det in detailsName)
            {
                detailName.Add(new SelectListItem() { Value = det.SubjectName, Text = det.SubjectName });
            }

            ViewBag.DetailName = detailName;

            //Edit Faculty
            List<SelectListItem> faculty = new List<SelectListItem>();
            var faculties = from f in db.Faculties select f;

            foreach (var fac in faculties)
            {
                faculty.Add(new SelectListItem() { Value = fac.FacultyName, Text = fac.FacultyName });
            }

            ViewBag.Faculty = faculty;

            //Edit School Year
            List<SelectListItem> school = new List<SelectListItem>();
            List<SelectListItem> schoolYear = new List<SelectListItem>();
            var schools = from s in db.StudyYears select s;
            int count = 0;

            foreach (var sch in schools)
            {
                school.Add(new SelectListItem() { Value = sch.SchoolYear, Text = sch.SchoolYear });
            }

            for (int i = 0; i < school.Count; i++)
            {
                count++;
                if (count == school.Count)
                {
                    schoolYear.Add(school[i]);
                }
            }
            ViewBag.School = schoolYear;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SubjectID,SubjectName,FacultyName,Duration,Unit,PerUnit,TotalFee,Status,SchoolYear")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                subject.TotalFee = subject.Duration * subject.PerUnit;
                db.Entry(subject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        // GET: Subject/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Subject subject = await db.Subjects.FindAsync(id);
            db.Subjects.Remove(subject);
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

        //GET: Convert to CSV File
        [HttpGet]
        public FileResult ExportCSV()
        {
            string[] subj = new string[] { "SubjectID", "SubjectName", "FacultyName", "Duration", "Unit", "PerUnit", "TotalFee", "Status", "SchoolYear" };
            var subjects = db.Subjects;
            string csv = string.Empty;

            foreach (var sub in subjects)
            {
                csv += sub.SubjectID.ToString().Replace(",", ";") + ',';
                csv += sub.SubjectName.ToString().Replace(",", ";") + ',';
                csv += sub.FacultyName.ToString().Replace(",", ";") + ',';
                csv += sub.Duration.ToString().Replace(",", ";") + ',';
                csv += sub.Unit.ToString().Replace(",", ";") + ',';
                csv += sub.PerUnit.ToString().Replace(",", ";") + ',';
                csv += sub.TotalFee.ToString().Replace(",", ";") + ',';
                csv += sub.Status.ToString().Replace(",", ";") + ',';
                csv += sub.SchoolYear.ToString().Replace(",", ";") + ',';
                csv += "\r\n";
            }

            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "text/csv", "Subject.csv");
        }

        //GET: Convert to Excel File
        public ActionResult ExportExcel()
        {
            var workbook = new XLWorkbook();
            var subjects = db.Subjects;
            var worksheet = workbook.Worksheets.Add("Subjects");
            var row = 1;

            worksheet.Cell(row, 1).Value = "Subject Id";
            worksheet.Cell(row, 2).Value = "Subject Name";
            worksheet.Cell(row, 3).Value = "Faculty";
            worksheet.Cell(row, 4).Value = "Duration";
            worksheet.Cell(row, 5).Value = "Unit";
            worksheet.Cell(row, 6).Value = "Fee Per Unit";
            worksheet.Cell(row, 7).Value = "Total";
            worksheet.Cell(row, 8).Value = "Status";
            worksheet.Cell(row, 9).Value = "School Year";

            foreach (var sub in subjects)
            {
                row++;
                worksheet.Cell(row, 1).Value = sub.SubjectID;
                worksheet.Cell(row, 2).Value = sub.SubjectName;
                worksheet.Cell(row, 3).Value = sub.FacultyName;
                worksheet.Cell(row, 4).Value = sub.Duration;
                worksheet.Cell(row, 5).Value = sub.Unit;
                worksheet.Cell(row, 6).Value = sub.PerUnit;
                worksheet.Cell(row, 7).Value = sub.TotalFee;
                worksheet.Cell(row, 8).Value = sub.Status;
                worksheet.Cell(row, 9).Value = sub.SchoolYear;
            }

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "subject.xlsx");
        }
    }
}
