using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnTap_Ltt_QlSinhVien.Models;

namespace OnTap_Ltt_QlSinhVien.Controllers
{
    public class MonHocsController : Controller
    {
        private lttqlsinhvienEntities db = new lttqlsinhvienEntities();

        // GET: MonHocs
        public ActionResult lttIndex()
        {
            return View(db.MonHoc.ToList());
        }

        // GET: MonHocs/lttDetails/5
        public ActionResult lttDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonHoc monHoc = db.MonHoc.Find(id);
            if (monHoc == null)
            {
                return HttpNotFound();
            }
            return View(monHoc);
        }

        // GET: MonHocs/lttCreate
        public ActionResult lttCreate()
        {
            return View();
        }

        // POST: MonHocs/lttCreate
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult lttCreate([Bind(Include = "MaMH,TenMH,Sotiet")] MonHoc monHoc)
        {
            if (ModelState.IsValid)
            {
                db.MonHoc.Add(monHoc);
                db.SaveChanges();
                return RedirectToAction("lttIndex");
            }

            return View(monHoc);
        }

        // GET: MonHocs/lttEdit/5
        public ActionResult lttEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonHoc monHoc = db.MonHoc.Find(id);
            if (monHoc == null)
            {
                return HttpNotFound();
            }
            return View(monHoc);
        }

        // POST: MonHocs/lttEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult lttEdit([Bind(Include = "MaMH,TenMH,Sotiet")] MonHoc monHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("lttIndex");
            }
            return View(monHoc);
        }

        // GET: MonHocs/lttDelete/5
        public ActionResult lttDelete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonHoc monHoc = db.MonHoc.Find(id);
            if (monHoc == null)
            {
                return HttpNotFound();
            }
            return View(monHoc);
        }

        // POST: MonHocs/lttDelete/5
        [HttpPost, ActionName("lttDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult lttDeleteConfirmed(string id)
        {
            MonHoc monHoc = db.MonHoc.Find(id);
            db.MonHoc.Remove(monHoc);
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
