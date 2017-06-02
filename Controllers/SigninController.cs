using MVC4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC4.Filter;


namespace MVC4.Controllers
{
    public class SigninController : Controller
    {
        private MeetDbContext MeetContext = new MeetDbContext();
        private UserDbContext UserContext = new UserDbContext();
        private MeetUserDbContext MeetUserContext = new MeetUserDbContext();
        private QuestionnaireDbContext QueDbContext = new QuestionnaireDbContext();

        // GET: Signin
        public ActionResult ShowForm(int id)
        {
            ViewBag.meet = MeetContext.find(id);
            return View();
        }

        [HttpPost]
        public ActionResult Signin(int id,FormCollection request)
        {
            string username = request["username"].ToString();
            string password = request["password"].ToString();
            password = Utils.Util.getMd5(password);
            User user = UserContext.findByName(username);
            if (password == user.password)
            {
                MeetUserContext.signin(new MeetUser { user_id = user.id, meet_id = id});
                Session["User"] = request["username"];
                return RedirectToAction("MeetDetail", new { id = id });
            }
            else
            {
                Session["flash_error_message"] = "用户名密码错误";
                return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            }
            
        }

        public ActionResult Signout(int id)
        {
            Session["User"] = null;
            return RedirectToAction("ShowForm", new { id = id });
        }

        [UserPolicy]
        public ActionResult MeetDetail(int id)
        {
            ViewBag.meet = MeetContext.find(id);
            ViewBag.questionnaire = QueDbContext.findByMeet(id);
            return View();
        }
    }
}