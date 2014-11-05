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

        public Question getQuestion(int id)
        {
            Questions que = db.Questions.Include("Categories").Include("Answers").FirstOrDefault(q => q.Id == id);
            return new Question()
            {
                id = que.Id,
                answer = que.Answers.Answer,
                asked = que.Asked,
                category = que.Categories.Name,
                categoryid = que.CategoriesId,
                email = que.Email,
                question = que.Question
            };
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