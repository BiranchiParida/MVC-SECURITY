using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_SECURITY.Controllers
{
    public class HomeController : BaseController
    {
       // https://www.codeproject.com/Articles/426766/Custom-Filters-in-MVC-Authorization-Action-Result
        //https://www.c-sharpcorner.com/uploadfile/cda5ba/security-feature-in-mvc/
        public class loginViewModel
        {
             public string CustomerId { get; set; }
            public string CustomerName { get; set; }
            public string Address { get; set; }
        }
        public class LoginRequest
        {
            public string CustomerId { get; set; }
            public string CustomerName { get; set; }
            public string Address { get; set; }
        }
        [HttpPost, ValidateInput(false)]
         public ActionResult SaveData(loginViewModel avc)
        {
            if (ModelState.IsValid)
            {
                int x = 0;
                int b = 3 / x;
                LoginRequest loginrequest = new LoginRequest();
                loginrequest.CustomerName = Sanitizer.GetSafeHtmlFragment(avc.CustomerName);
                loginrequest.Address = Sanitizer.GetSafeHtmlFragment(avc.Address);

               // LoginResponse loginResponse = await service.UserAuthenticate(loginrequest);
                //bool isValid = !(loginResponse.userStatus && loginResponse.failedLoginAttempt >= 0);
                //if (isValid)
                //{
                    FormsAuthentication.SetAuthCookie("biranchi", false);
                   // CreateSession(loginResponse);
                    return RedirectToAction("Redirect", "Main");
                //}
                //else
                //{
                //  
                // if (loginResponse.isLocked)
                //    {
                //        ModelState.AddModelError("modelError", "Your account has been locked. Please check your e-mail to reset the password."); return View();
                //    }
                //    else if (loginResponse.failedLoginAttempt > 0)
                //    {
                //        ModelState.AddModelError("modelError", "Your remaining  login attempts is  " + Convert.ToString(5 - loginResponse.failedLoginAttempt));
                //        return View();
                //    }
                //    else
                //    {
                //        ModelState.AddModelError("modelError", "Either Username or password is wrong"); return View();
                //    }
                //}

            }
            else
            {
                ModelState.AddModelError("modelError", "Either UserName Password is Wrong");
            }
            //save logic
            return View();
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeRoles("Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
//https://www.codeproject.com/Articles/654846/Security-In-ASP-NET-MVC

            return View();
        }
    }
}