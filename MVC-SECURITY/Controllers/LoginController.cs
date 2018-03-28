using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_SECURITY.Controllers
{
    public class LoginController : Controller
    {
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
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Login(loginViewModel avc, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {

                LoginRequest loginrequest = new LoginRequest();
                loginrequest.CustomerName = Sanitizer.GetSafeHtmlFragment(avc.CustomerName);
                loginrequest.Address = Sanitizer.GetSafeHtmlFragment(avc.Address);

                // LoginResponse loginResponse = await service.UserAuthenticate(loginrequest);
                //bool isValid = !(loginResponse.userStatus && loginResponse.failedLoginAttempt >= 0);
                //if (isValid)
                //{
                FormsAuthentication.SetAuthCookie(loginrequest.CustomerName, false);
                // CreateSession(loginResponse);
                


            }
            else
            {
                ModelState.AddModelError("modelError", "Either UserName Password is Wrong");
            }
            //save logic
            if (ReturnUrl != null)
            {
                RedirectToLocal(ReturnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}