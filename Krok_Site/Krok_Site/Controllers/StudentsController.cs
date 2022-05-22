using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Krok_Site.Models;

namespace Krok_Site.Controllers
{
    public class StudentsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Students
        public ActionResult Index(string CourseNumber, string GroupName)
        {


            var CourseLst = new List<string>();

            var GroupLst = new List<string>();


            var CourseQry = from s in db.Students
                            join g in db.Groups on s.GroupId equals g.Id
                            select g.GroupName;

            var GroupQry = from s in db.Students
                           join g in db.Groups on s.GroupId equals g.Id
                           select g.Course;



            CourseLst.AddRange(CourseQry.Distinct());
            ViewBag.CourseNumber = new SelectList(CourseLst);

            GroupLst.AddRange(GroupQry.Distinct());
            ViewBag.GroupName = new SelectList(GroupLst);

            var students = from m in db.Students select m;

            var groups = from n in db.Groups select n;

            if (!string.IsNullOrEmpty(CourseNumber))
            {
                students = students.Where(y => y.Groups.Course == CourseNumber);
            }
            if (!string.IsNullOrEmpty(GroupName))
            {
                groups = groups.Where(y => y.GroupName == GroupName);
            }


            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName");
            return View();
        }

        // POST: Students/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LastName,MiddleName,GroupId")] Students students)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(students);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", students.GroupId);
            return View(students);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", students.GroupId);
            return View(students);
        }

        // POST: Students/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LastName,MiddleName,GroupId")] Students students)
        {
            if (ModelState.IsValid)
            {
                db.Entry(students).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", students.GroupId);
            return View(students);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Students students = db.Students.Find(id);
            db.Students.Remove(students);
            db.SaveChanges();
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
