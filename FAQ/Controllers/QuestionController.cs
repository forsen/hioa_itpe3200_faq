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
    public class QuestionController : ApiController
    {
        DBQuestion db = new DBQuestion(); 


        // GET api/Question
        public HttpResponseMessage Get()
        {
            List<Question> allQuestions = db.getAllQuestions(null);

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
                inQuestion.asked = DateTime.Now;
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
    }
}
