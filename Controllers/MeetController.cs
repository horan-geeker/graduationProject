using MVC4.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC4.Filter;

namespace MVC4.Controllers
{
    [ActionFilter]
    public class MeetController : Controller
    {
        private MeetDbContext MeetContext = new MeetDbContext();        // GET: Meet
        private UserDbContext UserContext = new UserDbContext();        // GET: User
        private MeetUserDbContext MeetUserContext = new MeetUserDbContext();        // GET: Pivot

        public ActionResult Index()
        {
            List<Meet> meets = MeetContext.all();
            return View(meets);
        }

        public ActionResult Create()
        {
            ViewBag.users = UserContext.all();
            return View();
        }

        [HttpPost]
        public ActionResult Store(FormCollection request)
        {
          
            Meet meet = new Meet();
            meet.title = request["title"].ToString();
            meet.description = request["description"].ToString();
            meet.begin_at = request["begin_at"].ToString();
            int meet_result = MeetContext.create(meet);
            
            if (meet_result > 0)
            {
                
                foreach (var user_id in request.GetValues("users"))
                {
                    MeetUser item = new MeetUser();
                    item.meet_id = meet_result;
                    item.user_id = Int32.Parse(user_id);
                    MeetUserContext.create(item);
                }

                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
   
        }

        public ActionResult Show(int id)
        {
            ViewBag.meet = MeetContext.find(id);
            ViewBag.users = MeetContext.users(id);
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.meet = MeetContext.find(id);
            ViewBag.users = UserContext.all();
            ViewBag.meetUsers = MeetContext.users(id);
            return View();
        }

        public ActionResult Update(int id, FormCollection request)
        {
            Meet meet = new Meet();
            meet.title = request["title"].ToString();
            meet.description = request["description"].ToString();
            meet.begin_at = request["begin_at"].ToString();
            bool result = MeetContext.update(id, meet);
            MeetUserContext.delete(id);
            foreach (var user_id in request.GetValues("users"))
            {
                MeetUser item = new MeetUser();
                item.meet_id = id;
                item.user_id = Int32.Parse(user_id);
                MeetUserContext.create(item);
            }
            return RedirectToAction("Show", new { id = id });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool result = MeetContext.delete(id);
            MeetUserContext.delete(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}