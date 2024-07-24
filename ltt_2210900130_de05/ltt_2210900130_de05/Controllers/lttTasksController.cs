using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ltt_2210900130_de05.Controllers
{
    public class lttTasksController : Controller
    {
        private letuantinh_2210900130_de05Entities db = new letuantinh_2210900130_de05Entities();

        // GET: lttTasks
        public ActionResult lttIndex()
        {
            return View(db.lttTask.ToList());
        }

        // GET: lttTasks/lttDetails/5
        public ActionResult lttDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lttTask lttTask = db.lttTask.Find(id);
            if (lttTask == null)
            {
                return HttpNotFound();
            }
            return View(lttTask);
        }

        // GET: lttTasks/lttCreate
        public ActionResult lttCreate()
        {
            return View();
        }

        // POST: lttTasks/lttCreate
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult lttCreate([Bind(Include = "lttTaskId,lttTaskName,lttTaskLevel,lttStartDate,lttTaskStatus")] lttTask lttTask)
        {
            if (ModelState.IsValid)
            {
                db.lttTask.Add(lttTask);
                db.SaveChanges();
                return RedirectToAction("lttIndex");
            }

            return View(lttTask);
        }

        // GET: lttTasks/lttEdit/5
        public ActionResult lttEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lttTask lttTask = db.lttTask.Find(id);
            if (lttTask == null)
            {
                return HttpNotFound();
            }
            return View(lttTask);
        }

        // POST: lttTasks/lttEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult lttEdit([Bind(Include = "lttTaskId,lttTaskName,lttTaskLevel,lttStartDate,lttTaskStatus")] lttTask lttTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lttTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("lttIndex");
            }
            return View(lttTask);
        }

        // GET: lttTasks/lttDelete/5
        public ActionResult lttDelete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lttTask lttTask = db.lttTask.Find(id);
            if (lttTask == null)
            {
                return HttpNotFound();
            }
            return View(lttTask);
        }

        // POST: lttTasks/lttDelete/5
        [HttpPost, ActionName("lttDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult lttDeleteConfirmed(string id)
        {
            lttTask lttTask = db.lttTask.Find(id);
            db.lttTask.Remove(lttTask);
            db.SaveChanges();
            return RedirectToAction("lttIndex");
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
