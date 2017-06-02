using MVC4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC4.Filter;

namespace MVC4.Controllers
{
    [ActionFilter]
    public class AdminController : Controller
    {

        private AdminDbContext AdminContext = new AdminDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View(AdminContext.all());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Store(FormCollection request)
        {
            Admin admin = new Admin();
            admin.username = request["username"].ToString();
            admin.password = Utils.Util.getMd5(request["password"].ToString());
            int result = AdminContext.create(admin);

            if (result > 0)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");

        }

        public ActionResult Edit(int id)
        {
            ViewBag.user = AdminContext.find(id);
            return View();
        }

        public ActionResult Update(int id, FormCollection request)
        {
            Admin user = AdminContext.find(id);
            user.username = request["username"].ToString();
            if (user.password != request["password"].ToString())
            {
                user.password = Utils.Util.getMd5(request["password"].ToString());
            }
            bool result = AdminContext.update(id, user);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { id = id });
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            bool result = AdminContext.delete(id);
            //MeetUserContext.delete(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}