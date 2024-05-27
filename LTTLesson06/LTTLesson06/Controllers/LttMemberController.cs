using LTTLesson06.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LTTLesson06.Controllers
{
    public class LttMemberController : Controller
    {
        // GET: /LttMember/LttCreateOne

        public ActionResult LttCreateOne()
        {

            return View();
        }

        //POST: /LttMember/LttCreateOne
        [HttpPost]
        public ActionResult LttCreateOne(LttMember m)
        {

            return View("Detail", m);
        }

        //GET:/Member/CreateTwo
        public ActionResult LttCreateTwo()
        {
            return View();
        }

        //POST: /Member/CreateTwo
        [HttpPost]
        public ActionResult LttCreateTwo(LttMember m)
        {
            //kiểm tra trống các trường và thông báo lỗi tới
            if (m.Id == null)
            {

                ViewBag.error = "Hãy nhập mã số";
                return View();
            }
            if (m.Username == null)
            {
                ViewBag.error = "Hay nhap ten dang nhap";
                return View();
            }
            if (m.FullName == null)
            {
                ViewBag.error = "hay nhap ho va ten";
                return View();
            }

            if (m.Age == null)
            {
                ViewBag.error = "hay nhap tuoi";
                return View();
            }
            if (m.Email == null)
            {
                ViewBag.error = "hay nhap email";
                return View();
            }

            //mẫu kiểm tra Email
            string regexPattern = @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}";
            if
            (!System.Text.RegularExpressions.Regex.IsMatch(m.Email, regexPattern))
            {
                ViewBag.error= "Hãy nhập đúng định dạng";
                return View();
            }
            
            //nếu không xảy ra lỗi thì chuyển dữ liệu tới ViewDetails
return View("Details", m);
        }
        //GET: /LttMember/CreateThree
    public ActionResult LttCreateThree()
        {
            return View();
        }
        //POST: /LttMember/CreateThree
        [HttpPost]
        public ActionResult CreateThree(LttMember m)
        {
            //nếu trạng thái dữ liệu của Model hợp lệ thì chuyển dữ liệu tới  View Details 
            if (ModelState.IsValid)
                return View("Details", m);
            else
                return View(); //quay lại view Three để báo lỗi 
        }
        //GET: /LttMember/Details 
        public ActionResult Details()
        {
            return View();
        }

    }
}        

