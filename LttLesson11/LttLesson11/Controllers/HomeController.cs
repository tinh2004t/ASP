using LttLesson11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LttLesson11.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult LttIndex()
        {
            // lấy thông tin từ session
            //ViewBag["lttTaiKhoan"] = "";
            if (Session["LttMember"] != null)
            {
                ViewBag.LttTaiKhoan = ((LttTaiKhoan)Session["LttMember"]).LttFullName;
            }
            return View();
        }

        public ActionResult LttAbout()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult LttContact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}