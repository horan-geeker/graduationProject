using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var session = System.Web.HttpContext.Current.Session;
            if (session["admin"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        public ActionResult Test()
        {
            return RedirectToAction("Show","Meet",new { id=10 });
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}