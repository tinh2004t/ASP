using LttBaiDanhGiaGiuaKy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LttBaiDanhGiaGiuaKy.Controllers
{
    public class LttProductController : Controller
    {
        private static List<LttProduct> LttProducts = new List<LttProduct>()
        {
            new LttProduct{LttId=204, LttName="Banh Bao", LttImage="../Images/Anhbanhbao.png", LttQuantity=24,LttPrice=10000,LttIsActive=true},
            new LttProduct{LttId=202, LttName="Banh Ca", LttImage="../Images/Anhbanhca.png", LttQuantity=14,LttPrice=12000,LttIsActive=true}
        };

        // GET: LttProduct
        public ActionResult Index()
        {
            return View(LttProducts);
        }

        //Get: Create Products
        public ActionResult LttCreate()
        {
            var LttProduct = new LttProduct();
            return View(LttProduct);
        }

        //Post: Create Products
        [HttpPost]
        public ActionResult LttCreate(LttProduct lttProduct)
        {
            if(!ModelState.IsValid)
            {
                return View(lttProduct);
            }

            LttProducts.Add(lttProduct);
            return RedirectToAction("Index");
        }
        //Get: Edit Products
        [HttpGet]
        public ActionResult LttEdit(int id)
        {
            var LttProduct = LttProducts.FirstOrDefault(p => p.LttId == id);

            if (LttProduct == null)
            {
                return HttpNotFound();
            }

            return View(LttProduct);
        }

        [HttpPost]
        public ActionResult LttEdit(LttProduct lttProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(lttProduct);
            }

            var existingProduct = LttProducts.FirstOrDefault(p => p.LttId == lttProduct.LttId);

            if (existingProduct == null)
            {
                return HttpNotFound();
            }

            existingProduct.LttName = lttProduct.LttName;
            existingProduct.LttImage = lttProduct.LttImage;
            existingProduct.LttQuantity = lttProduct.LttQuantity;
            existingProduct.LttPrice = lttProduct.LttPrice;
            existingProduct.LttIsActive = lttProduct.LttIsActive;


            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult LttDelete(int id)
        {
            var LttProduct = LttProducts.FirstOrDefault(p => p.LttId == id);

            if (LttProduct == null)
            {
                return HttpNotFound();
            }

            // Confirmation page (optional)
            // return View("DeleteConfirmation", LttProduct);

            // Direct delete
            LttProducts.Remove(LttProduct);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult LttDetail(int id)
        {
            var LttProduct = LttProducts.FirstOrDefault(p => p.LttId == id);

            if (LttProduct == null)
            {
                return HttpNotFound();
            }

            return View(LttProduct);
        }


    }
}