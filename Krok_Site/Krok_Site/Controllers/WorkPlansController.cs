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
    public class WorkPlansController : Controller
    {
        private Model1 db = new Model1();

        // GET: WorkPlans
        public ActionResult Index(string GroupName, string CourseNumber, string SubjectName)
        {


            var GroupLst = new List<string>();

            var GroupQry = from l in db.WorkPlan
                           join r in db.Groups on l.GroupId equals r.Id
                           select r.GroupName;

            var CourseLst = new List<string>();

            var CourseQry = from l in db.WorkPlan
                            join r in db.Groups on l.GroupId equals r.Id
                            select r.Course;

            var SubLst = new List<string>();

            var SubQry = from l in db.WorkPlan
                         join r in db.Subjects on l.SubjectId equals r.Id
                         select r.Name;



            GroupLst.AddRange(GroupQry.Distinct());
            ViewBag.GroupName = new SelectList(GroupLst);

            CourseLst.AddRange(CourseQry.Distinct());
            ViewBag.CourseNumber = new SelectList(CourseLst);

            SubLst.AddRange(SubQry.Distinct());
            ViewBag.SubjectName = new SelectList(SubLst);

            var table = from m in db.WorkPlan select m;



            if (!string.IsNullOrEmpty(GroupName))
            {
                table = table.Where(y => y.Groups.GroupName == GroupName);
            }
            if (!string.IsNullOrEmpty(CourseNumber))
            {
                table = table.Where(y => y.Groups.Course == CourseNumber);
            }
            if (!string.IsNullOrEmpty(SubjectName))
            {
                table = table.Where(y => y.Subjects.Name == SubjectName);
            }
            return View(table.ToList());
        }

        // GET: WorkPlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkPlan workPlan = db.WorkPlan.Find(id);
            if (workPlan == null)
            {
                return HttpNotFound();
            }
            return View(workPlan);
        }

        // GET: WorkPlans/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName");
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name");
            return View();
        }

        // POST: WorkPlans/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StudentId,GroupId,Semester,TimeAll,LectionTime,LaboratoryTime,ConsultationTime,exam,Credit,CourseWork,Degree,Credits")] WorkPlan workPlan)
        {
            if (ModelState.IsValid)
            {
                db.WorkPlan.Add(workPlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", workPlan.GroupId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", workPlan.StudentId);
            return View(workPlan);
        }

        // GET: WorkPlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkPlan workPlan = db.WorkPlan.Find(id);
            if (workPlan == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", workPlan.GroupId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", workPlan.StudentId);
            return View(workPlan);
        }

        // POST: WorkPlans/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentId,GroupId,Semester,TimeAll,LectionTime,LaboratoryTime,ConsultationTime,exam,Credit,CourseWork,Degree,Credits")] WorkPlan workPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", workPlan.GroupId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", workPlan.StudentId);
            return View(workPlan);
        }

        // GET: WorkPlans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkPlan workPlan = db.WorkPlan.Find(id);
            if (workPlan == null)
            {
                return HttpNotFound();
            }
            return View(workPlan);
        }

        // POST: WorkPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkPlan workPlan = db.WorkPlan.Find(id);
            db.WorkPlan.Remove(workPlan);
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