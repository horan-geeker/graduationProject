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
    public class UserController : Controller
    {
        private UserDbContext UserContext = new UserDbContext();        // GET: User

        // GET: User
        public ActionResult Index()
        {
            return View(UserContext.all());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Store(FormCollection request)
        {

            User user = new User();
            user.username = request["username"].ToString();
            user.password = Utils.Util.getMd5(request["password"].ToString());
            user.gender = request["gender"].ToString();
            int result = UserContext.create(user);

            if (result > 0)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");

        }

        public ActionResult Show(int id)
        {
            ViewBag.user = UserContext.find(id);
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.user = UserContext.find(id);
            return View();
        }

        public ActionResult Update(int id, FormCollection request)
        {
            User user = UserContext.find(id);
            user.username = request["username"].ToString();
            if (user.password != request["password"].ToString())
            {
                user.password = Utils.Util.getMd5(request["password"].ToString());
            }
            user.gender = request["gender"].ToString();
            bool result = UserContext.update(id, user);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit",new { id=id });
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            bool result = UserContext.delete(id);
            //MeetUserContext.delete(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}