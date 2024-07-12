using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeTuanTinh_2210900130.Models;

namespace LeTuanTinh_2210900130.Controllers
{
    public class ltt_SACHController : Controller
    {
        private LeTuanTinh_2210900130Entities1 db = new LeTuanTinh_2210900130Entities1();

        // GET: ltt_SACH
        public ActionResult LttIndex()
        {
            var ltt_SACH = db.ltt_SACH.Include(l => l.ltt_TAGGIA);
            return View(ltt_SACH.ToList());
        }

        // GET: ltt_SACH/LttDetails/5
        public ActionResult LttDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ltt_SACH ltt_SACH = db.ltt_SACH.Find(id);
            if (ltt_SACH == null)
            {
                return HttpNotFound();
            }
            return View(ltt_SACH);
        }

        // GET: ltt_SACH/LttCreate
        public ActionResult LttCreate()
        {
            ViewBag.ltt_MaTG = new SelectList(db.ltt_TAGGIA, "ltt_MaTG", "ltt_TenTG");
            return View();
        }

        // POST: ltt_SACH/LttCreate
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LttCreate([Bind(Include = "ltt_MaSach,ltt_TenSach,ltt_SoTrang,ltt_NamXB,ltt_MaTG,ltt_TrangThai")] ltt_SACH ltt_SACH)
        {
            if (ModelState.IsValid)
            {
                db.ltt_SACH.Add(ltt_SACH);
                db.SaveChanges();
                return RedirectToAction("LttIndex");
            }

            ViewBag.ltt_MaTG = new SelectList(db.ltt_TAGGIA, "ltt_MaTG", "ltt_TenTG", ltt_SACH.ltt_MaTG);
            return View(ltt_SACH);
        }

        // GET: ltt_SACH/LttEdit/5
        public ActionResult LttEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ltt_SACH ltt_SACH = db.ltt_SACH.Find(id);
            if (ltt_SACH == null)
            {
                return HttpNotFound();
            }
            ViewBag.ltt_MaTG = new SelectList(db.ltt_TAGGIA, "ltt_MaTG", "ltt_TenTG", ltt_SACH.ltt_MaTG);
            return View(ltt_SACH);
        }

        // POST: ltt_SACH/LttEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LttEdit([Bind(Include = "ltt_MaSach,ltt_TenSach,ltt_SoTrang,ltt_NamXB,ltt_MaTG,ltt_TrangThai")] ltt_SACH ltt_SACH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ltt_SACH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LttIndex");
            }
            ViewBag.ltt_MaTG = new SelectList(db.ltt_TAGGIA, "ltt_MaTG", "ltt_TenTG", ltt_SACH.ltt_MaTG);
            return View(ltt_SACH);
        }

        // GET: ltt_SACH/LttDelete/5
        public ActionResult LttDelete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ltt_SACH ltt_SACH = db.ltt_SACH.Find(id);
            if (ltt_SACH == null)
            {
                return HttpNotFound();
            }
            return View(ltt_SACH);
        }

        // POST: ltt_SACH/LttDelete/5
        [HttpPost, ActionName("LttDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult LttDeleteConfirmed(string id)
        {
            ltt_SACH ltt_SACH = db.ltt_SACH.Find(id);
            db.ltt_SACH.Remove(ltt_SACH);
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
