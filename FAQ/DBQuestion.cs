using FAQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAQ
{
    public class DBQuestion
    {
        DatabaseContext db = new DatabaseContext(); 
        public List<Question> getAllQuestions(int? category)
        {
            List<Question> allQuestions = db.Questions.Where(q => q.CategoriesId == category).Select(p => new Question()
            {
                id = p.Id,
                answer = p.Answers.Answer,
                asked = p.Asked,
                category = p.Categories.Name,
                email = p.Email,
                question = p.Question
            }).ToList();

            return allQuestions; 
        }

        public bool saveNewQuestion(Question q)
        {
            Questions newQuestion = new Questions()
            {
                Asked = q.asked,
                Email = q.email,
                Question = q.question,
                CategoriesId = q.categoryid
            };
            try
            {
                db.Questions.Add(newQuestion);
                db.SaveChanges(); 
            }
            catch(Exception feil)
            {
                return false; 
            }
            return true;
        }
    }
}