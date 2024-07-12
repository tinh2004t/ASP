using LeTuanTinh_2210900130.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeTuanTinh_2210900130.Controllers
{
    public class LttHomeController : Controller
    {
        LeTuanTinh_2210900130Entities1 db = new LeTuanTinh_2210900130Entities1();

        public ActionResult LttIndex()
        {
            return View();
        }

        // HttP get /LttHome/LttSign/
        public ActionResult LttSignin()
        {
            return View();
        }
        // HttP post /LttHome/LttSignin/
        [HttpPost]
        public ActionResult LttSignin(User user)
        {
            db.User.Add(user);
            db.SaveChanges();
            return RedirectToAction("LttLogin");
        }
        // HttP get /LttHome/LttLogin/
        public ActionResult LttLogin()
        {

            return View();
        }
        // HttP post /LttHome/LttLogin/
        [HttpPost]
        public ActionResult LttLogin(User user)
        {
            var UserNameForm = user.ltt_UserName;
            var PasswordForm = user.ltt_Password;
            var userCheck = db.User.SingleOrDefault(x => x.ltt_UserName.Equals(UserNameForm) && x.ltt_Password.Equals(PasswordForm));
            if ((userCheck != null))
            {
                Session["User"]= userCheck;
                return RedirectToAction("LttIndex", "LttHome");
            }
            else
            {
                ViewBag.LoginFail = "Dang nhap that bai. Vui long kiem tra lai tai khoan, mat khau!";
                return View("LttLogin");
            }

            return View();
        }

        // HttP post /LttHome/LttLogout/
        
        public ActionResult LttLogout()
        {
            Session["User"] = null;
            return RedirectToAction("LttIndex", "LttHome");
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