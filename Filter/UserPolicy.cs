using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4.Filter
{
    public class UserPolicy : FilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {

                throw new ArgumentNullException("filterContext");
                //filterContext.HttpContext.Response.Write("<script>alert('" + "您还没有登录，请先登录!" + "')</script>");
                //filterContext.Result = new HttpUnauthorizedResult();
            }
            Object user = filterContext.HttpContext.Session["User"];
            if (user == null)
            {
                //filterContext.Result = new RedirectResult("~/Auth/Login");
                filterContext.HttpContext.Response.Write("<script>alert('" + "您还没有登录，请先登录!" + "');window.history.back()</script>");
                return;
            }
            /**
            else if (user.ToString() != "user")
            {
                //filterContext.Result = new RedirectResult("~/Auth/Login");
                filterContext.HttpContext.Response.Write("<script>alert('" + "您还没有登录，请先登录!" + "');window.history.back()</script>");
                return;
            }
    **/

        }
    }
}