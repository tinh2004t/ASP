using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LttLesson11.Models;
using LttLesson11.ModelViews;

namespace LttLesson11.Controllers
{
    public class LttTaiKhoansController : Controller
    {
        private LttLesson11DbEntities db = new LttLesson11DbEntities();

        // GET: LttTaiKhoans
        public ActionResult Index()
        {
            return View(db.LttTaiKhoans.ToList());
        }

        // GET: LttTaiKhoans/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LttTaiKhoan lttTaiKhoan = db.LttTaiKhoans.Find(id);
            if (lttTaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(lttTaiKhoan);
        }

        // GET: LttTaiKhoans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LttTaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LttId,LttUserName,LttFPassword,LttFullName,LttAge,LttEmail,LttPhone,LttStatus")] LttTaiKhoan lttTaiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.LttTaiKhoans.Add(lttTaiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lttTaiKhoan);
        }

        // GET: LttTaiKhoans/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LttTaiKhoan lttTaiKhoan = db.LttTaiKhoans.Find(id);
            if (lttTaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(lttTaiKhoan);
        }

        // POST: LttTaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LttId,LttUserName,LttFPassword,LttFullName,LttAge,LttEmail,LttPhone,LttStatus")] LttTaiKhoan lttTaiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lttTaiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lttTaiKhoan);
        }

        // GET: LttTaiKhoans/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LttTaiKhoan lttTaiKhoan = db.LttTaiKhoans.Find(id);
            if (lttTaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(lttTaiKhoan);
        }

        // POST: LttTaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LttTaiKhoan lttTaiKhoan = db.LttTaiKhoans.Find(id);
            db.LttTaiKhoans.Remove(lttTaiKhoan);
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
        // Form đăng nhập hệ thống
        public ActionResult LttLogin()
        {
            var LttModel = new LttLoginModel();
            return View(LttModel);
        }

        [HttpPost]
        public ActionResult LttLogin(LttLoginModel LttModel)
        {
            // khi người dùng nhấn nút đăng nhập; xử lý và tìm kiến, so sanh trong db

            var LttCheckLogin = db.LttTaiKhoans.Where(x => x.LttUserName.Equals(LttModel.LttUserName) && x.LttFPassword.Equals(LttModel.LttPassword)).FirstOrDefault();
            if (LttCheckLogin != null)
            {
                //Lưu trữ session
                Session["LttMember"] = LttCheckLogin;

                return Redirect("/");
            }
            return View(LttModel);
        }
        public ActionResult Logout()
        {
            Session.Remove("LttMember");
            return Redirect("/");
        }
    }
}
