using LttLesson8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LttLesson8.Controllers
{
    public class LttCategoryController : Controller
    {
        private static LttBookStore _LttbookStore;
        public LttCategoryController()
        {
            _LttbookStore = new LttBookStore();
        }
        // GET: LttCategory
        public ActionResult LttIndex()
        {
            var lttCategory = _LttbookStore.LttCategories.ToList();
            return View(lttCategory);
        }
        public ActionResult LttCreate() 
        {
            var lttCategory = new LttCategory();
            return View();
        }
        [HttpPost]
        public ActionResult LttCreate(LttCategory lttCategory)
        {
            _LttbookStore.LttCategories.Add(lttCategory);
            _LttbookStore.SaveChanges();
            return RedirectToAction("LttIndex");
        }
    }
}