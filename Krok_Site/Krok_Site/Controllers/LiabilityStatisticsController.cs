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
    public class LiabilityStatisticsController : Controller
    {
        private Model1 db = new Model1();

        // GET: LiabilityStatistics
        public ActionResult Index(string Mounth, string DepatentName, string GroupName, string CourseNumber, string SubjectName)
        {
            var MountLst = new List<string>();

            var MounthQry = from l in db.LiabilityStatistics
                            join r in db.Responsebles on l.ResponsebleId equals r.Id
                            select r.Mounth;

            var DepLst = new List<string>();

            var DepQry = from l in db.LiabilityStatistics
                         join r in db.Departments on l.DepartmentId equals r.Id
                         select r.Name;

            var GroupLst = new List<string>();

            var GroupQry = from l in db.LiabilityStatistics
                           join r in db.Groups on l.GroupId equals r.Id
                           select r.GroupName;
            var CourseLst = new List<string>();

            var CourseQry = from l in db.LiabilityStatistics
                            join r in db.Groups on l.GroupId equals r.Id
                            select r.Course;
            var SubLst = new List<string>();

            var SubQry = from l in db.Responsebles
                         join r in db.Subjects on l.SubjectId equals r.Id
                         select r.Name;


            MountLst.AddRange(MounthQry.Distinct());
            ViewBag.Mounth = new SelectList(MountLst);

            DepLst.AddRange(DepQry.Distinct());
            ViewBag.DepatentName = new SelectList(DepLst);

            GroupLst.AddRange(GroupQry.Distinct());
            ViewBag.GroupName = new SelectList(GroupLst);

            CourseLst.AddRange(CourseQry.Distinct());
            ViewBag.CourseNumber = new SelectList(CourseLst);

            SubLst.AddRange(SubQry.Distinct());
            ViewBag.SubjectName = new SelectList(SubLst);

            var table = from m in db.LiabilityStatistics select m;

            if (!string.IsNullOrEmpty(Mounth))
            {
                table = table.Where(y => y.Responsebles.Mounth == Mounth);
            }
            if (!string.IsNullOrEmpty(DepatentName))
            {
                table = table.Where(y => y.Departments.Name == DepatentName);
            }
            if (!string.IsNullOrEmpty(GroupName))
            {
                table = table.Where(y => y.Groups.GroupName == GroupName);
            }
            if (!string.IsNullOrEmpty(CourseNumber))
            {
                table = table.Where(y => y.Groups.Course == CourseNumber);
            }
            return View(table.ToList());
        }

        // GET: LiabilityStatistics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiabilityStatistics liabilityStatistics = db.LiabilityStatistics.Find(id);
            if (liabilityStatistics == null)
            {
                return HttpNotFound();
            }
            return View(liabilityStatistics);
        }

        // GET: LiabilityStatistics/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName");
            ViewBag.ResponsebleId = new SelectList(db.Responsebles, "Id", "Mounth");
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name");
            return View();
        }

        // POST: LiabilityStatistics/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GroupId,StudentId,DepartmentId,ResponsebleId")] LiabilityStatistics liabilityStatistics)
        {
            if (ModelState.IsValid)
            {
                db.LiabilityStatistics.Add(liabilityStatistics);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", liabilityStatistics.DepartmentId);
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", liabilityStatistics.GroupId);
            ViewBag.ResponsebleId = new SelectList(db.Responsebles, "Id", "Mounth", liabilityStatistics.ResponsebleId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", liabilityStatistics.StudentId);
            return View(liabilityStatistics);
        }

        // GET: LiabilityStatistics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiabilityStatistics liabilityStatistics = db.LiabilityStatistics.Find(id);
            if (liabilityStatistics == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", liabilityStatistics.DepartmentId);
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", liabilityStatistics.GroupId);
            ViewBag.ResponsebleId = new SelectList(db.Responsebles, "Id", "Mounth", liabilityStatistics.ResponsebleId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", liabilityStatistics.StudentId);
            return View(liabilityStatistics);
        }

        // POST: LiabilityStatistics/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GroupId,StudentId,DepartmentId,ResponsebleId")] LiabilityStatistics liabilityStatistics)
        {
            if (ModelState.IsValid)
            {
                db.Entry(liabilityStatistics).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", liabilityStatistics.DepartmentId);
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", liabilityStatistics.GroupId);
            ViewBag.ResponsebleId = new SelectList(db.Responsebles, "Id", "Mounth", liabilityStatistics.ResponsebleId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Name", liabilityStatistics.StudentId);
            return View(liabilityStatistics);
        }

        // GET: LiabilityStatistics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiabilityStatistics liabilityStatistics = db.LiabilityStatistics.Find(id);
            if (liabilityStatistics == null)
            {
                return HttpNotFound();
            }
            return View(liabilityStatistics);
        }

        // POST: LiabilityStatistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LiabilityStatistics liabilityStatistics = db.LiabilityStatistics.Find(id);
            db.LiabilityStatistics.Remove(liabilityStatistics);
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
