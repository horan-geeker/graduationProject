using MVC4.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC4.Filter;
using System.Net;

namespace MVC4.Controllers
{
    [ActionFilter]
    public class QuestionnaireController : Controller
    {
        private MeetDbContext MeetContext = new MeetDbContext();        // GET: Meet
        private QuestionnaireDbContext QueContext = new QuestionnaireDbContext();
        private QuestionDbContext QuestionContext = new QuestionDbContext();
        private QuestionChoiceDbContext QuestionChoiceContext = new QuestionChoiceDbContext();

        // GET: Questionnaire
        public ActionResult Index()
        {
            List<Questionnaire> que = QueContext.all();
            return View(que);
        }

        public ActionResult Create()
        {
            ViewBag.meets = MeetContext.all();
            return View();
        }

        [HttpPost]
        public JsonResult Store(FormCollection request)
        {
            Questionnaire quenaire = new Questionnaire();
            quenaire.meet_id = request["meet_id"];
            quenaire.title = request["title"];
            quenaire.direction = request["direction"];
            quenaire.end_at = request["end_at"];
            quenaire.type = request["type"];
            int quenaireId = QueContext.create(quenaire);
            string queStr = request["questions"].ToString();
            JArray jarr = (JArray)JsonConvert.DeserializeObject(queStr);

            for (int i = 0; i < jarr.Count; i++)
            {
                Question question = new Question();
                question.questionnaire_id = quenaireId;
                question.question_content = jarr[i]["content"].ToString();
                question.type_id = Convert.ToInt32(jarr[i]["type"].ToString());
                // question.Question_Seq = i + 1;
                question.is_required = Convert.ToInt32(jarr[i]["is_required"].ToString());
                //string content = jarr[0]["content"].ToString();
                //string type = jarr[0]["type"].ToString();
                int questionId = QuestionContext.create(question);
                string choiceStr = jarr[i]["choices"].ToString();
                JArray choiceArr = (JArray)JsonConvert.DeserializeObject(choiceStr);
                for (int j = 0; j < choiceArr.Count; j++)
                {
                    QuestionChoice choice = new QuestionChoice();
                    choice.question_id = questionId;
                    char tag = (char)('A' + j);
                    choice.choice_tag = tag + "";
                    choice.choice_content = choiceArr[j]["choiceContent"].ToString();
                    // choice.is_writed = 0;
                    QuestionChoiceContext.create(choice);
                    //string choiceContent = choiceArr[0]["choiceContent"].ToString();
                }
            }

            return Json(new { status = 0, msg = "success", data = new { id = quenaireId } });
        }

        public ActionResult Show(int id)
        {
            Questionnaire que = QueContext.find(id);
            que.questions = QueContext.questions(id);

            foreach(Question question in que.questions)
            {
                question.questionChoices = QuestionContext.questionChoices(question.id);
            }
            ViewBag.que = que;
            return View();
        }

        public ActionResult Delete(int id)
        {
            bool result = QueContext.delete(id);
            if (result)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}