using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_SECURITY
{
    public class CustomErrorFilter: HandleErrorAttribute

    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception e = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var result = new ViewResult()
            {
                ViewName = "Error"
            }; ;
            result.ViewBag.Error = e+ "Error Occur While Processing Your Request Please Check After Some Time";
            filterContext.Result = result;
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)] // it prevent the use of the Multiple time using
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        private readonly string[] userAssignedRoles;

        public AuthorizeRolesAttribute(params string[] roles)
        {
            string role = Convert.ToString(ConfigurationManager.AppSettings["LocalUser"]);
            this.userAssignedRoles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            //using (DemoDBEntities db = new DemoDBEntities())
            //{
            //    UserManager UM = new UserManager();
            //    foreach (var roles in userAssignedRoles)
            //    {
            //        authorize = UM.IsUserInRole(httpContext.User.Identity.Name, roles);
            //        if (authorize)
            //            return authorize;
            //    }
            //}
            string s = userAssignedRoles.ToString();

            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Login/UnAuthorized");
        }
    }

}
