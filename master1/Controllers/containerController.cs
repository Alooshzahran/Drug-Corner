using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using master1.Models;
using Microsoft.AspNet.Identity;

namespace master1.Controllers
{
    public class containerController : Controller
    {
        master1Entities db = new master1Entities();
        // GET: container
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Home()
        {
            string idd = User.Identity.GetUserId();

            HttpCookie id = new HttpCookie("id");

            id.Values.Add("userid", idd);
            Response.Cookies.Add(id);
            return View();
        }
        public ActionResult singlecard()
        {
            return View();
        }
        public ActionResult Cart()
        {
            string Id = Request.QueryString["id"].ToString();
            var carts = from ca in db.carts
                        where ca.userid == Id
                        select ca.cart_id;

            //var productincart = from pr in db.ProductsinCarts
            //                    where pr.cartid<int> == carts
            //                    select pr;
            //ViewBag.dbcart = productincart.ToList();

            return View();
        }
        public ActionResult shop(string search)
        {
            var products = from pr in db.products select pr;
            if (search != null)
            {
            
                var product = db.products.Where(r => r.product_name.Contains(search)).ToList();
                ViewBag.dbproduct = product;
               
                return View();
            }
            else
            {
                ViewBag.Message = "Your application description page.";
                ViewBag.dbproduct = db.products.ToList();
                return View();
            }
        }
        public ActionResult Rosheta()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Profile()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}