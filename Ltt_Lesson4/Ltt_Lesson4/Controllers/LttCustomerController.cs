using Ltt_Lesson4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ltt_Lesson4.Controllers
{
    public class LttCustomerController : Controller
    {
        // GET: LttCustomer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomerDetails()
        {
            LttCustomer cus = new LttCustomer()
            {
                CustomerId = 1,
                LastName = "Tinh",
                FirstName = "Le Tuan",
                Address = "HaNoi",
                YearOfBirth = 2004
            };
            ViewBag.customer = cus;
            return View();
        }

        public ActionResult ListCustomer()
        {
            List<LttCustomer> list = new List<LttCustomer>()
            {
                new LttCustomer()
                {
                    CustomerId = 1,
                    FirstName = "Le",
                    LastName = "Tinh",
                    Address = "Hanoi",
                    YearOfBirth = 2004
                },

                new LttCustomer()
                {
                    CustomerId = 2,
                    FirstName = "Nguyen Phuong",
                    LastName = "Dung",
                    Address = "Hanoi",
                    YearOfBirth = 2004
                },

                new LttCustomer()
                {
                    CustomerId = 3,
                    FirstName = "Nguyen Thi Tra",
                    LastName = "My",
                    Address = "Hanoi",
                    YearOfBirth = 2004
                },

                new LttCustomer()
                {
                    CustomerId = 4,
                    FirstName = "Nguyen Huu",
                    LastName = "Puc",
                    Address = "Hanoi",
                    YearOfBirth = 2004
                }
            };
            //ViewBag.list = list;
            return View(list);
        }

        public ActionResult CreateCustomer()
        {
            return View();
        }

        //Post: CreateCustomer
        [HttpPost]

        public ActionResult CreateCustomer(LttCustomer cus)
        {
            string infor = "ID:" + cus.CustomerId;
            infor += "<br>Name:" + cus.FirstName + " " + cus.LastName;
            infor += "<br>Address:" + cus.Address;
            infor += "<br>Year:" + cus.YearOfBirth;
            return Content(infor);
        }
    }   
}