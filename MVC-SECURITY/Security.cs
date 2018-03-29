using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//https://www.codeproject.com/Articles/577776/Filters-and-Attributes-in-ASPNET-MVC
namespace MVC_SECURITY
{
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
