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
    public class TimeTablesController : Controller
    {
        private Model1 db = new Model1();

        // GET: TimeTables
        public ActionResult Index(string SubjectName, string GroupName, string DepartmentName)
        {
            /*   .Where(m => m.Date <= maxDate && m.Date >= minDate)*/
            var DeptLst = new List<string>();
            var SubLst = new List<string>();
            var GroupLst = new List<string>();

            var DeptQry = from t in db.TimeTables
                          join d in db.Departments on t.DepartmentId equals d.Id
                          select d.Name;
            var SubQry = from t in db.TimeTables
                         join s in db.Subjects on t.SubjectId equals s.Id
                         select s.Name;
            var GroupQry = from t in db.TimeTables
                           join g in db.Groups on t.GroupId equals g.Id
                           select g.GroupName;

            DeptLst.AddRange(DeptQry.Distinct());
            ViewBag.DepartmentName = new SelectList(DeptLst);

            SubLst.AddRange(SubQry.Distinct());
            ViewBag.SubjectName = new SelectList(SubLst);

            GroupLst.AddRange(GroupQry.Distinct());
            ViewBag.GroupName = new SelectList(GroupLst);

            var timetable = from m in db.TimeTables select m;

            var TableDate = from s in timetable
                              group s by s.DateTime;

            if (!string.IsNullOrEmpty(SubjectName))
            {
                timetable = timetable.Where(y => y.Subjects.Name == SubjectName);
            }
            if (!string.IsNullOrEmpty(GroupName))
            {
                timetable = timetable.Where(y => y.Groups.GroupName == GroupName);
            }
            if (!string.IsNullOrEmpty(DepartmentName))
            {
                timetable = timetable.Where(y => y.Departments.Name == DepartmentName);
            }

       

            return View(timetable.ToList().OrderBy(y => y.DateTime));

        }

        // GET: TimeTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeTables timeTables = db.TimeTables.Find(id);
            if (timeTables == null)
            {
                return HttpNotFound();
            }
            return View(timeTables);
        }

        // GET: TimeTables/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName");
            ViewBag.ProfessorId = new SelectList(db.Professors, "Id", "Name");
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name");
            return View();
        }

        // POST: TimeTables/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GroupId,SubjectId,TipeLesson,ProfessorId,LectureHall,Time,DateTime,NomberPair,DepartmentId")] TimeTables timeTables)
        {
            if (ModelState.IsValid)
            {
                db.TimeTables.Add(timeTables);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", timeTables.DepartmentId);
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", timeTables.GroupId);
            ViewBag.ProfessorId = new SelectList(db.Professors, "Id", "Name", timeTables.ProfessorId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", timeTables.SubjectId);
            return View(timeTables);
        }

        // GET: TimeTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeTables timeTables = db.TimeTables.Find(id);
            if (timeTables == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", timeTables.DepartmentId);
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", timeTables.GroupId);
            ViewBag.ProfessorId = new SelectList(db.Professors, "Id", "Name", timeTables.ProfessorId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", timeTables.SubjectId);
            return View(timeTables);
        }

        // POST: TimeTables/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GroupId,SubjectId,TipeLesson,ProfessorId,LectureHall,Time,DateTime,NomberPair,DepartmentId")] TimeTables timeTables)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeTables).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", timeTables.DepartmentId);
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", timeTables.GroupId);
            ViewBag.ProfessorId = new SelectList(db.Professors, "Id", "Name", timeTables.ProfessorId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "Name", timeTables.SubjectId);
            return View(timeTables);
        }

        // GET: TimeTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeTables timeTables = db.TimeTables.Find(id);
            if (timeTables == null)
            {
                return HttpNotFound();
            }
            return View(timeTables);
        }

        // POST: TimeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeTables timeTables = db.TimeTables.Find(id);
            db.TimeTables.Remove(timeTables);
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
