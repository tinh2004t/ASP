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
    public class SinhViensController : Controller
    {
        private lttqlsinhvienEntities db = new lttqlsinhvienEntities();

        // GET: SinhViens
        public ActionResult lttIndex()
        {
            var sinhVien = db.SinhVien.Include(s => s.Khoa);
            return View(sinhVien.ToList());
        }

        // GET: SinhViens/Details/5
        public ActionResult lttDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhVien.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // GET: SinhViens/Create
        public ActionResult lttCreate()
        {
            ViewBag.MaKH = new SelectList(db.Khoa, "MaKH", "TenKH");
            return View();
        }

        // POST: SinhViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult lttCreate([Bind(Include = "MaSV,HoSV,TenSV,Phai,NgaySinh,NoiSinh,MaKH,HocBong,DTB")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                db.SinhVien.Add(sinhVien);
                db.SaveChanges();
                return RedirectToAction("lttIndex");
            }

            ViewBag.MaKH = new SelectList(db.Khoa, "MaKH", "TenKH", sinhVien.MaKH);
            return View(sinhVien);
        }

        // GET: SinhViens/Edit/5
        public ActionResult lttEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhVien.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.Khoa, "MaKH", "TenKH", sinhVien.MaKH);
            return View(sinhVien);
        }

        // POST: SinhViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult lttEdit([Bind(Include = "MaSV,HoSV,TenSV,Phai,NgaySinh,NoiSinh,MaKH,HocBong,DTB")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("lttIndex");
            }
            ViewBag.MaKH = new SelectList(db.Khoa, "MaKH", "TenKH", sinhVien.MaKH);
            return View(sinhVien);
        }

        // GET: SinhViens/Delete/5
        public ActionResult lttDelete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhVien = db.SinhVien.Find(id);
            if (sinhVien == null)
            {
                return HttpNotFound();
            }
            return View(sinhVien);
        }

        // POST: SinhViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SinhVien sinhVien = db.SinhVien.Find(id);
            db.SinhVien.Remove(sinhVien);
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
