using Ltt_Lesson4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ltt_Lesson4.Controllers
{
    public  class LttCustomerScaffdingController : Controller
    {
        //mockdata
        private static List<LttCustomer> list = new List<LttCustomer>()
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
            
        // GET: LttCustomerScaffding
        public ActionResult Index()
        {
            return View(list);
        }

   

        [HttpGet]
        public ActionResult LttCreate()
        {
            var model = new LttCustomer();
            return View(model);
        }

        [HttpPost]
        public ActionResult LttCreate(LttCustomer model)
        {
            // them moi doi tuong khach hang vao du lieu
            list.Add(model);

            //return View(model);
            //chuyen ve trang danh sach
            return RedirectToAction("Index");
        }
        public ActionResult LttEdit(int id)
        {
            var customer = list.FirstOrDefault(x => x.CustomerId == id);
            return View(customer);
        }
    }
}