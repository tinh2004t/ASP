using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ltt_2210900130_de05.Controllers
{
    public class lttHomeController : Controller
    {
        public ActionResult lttIndex()
        {
            return View();
        }

        public ActionResult lttAbout()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult lttContact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}