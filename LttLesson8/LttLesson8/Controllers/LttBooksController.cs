using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LttLesson8.Models;

namespace LttLesson8.Controllers
{
    public class LttBooksController : Controller
    {
        private LttBookStore db = new LttBookStore();

        // GET: LttBooks
        public ActionResult LttIndex()
        {
            return View(db.LttBooks.ToList());
        }

        // GET: LttBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LttBook lttBook = db.LttBooks.Find(id);
            if (lttBook == null)
            {
                return HttpNotFound();
            }
            return View(lttBook);
        }

        // GET: LttBooks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LttBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LttBookId,LttTitle,LttAuthor,LttYear,LttPublisher,LttPicture,LttCategoryId")] LttBook lttBook)
        {
            if (ModelState.IsValid)
            {
                db.LttBooks.Add(lttBook);
                db.SaveChanges();
                return RedirectToAction("LttIndex");
            }

            return View(lttBook);
        }

        // GET: LttBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LttBook lttBook = db.LttBooks.Find(id);
            if (lttBook == null)
            {
                return HttpNotFound();
            }
            return View(lttBook);
        }

        // POST: LttBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LttBookId,LttTitle,LttAuthor,LttYear,LttPublisher,LttPicture,LttCategoryId")] LttBook lttBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lttBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LttIndex");
            }
            return View(lttBook);
        }

        // GET: LttBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LttBook lttBook = db.LttBooks.Find(id);
            if (lttBook == null)
            {
                return HttpNotFound();
            }
            return View(lttBook);
        }

        // POST: LttBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LttBook lttBook = db.LttBooks.Find(id);
            db.LttBooks.Remove(lttBook);
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
