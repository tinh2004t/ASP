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
    public class ltt_TAGGIAController : Controller
    {
        private LeTuanTinh_2210900130Entities1 db = new LeTuanTinh_2210900130Entities1();

        // GET: ltt_TAGGIA
        public ActionResult LttIndex()
        {
            return View(db.ltt_TAGGIA.ToList());
        }

        // GET: ltt_TAGGIA/LttDetails/5
        public ActionResult LttDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ltt_TAGGIA ltt_TAGGIA = db.ltt_TAGGIA.Find(id);
            if (ltt_TAGGIA == null)
            {
                return HttpNotFound();
            }
            return View(ltt_TAGGIA);
        }

        // GET: ltt_TAGGIA/LttCreate
        public ActionResult LttCreate()
        {
            return View();
        }

        // POST: ltt_TAGGIA/LttCreate
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LttCreate([Bind(Include = "ltt_MaTG,ltt_TenTG")] ltt_TAGGIA ltt_TAGGIA)
        {
            if (ModelState.IsValid)
            {
                db.ltt_TAGGIA.Add(ltt_TAGGIA);
                db.SaveChanges();
                return RedirectToAction("LttIndex");
            }

            return View(ltt_TAGGIA);
        }

        // GET: ltt_TAGGIA/LttEdit/5
        public ActionResult LttEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ltt_TAGGIA ltt_TAGGIA = db.ltt_TAGGIA.Find(id);
            if (ltt_TAGGIA == null)
            {
                return HttpNotFound();
            }
            return View(ltt_TAGGIA);
        }

        // POST: ltt_TAGGIA/LttEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LttEdit([Bind(Include = "ltt_MaTG,ltt_TenTG")] ltt_TAGGIA ltt_TAGGIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ltt_TAGGIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LttIndex");
            }
            return View(ltt_TAGGIA);
        }

        // GET: ltt_TAGGIA/LttDelete/5
        public ActionResult LttDelete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ltt_TAGGIA ltt_TAGGIA = db.ltt_TAGGIA.Find(id);
            if (ltt_TAGGIA == null)
            {
                return HttpNotFound();
            }
            return View(ltt_TAGGIA);
        }

        // POST: ltt_TAGGIA/LttDelete/5
        [HttpPost, ActionName("LttDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult LttDeleteConfirmed(string id)
        {
            ltt_TAGGIA ltt_TAGGIA = db.ltt_TAGGIA.Find(id);
            db.ltt_TAGGIA.Remove(ltt_TAGGIA);
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
