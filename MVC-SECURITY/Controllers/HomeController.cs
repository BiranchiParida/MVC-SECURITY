using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_SECURITY.Controllers
{
    public class HomeController : Controller
    {
        //https://www.c-sharpcorner.com/uploadfile/cda5ba/security-feature-in-mvc/
        public class AVC
        {
             public string CustomerId { get; set; }
            public string CustomerName { get; set; }
            public string Address { get; set; }
        }
         [HttpPost]
         public ActionResult SaveData(AVC avc)
        {
            //save logic
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}