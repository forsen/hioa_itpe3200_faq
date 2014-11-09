using FAQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace FAQ.Controllers
{
    [RoutePrefix("api/Question")]
    public class QuestionController : ApiController
    {
        DBQuestion db = new DBQuestion(); 


        // GET api/Question
        public HttpResponseMessage Get([FromUri] string unanswered)
        {
            List<Question> allQuestions;
            if(unanswered.Equals("true"))
                allQuestions = db.getAllUnanswered(null);
            else
                allQuestions = db.getAllQuestions(null);

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(allQuestions);
            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        public HttpResponseMessage Get(int id)
        {
            Question question = db.getQuestion(id);

            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(question);
            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }
        [Route("cat/{categoryid}")]
        public HttpResponseMessage GetQuestionByCategory(int categoryid)
        {
            List<Question> allQuestions = db.getAllQuestions(categoryid);
            allQuestions = allQuestions.OrderByDescending(c => c.upvotes).ToList();
            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(allQuestions);
            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        // POST api/Question
        public HttpResponseMessage Post(Question inQuestion)
        {
            if(ModelState.IsValid)
            {
                if(db.saveNewQuestion(inQuestion)){
                    return new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    };
                }
                return new HttpResponseMessage()
                {
                    Content = new StringContent("Noe gikk galt"),
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
            return new HttpResponseMessage()
            {
                Content = new StringContent("Input er ikke validert, prøv på nytt"),
                StatusCode = HttpStatusCode.Forbidden
            };
        }

        public HttpResponseMessage Put(int id, Question inQuestion)
        {
            if(ModelState.IsValid)
            {
                bool ok = db.updateQuestion(id, inQuestion);
                if (ok)
                    return new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK
                    };
            }

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound
            };
        }

        [Route("ans/{id}")]
        public HttpResponseMessage Put(int id, Answer inAnswer)
        {
            if(ModelState.IsValid)
            {
                Question question = db.getQuestion(id);
                question.answer = inAnswer;
                 
                if(db.updateQuestion(id, question))
                    if(db.addAnswer(id,inAnswer))
                        return new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.OK
                        };
            }

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound
            };
        }

    }
}
