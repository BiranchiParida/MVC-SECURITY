namespace MVC_SECURITY.Controllers
{
    using System;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Security;

    public class BaseController : Controller
    {
        //https://www.codeproject.com/Articles/426766/Custom-Filters-in-MVC-Authorization-Action-Result
        http://www.dotnet-stuff.com/tutorials/aspnet-mvc/understanding-asp-net-mvc-filters-and-attributes
        private bool IsSessionExist()
        {
            bool status = false;
            if (!string.IsNullOrEmpty(Convert.ToString(Session["CustomerID"])))
            {
                status = true;
            }
            return status;
        }
        /// <summary>
        /// It will hit every time before any action to be hit wether session exist or not ,including both request(AJAx request and Normal Request)
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (!IsSessionExist())
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult
                    {
                        Data = "login",//Session Expired.
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    filterContext.Result = new RedirectResult(FormsAuthentication.DefaultUrl);
                }
            }

            if (HttpContext.Request.TimedOutToken.IsCancellationRequested)
            {
                filterContext.Result = new RedirectResult(FormsAuthentication.DefaultUrl);
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (HttpContext.Request.TimedOutToken.IsCancellationRequested)
            {
                filterContext.Result = new RedirectResult(FormsAuthentication.DefaultUrl);
            }
        }
        /// <summary>
        /// It will override the Json Max Lenght during data passing from Action to ClientSide Scripting
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }/// <summary>
         /// Handel Unknown Action if Controller is exist
         /// </summary>
         /// <param name="actionName"></param>
        protected override void HandleUnknownAction(string actionName)
        {

            this.View("NotFound").ExecuteResult(this.ControllerContext);
        }
        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/ms525713(v=vs.90).aspx
        /// http://stackoverflow.com/questions/4464613/do-we-need-to-response-clear-in-custom-handleerrorattribute
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult()
                {
                    Data = filterContext.Exception.Message,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();

            }
            base.OnException(filterContext);
            // filterContext.Result = new RedirectResult("~/Login/Error");
        }
    }
}