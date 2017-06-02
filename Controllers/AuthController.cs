using MVC4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4.Controllers
{
    public class AUthController : Controller
    {
        private AdminDbContext AdminContext = new AdminDbContext();

        // GET: AUth
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["Admin"] = null;
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult LoginPost(FormCollection request)
        {
            string username = request["username"].ToString();
            string password = request["password"].ToString();
            password = Utils.Util.getMd5(password);
            Admin admin = AdminContext.findByName(username);
            if (password == admin.password)
            {
                Session["Admin"] = request["username"];
                return RedirectToAction("Index","Home");
            }
            else
            {
                Session["flash_error_message"] = "用户名密码错误";
                return RedirectToAction("Login","Auth");
            }
        }
    }
}