using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LttLesson09.Models;

namespace LttLesson09.Controllers
{
    public class LttKhoasController : Controller
    {
        private LttK22CNTLesson09DbEntities2 db = new LttK22CNTLesson09DbEntities2();

        // GET: LttKhoas
        public ActionResult LttIndex()
        {
            return View(db.LttKhoa.ToList());
        }

        // GET: LttKhoas/Details/5
        public ActionResult LttDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LttKhoa lttKhoa = db.LttKhoa.Find(id);
            if (lttKhoa == null)
            {
                return HttpNotFound();
            }
            return View(lttKhoa);
        }

        // GET: LttKhoas/Create
        public ActionResult LttCreate()
        {
            return View();
        }

        // POST: LttKhoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LttCreate([Bind(Include = "LttMaKH,LttTenKH,LttTrangThai")] LttKhoa lttKhoa)
        {
            if (ModelState.IsValid)
            {
                db.LttKhoa.Add(lttKhoa);
                db.SaveChanges();
                return RedirectToAction("LttIndex");
            }

            return View(lttKhoa);
        }

        // GET: LttKhoas/Edit/5
        public ActionResult LttEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LttKhoa lttKhoa = db.LttKhoa.Find(id);
            if (lttKhoa == null)
            {
                return HttpNotFound();
            }
            return View(lttKhoa);
        }

        // POST: LttKhoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LttMaKH,LttTenKH,LttTrangThai")] LttKhoa lttKhoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lttKhoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LttIndex");
            }
            return View(lttKhoa);
        }

        // GET: LttKhoas/Delete/5
        public ActionResult LttDelete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LttKhoa lttKhoa = db.LttKhoa.Find(id);
            if (lttKhoa == null)
            {
                return HttpNotFound();
            }
            return View(lttKhoa);
        }

        // POST: LttKhoas/Delete/5
        [HttpPost, ActionName("LttDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LttKhoa lttKhoa = db.LttKhoa.Find(id);
            db.LttKhoa.Remove(lttKhoa);
            db.SaveChanges();
            return RedirectToAction("LttIndex");
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
