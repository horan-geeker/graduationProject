using MVC4.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MVC4.Controllers
{
    public class AnswerController : Controller
    {
        private UserDbContext UserContext = new UserDbContext();
        private QuestionnaireDbContext QueContext = new QuestionnaireDbContext();
        private QuestionDbContext QuestionContext = new QuestionDbContext();
        private QuestionChoiceDbContext QuestionChoiceContext = new QuestionChoiceDbContext();
        private AnswerDbContext AnswerContext = new AnswerDbContext();

        // GET: Answer
        public ActionResult Store(FormCollection request)
        {
            int user_id = 0;
            int questionnaire_id = Int32.Parse(request["questionnaire_id"]);
            string username = Session["User"].ToString();
            if (request["questionnaire_type"] == "1")
            {
                User user = UserContext.findByName(username);
                user_id = user.id;
            }
            
            foreach (string question_id in request)
            {
                if (question_id == "questionnaire_type" || question_id == "questionnaire_id")
                {
                    continue;
                }
                Answer answer = new Answer {
                    questionnaire_id = questionnaire_id,
                    user_id = user_id,
                    question_id = Int32.Parse(question_id),
                    content = request[question_id]
                };
                AnswerContext.create(answer);
                
            }
            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Statistics(int id)
        {
            Questionnaire que = QueContext.find(id);
            List<Question> questions = QueContext.questions(id);
            que.questions = questions;
            foreach(Question question in questions)
            {
                List<Answer> answers = AnswerContext.findByQuestion(question.id);
                question.answers = answers;
                foreach(Answer answer in answers)
                {
                    answer.user = UserContext.find(answer.user_id);
                    
                }
            }
            
            ViewBag.que = que;
            return View();
        }

        public JsonResult ChartData(FormCollection request)
        {
            Question question = QuestionContext.find(Int32.Parse(request["id"]));
            List<QuestionChoice> questionChoices = QuestionChoiceContext.findByQuestion(Int32.Parse(request["id"]));
            List<Answer> answers = AnswerContext.findByQuestion(Int32.Parse(request["id"]));
            foreach(QuestionChoice questionChoice in questionChoices)
            {
                foreach (Answer answer in answers)
                {
                    if(answer.content == questionChoice.choice_tag)
                    {
                        questionChoice.count++;
                    }
                }
            }  
            return Json(new {
                title =question.question_content,
                choice = questionChoices
            });
        }
    }
}