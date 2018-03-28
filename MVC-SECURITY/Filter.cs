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
}