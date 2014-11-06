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
            List<Question> allQuestions; 
            if(category == null)
            {
                allQuestions = db.Questions.Where(c=>c.Answer != null).Select(p => new Question()
                {
                    id = p.Id,
                    answer = p.Answer,
                    asked = p.Asked,
                    categoryid = p.CategoryId,
                    categoryname = p.Category.Name,
                    email = p.Email,
                    question = p.Question,
                    upvotes = p.UpVotes
                }).ToList();

                return allQuestions; 
            }

            allQuestions = db.Questions.Where(q => q.CategoryId == category && q.Answer != null).Select(p => new Question()
            {
                id = p.Id,
                answer = p.Answer,
                asked = p.Asked,
                categoryid= p.CategoryId,
                categoryname = p.Category.Name,
                email = p.Email,
                question = p.Question,
                upvotes = p.UpVotes
            }).ToList();

            return allQuestions; 
        }

        public List<Question> getAllUnanswered(int? category)
        {
            List<Question> allQuestions;
            if (category == null)
            {
                allQuestions = db.Questions.Where(c => c.Answer == null).Select(p => new Question()
                {
                    id = p.Id,
                    answer = p.Answer,
                    asked = p.Asked,
                    categoryid = p.CategoryId,
                    categoryname = p.Category.Name,
                    email = p.Email,
                    question = p.Question,
                    upvotes = p.UpVotes
                }).ToList();

                return allQuestions;
            }

            allQuestions = db.Questions.Where(q => q.CategoryId == category && q.Answer == null).Select(p => new Question()
            {
                id = p.Id,
                answer = p.Answer,
                asked = p.Asked,
                categoryid = p.CategoryId,
                categoryname = p.Category.Name,
                email = p.Email,
                question = p.Question,
                upvotes = p.UpVotes
            }).ToList();

            return allQuestions;
        }
        public Question getQuestion(int id)
        {
            Questions que = db.Questions.Include("Category").FirstOrDefault(q => q.Id == id);
            return new Question()
            {
                id = que.Id,
                answer = que.Answer,
                asked = que.Asked,
                categoryname = que.Category.Name,
                categoryid = que.CategoryId,
                email = que.Email,
                question = que.Question,
                upvotes = que.UpVotes
            };
        }

        public bool saveNewQuestion(Question inQuestion)
        {
            Questions newQuestion = new Questions()
            {
                Asked = inQuestion.asked,
                Email = inQuestion.email,
                Question = inQuestion.question,
                CategoryId = inQuestion.categoryid
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
        public bool updateQuestion(int id, Question inQuestion)
        {
            Questions updatedQuestion = db.Questions.FirstOrDefault(k => k.Id == id);
            if (updatedQuestion == null)
                return false;
            
            //updatedQuestion.Answer = inQuestion.answer;
            updatedQuestion.Asked = inQuestion.asked;
            updatedQuestion.CategoryId = inQuestion.categoryid;
            updatedQuestion.Email = inQuestion.email;
            updatedQuestion.Question = inQuestion.question;
            updatedQuestion.UpVotes = inQuestion.upvotes;
             
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return false;
            }
            return true; 

        }
    }
}