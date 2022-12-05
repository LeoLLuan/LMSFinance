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
using LMSFinance.Migrations;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.EnterpriseServices.Internal;
using System.Web.UI.WebControls;
using System.Web.UI;
using Korzh.EasyQuery;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Ajax.Utilities;
using DocumentFormat.OpenXml.EMMA;
using PagedList;
using System.Windows;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace LMSFinance.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Student Sort, Search, Pagination
        public async Task<ActionResult> Index(string currentFilter, int? page, string sortOrder, string searchString, string searchGrade,
            string searchClass, string searchObject, string searchStatus)
        {
            //Declare Database as students
            var students = from s in db.Students select s;

            //Declare Grade
            List<SelectListItem> studentGrade = new List<SelectListItem>();
            foreach (var gra in db.Grades)
            {
                studentGrade.Add(new SelectListItem() { Value = gra.GradeName, Text = gra.GradeName });
            }
            ViewBag.StudentGrades = studentGrade;

            //Declare Object
            List<SelectListItem> Objects = new List<SelectListItem>();
            foreach (var oj in db.Discounts)
            {
                Objects.Add(new SelectListItem() { Value = oj.Discounts, Text = oj.Discounts });
            }
            ViewBag.Objects = Objects;

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

            //Search Student Name
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.StudentName.Contains(searchString));
            }

            //Search Student Grade
            ViewBag.SearchGrade = searchGrade;
            if (!String.IsNullOrEmpty(searchGrade))
            {
                students = students.Where(s => s.GradeName.Contains(searchGrade));
            }

            //Search Student Class
            ViewBag.SearchClass = searchClass;
            if (!String.IsNullOrEmpty(searchClass))
            {
                students = students.Where(s => s.ClassName.Contains(searchClass));
            }

            //Search Student Obejct
            ViewBag.SearchObject = searchObject;
            if (!String.IsNullOrEmpty(searchObject))
            {
                students = students.Where(s => s.Object.Contains(searchObject));
            }

            //Search Student Status
            ViewBag.SearchStatus = searchStatus;
            if (!String.IsNullOrEmpty(searchStatus))
            {
                students = students.Where(s => s.Status.Contains(searchStatus));
            }

            //Sort
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "Id_desc" : "";
            ViewBag.StuIdDesSortParm = sortOrder == "StuIdDes" ? "StuIdDes" : "StuIdDes";
            ViewBag.StuNameAsSortParm = sortOrder == "StutNameAs" ? "StuNameAs" : "StuNameAs";
            ViewBag.StuNameDesSortParm = sortOrder == "StuNameDes" ? "StuNameDes" : "StuNameDes";
            ViewBag.ClassAsSortParm = sortOrder == "ClassAs" ? "ClassAs" : "ClassAs";
            ViewBag.ClassDesSortParm = sortOrder == "ClassDes" ? "ClassDes" : "ClassDes";
            ViewBag.GradeAsSortParm = sortOrder == "GradeAs" ? "GradeAs" : "GradeAs";
            ViewBag.GradeDesSortParm = sortOrder == "GradeDes" ? "GradeDes" : "GradeDes";
            ViewBag.StuPhoneAsSortParm = sortOrder == "StuPhoneAs" ? "StuPhoneAs" : "StuPhoneAs";
            ViewBag.StuPhoneDesSortParm = sortOrder == "StuPhoneDes" ? "StuPhoneDes" : "StuPhoneDes";
            ViewBag.ObjectAsSortParm = sortOrder == "ObjectAs" ? "ObjectAs" : "ObjectAs";
            ViewBag.ObjectDesSortParm = sortOrder == "ObjectDes" ? "ObjectDes" : "ObjectDes";
            ViewBag.SchFeeAsSortParm = sortOrder == "SchFeeAs" ? "SchFeeAs" : "SchFeeAs";
            ViewBag.SchFeeDesSortParm = sortOrder == "SchFeeDes" ? "SchFeeDes" : "SchFeeDes";
            ViewBag.AdDateAsSortParm = sortOrder == "AdDateAs" ? "AdDateAs" : "AdDateAs";
            ViewBag.AdDateDesSortParm = sortOrder == "AdDateDes" ? "AdDateDes" : "AdDateDes";
            ViewBag.StatusAsSortParm = sortOrder == "StatusAs" ? "StatusAs" : "StatusAs";
            ViewBag.StatusDesSortParm = sortOrder == "StatusDes" ? "StatusDes" : "StatusDes";
            ViewBag.CFeeAsSortParm = sortOrder == "CFeeAs" ? "CFeeAs" : "CFeeAs";
            ViewBag.CFeeDesSortParm = sortOrder == "CFeeDes" ? "CFeeDes" : "CFeeDes";
            ViewBag.PayAsSortParm = sortOrder == "PayAs" ? "PayAs" : "PayAs";
            ViewBag.PayDesSortParm = sortOrder == "PayDes" ? "PayDes" : "PayDes";

            switch (sortOrder)
            {
                case "StuIdDes":
                    students = students.OrderByDescending(s => s.StudentId);
                    break;
                case "StuNameDes":
                    students = students.OrderByDescending(s => s.StudentName);
                    break;
                case "StuNameAs":
                    students = students.OrderBy(s => s.StudentName);
                    break;
                case "ClassDes":
                    students = students.OrderByDescending(s => s.ClassName);
                    break;
                case "ClassAs":
                    students = students.OrderBy(s => s.ClassName);
                    break;
                case "GradeDes":
                    students = students.OrderByDescending(s => s.GradeName);
                    break;
                case "GradeAs":
                    students = students.OrderBy(s => s.GradeName);
                    break;
                case "StuPhoneDes":
                    students = students.OrderByDescending(s => s.PhoneNumber);
                    break;
                case "StuPhoneAs":
                    students = students.OrderBy(s => s.PhoneNumber);
                    break;
                case "ObjectDes":
                    students = students.OrderByDescending(s => s.Object);
                    break;
                case "ObjectAs":
                    students = students.OrderBy(s => s.Object);
                    break;
                case "SchFeeDes":
                    students = students.OrderByDescending(s => s.SchoolFee);
                    break;
                case "SchFeeAs":
                    students = students.OrderBy(s => s.SchoolFee);
                    break;
                case "AdDateDes":
                    students = students.OrderByDescending(s => s.AdmissionDate);
                    break;
                case "AdDateAs":
                    students = students.OrderBy(s => s.AdmissionDate);
                    break;
                case "StatusDes":
                    students = students.OrderByDescending(s => s.Status);
                    break;
                case "StatusAs":
                    students = students.OrderBy(s => s.Status);
                    break;
                case "CFeeDes":
                    students = students.OrderByDescending(s => s.RealMoney);
                    break;
                case "CFeeAs":
                    students = students.OrderBy(s => s.RealMoney);
                    break;
                case "PayDes":
                    students = students.OrderByDescending(s => s.PaymentDate);
                    break;
                case "PayAs":
                    students = students.OrderBy(s => s.PaymentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.StudentId);
                    break;
            }

            return View(students.ToPagedList(pageNumber, pageSize));
        }

        // GET: Student/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            //Create Grade
            List<SelectListItem> studentGrade = new List<SelectListItem>();
            var Grades = from s in db.Grades select s;
            foreach (var gra in Grades)
            {
                studentGrade.Add(new SelectListItem() { Value = gra.GradeName, Text = gra.GradeName });
            }

            ViewBag.StudentGrades = studentGrade;

            //Declare Object
            List<SelectListItem> Objects = new List<SelectListItem>();
            foreach (var oj in db.Discounts)
            {
                Objects.Add(new SelectListItem() { Value = oj.Discounts, Text = oj.Discounts });
            }
            ViewBag.Objects = Objects;

            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StudentId,StudentName,DoB,ClassName,GradeName,SchoolFee,Object,PhoneNumber,AdmissionDate,Status,RealMoney,PaymentDate")] Student student)
        {
            //Create Grade
            List<SelectListItem> studentGrade = new List<SelectListItem>();
            var Grades = from s in db.Grades select s;
            foreach (var gra in Grades)
            {
                studentGrade.Add(new SelectListItem() { Value = gra.GradeName, Text = gra.GradeName });
            }
            ViewBag.StudentGrades = studentGrade;


            //Declare Object
            List<SelectListItem> Objects = new List<SelectListItem>();
            foreach (var oj in db.Discounts)
            {
                Objects.Add(new SelectListItem() { Value = oj.Discounts, Text = oj.Discounts });
            }
            ViewBag.Objects = Objects;


            if (ModelState.IsValid)
            {
                //Auto fill Student Fee
                List<SubmitForm> submitForms = new List<SubmitForm>();
                List<Subject> subjects = new List<Subject>();

                foreach (var smfs in db.SubmitForms)
                {
                    submitForms.Add(smfs);
                }
                foreach (var sjfs in db.Subjects)
                {
                    subjects.Add(sjfs);
                }

                foreach (var submit in submitForms)
                {
                    if (submit.StudentId == student.StudentId)
                    {
                        foreach (var suf in subjects)
                        {
                            if (submit.SubjectName == suf.SubjectName)
                            {
                                student.SchoolFee += suf.TotalFee;
                            }
                        }
                    }
                }

                //Auto fill Real Fee
                if (student.Object == "None")
                {
                    student.RealMoney = student.SchoolFee;
                }
                else
                {

                    if (student.Object.Length == 4)
                    {
                        string cutCharacter = student.Object.Substring(0, 3);
                        student.RealMoney = student.SchoolFee - (student.SchoolFee / 100 * cutCharacter.ToInt());
                    }
                    if (student.Object.Length == 3)
                    {
                        string cutCharacter = student.Object.Substring(0, 2);
                        student.RealMoney = student.SchoolFee - (student.SchoolFee / 100 * cutCharacter.ToInt());
                    }
                }

                db.Students.Add(student);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            //Declare Grade
            List<SelectListItem> studentGrade = new List<SelectListItem>();
            foreach (var gra in db.Grades)
            {
                studentGrade.Add(new SelectListItem() { Value = gra.GradeName, Text = gra.GradeName });
            }

            ViewBag.StudentGrades = studentGrade;


            //Declare Object
            List<SelectListItem> Objects = new List<SelectListItem>();
            foreach (var oj in db.Discounts)
            {
                Objects.Add(new SelectListItem() { Value = oj.Discounts, Text = oj.Discounts });
            }
            ViewBag.Objects = Objects;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StudentId,StudentName,DoB,ClassName,GradeName,PhoneNumber,Object,SchoolFee,AdmissionDate,Status,RealMoney,PaymentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;

                //Auto fill Student Fee
                List<SubmitForm> submitForms = new List<SubmitForm>();
                List<Subject> subjects = new List<Subject>();

                foreach (var smfs in db.SubmitForms)
                {
                    submitForms.Add(smfs);
                }
                foreach (var sjfs in db.Subjects)
                {
                    subjects.Add(sjfs);
                }

                foreach (var submit in submitForms)
                {
                    if (submit.StudentId == student.StudentId)
                    {
                        foreach (var suf in subjects)
                        {
                            if (submit.SubjectName == suf.SubjectName)
                            {
                                student.SchoolFee += suf.TotalFee;
                            }
                        }
                    }
                }

                //Auto fill Real Fee
                if (student.Object == "None")
                {
                    student.RealMoney = student.SchoolFee;
                }
                else
                {
                    if (student.Object.Length == 4)
                    {
                        string cutCharacter = student.Object.Substring(0, 3);
                        student.RealMoney = student.SchoolFee - (student.SchoolFee / 100 * cutCharacter.ToInt());
                    }
                    if (student.Object.Length == 3)
                    {
                        string cutCharacter = student.Object.Substring(0, 2);
                        student.RealMoney = student.SchoolFee - (student.SchoolFee / 100 * cutCharacter.ToInt());
                    }
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Student student = await db.Students.FindAsync(id);
            db.Students.Remove(student);
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
        public enum Status
        {
            Processing,
            Finished
        }

        //GET: Convert to CSV File
        [HttpGet]
        public FileResult ExportCSV()
        {
            string[] stus = new string[] { "StudentId", "StudentName", "DoB", "ClassName", "GradeName", "PhoneNumber", "Object", "SchoolFee", "AdmissionDate", "Status", "RealMoney", "PaymentDate" };
            var students = db.Students;
            string csv = string.Empty;

            foreach (var stu in students)
            {
                csv += stu.StudentId.ToString().Replace(",", ";") + ',';
                csv += stu.StudentName.ToString().Replace(",", ";") + ',';
                csv += stu.DoB.ToString().Replace(",", ";") + ',';
                csv += stu.ClassName.ToString().Replace(",", ";") + ',';
                csv += stu.GradeName.ToString().Replace(",", ";") + ',';
                csv += stu.PhoneNumber.ToString().Replace(",", ";") + ',';
                csv += stu.Object.ToString().Replace(",", ";") + ',';
                csv += stu.SchoolFee.ToString().Replace(",", ";") + ',';
                csv += stu.AdmissionDate.ToString().Replace(",", ";") + ',';
                csv += stu.Status.ToString().Replace(",", ";") + ',';
                csv += stu.RealMoney.ToString().Replace(",", ";") + ',';
                csv += stu.PaymentDate.ToString().Replace(",", ";") + ',';

                csv += "\r\n";
            }

            byte[] bytes = Encoding.ASCII.GetBytes(csv);
            return File(bytes, "text/csv", "Student.csv");
        }

        //GET: Convert to Excel File
        public ActionResult ExportExcel()
        {
            var workbook = new XLWorkbook();
            var students = db.Students;
            var worksheet = workbook.Worksheets.Add("Students");
            var row = 1;

            worksheet.Cell(row, 1).Value = "Student Id";
            worksheet.Cell(row, 2).Value = "Student Name";
            worksheet.Cell(row, 3).Value = "Date Of Birth";
            worksheet.Cell(row, 4).Value = "Class";
            worksheet.Cell(row, 5).Value = "Grade";
            worksheet.Cell(row, 6).Value = "Phone Number";
            worksheet.Cell(row, 7).Value = "Object";
            worksheet.Cell(row, 8).Value = "School Fee";
            worksheet.Cell(row, 9).Value = "Admission Date";
            worksheet.Cell(row, 10).Value = "Status";
            worksheet.Cell(row, 11).Value = "Real Money";
            worksheet.Cell(row, 12).Value = "Payment Date";

            foreach (var stu in students)
            {
                row++;
                worksheet.Cell(row, 1).Value = stu.StudentId;
                worksheet.Cell(row, 2).Value = stu.StudentName;
                worksheet.Cell(row, 3).Value = stu.DoB;
                worksheet.Cell(row, 4).Value = stu.ClassName;
                worksheet.Cell(row, 5).Value = stu.GradeName;
                worksheet.Cell(row, 6).Value = stu.PhoneNumber;
                worksheet.Cell(row, 7).Value = stu.Object;
                worksheet.Cell(row, 8).Value = stu.SchoolFee;
                worksheet.Cell(row, 9).Value = stu.AdmissionDate;
                worksheet.Cell(row, 10).Value = stu.Status;
                worksheet.Cell(row, 11).Value = stu.RealMoney;
                worksheet.Cell(row, 12).Value = stu.PaymentDate;
            }

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "student.xlsx");
        }

        //GET: Student/Form
        static FormModel FormModels = new FormModel();
        public ActionResult Form(string id, Receipt receipt)
        {
            if (User.Identity.GetUserId() == null)
            {
                return HttpNotFound();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> Present = new List<SelectListItem>();
            foreach (var pre in db.Presents)
            {
                Present.Add(new SelectListItem() { Value = pre.Presents, Text = pre.Presents });
            }
            ViewBag.Present = Present;

            //Create Storage List
            var Subject = new List<Subject>();
            var Student = new List<Student>();
            var Receipt = new List<Receipt>();

            //Create Compared List
            var Subjects = new List<Subject>();
            var Students = new List<Student>();
            var Forms = new List<SubmitForm>();

            //Fulfil the Storage List
            foreach (var sf in db.SubmitForms)
            {
                Forms.Add(sf);
            }
            foreach (var su in db.Subjects)
            {
                Subjects.Add(su);
            }
            foreach (var st in db.Students)
            {
                Students.Add(st);
            }

            //Compare and Add to List
            foreach (var st in Students)
            {
                if (st.StudentId == id)
                {
                    Student.Add(st);
                }
            }
            foreach (var sf in Forms)
            {
                if (sf.StudentId == id)
                {
                    foreach (var su in Subjects)
                    {
                        if (sf.SubjectName == su.SubjectName)
                        {
                            Subject.Add(su);
                        }
                    }
                }
            }

            //Compare to take receipt table
            foreach (var sf in Forms)
            {
                if (sf.StudentId == id)
                {
                    receipt.StudentID = sf.StudentId;
                    receipt.StudentName = sf.StudentName;
                    foreach (var su in Subjects)
                    {
                        if (sf.SubjectName == su.SubjectName)
                        {
                            receipt.TotalDuration += su.Duration;
                            receipt.FeePerUnit = su.PerUnit;
                            receipt.TotalSchoolFee += su.TotalFee;
                            receipt.DiscountObject = student.Object.ToString();

                            if (receipt.DiscountObject == "None")
                            {
                                receipt.Money = student.SchoolFee;
                            }

                            else
                            {
                                if (receipt.DiscountObject.Length == 4)
                                {
                                    string cutCharacter = student.Object.Substring(0, 3);
                                    receipt.Money = receipt.TotalSchoolFee - (receipt.TotalSchoolFee / 100 * cutCharacter.ToInt());
                                }
                                if (receipt.DiscountObject.Length == 3)
                                {
                                    string cutCharacter = student.Object.Substring(0, 2);
                                    receipt.Money = receipt.TotalSchoolFee - (receipt.TotalSchoolFee / 100 * cutCharacter.ToInt());
                                }
                            }
                        }
                    }
                    receipt.CollectedDate = DateTime.Now;
                }
            }

            foreach (var user in db.Users)
            {
                if (user.Id == User.Identity.GetUserId())
                {
                    receipt.Collecter = user.FullName;
                }
            };

            FormModels.Subjectss = Subject;
            FormModels.Studentss = Student;
            FormModels.Receipt = receipt;

            //Show Model
            return View(FormModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Form([Bind(Include = "NO,Collecter,StudentID,StudentName,TotalDuration,FeePerUnit," +
            "TotalSchoolFee,CollectMethod,DiscountObject,Sale,SaleType,TotalSubFeeCheck,TotalSubFee,SubFeeDescription,CollectedDate,Money")]  Receipt Receipt)
        {
            if (ModelState.IsValid)
            {
                Receipt.NO = FormModels.Receipt.NO;
                Receipt.CollectedDate = FormModels.Receipt.CollectedDate;
                Receipt.FeePerUnit = FormModels.Receipt.FeePerUnit;
                Receipt.TotalSchoolFee = FormModels.Receipt.TotalSchoolFee;
                Receipt.DiscountObject= FormModels.Receipt.DiscountObject;
                Receipt.StudentID = FormModels.Receipt.StudentID;
                Receipt.StudentName= FormModels.Receipt.StudentName;
                Receipt.Collecter = FormModels.Receipt.Collecter;
                Receipt.Money = FormModels.Receipt.Money;
                Receipt.TotalDuration = FormModels.Receipt.TotalDuration;

                db.Receipts.Add(Receipt);
                await db.SaveChangesAsync();
                return View("Print");
            }
            return View(Receipt);
        }
    }
}
