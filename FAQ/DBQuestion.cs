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
                allQuestions = db.Questions.Where(c=>c.Answer != null && !c.DontShowInFaq).Select(p => new Question()
                {
                    id = p.Id,
                    answer = new Answer()
                    {
                        answer = p.Answer.Answer,
                        id = p.Id,
                        answered = p.Answer.Answered.ToString(),
                        userid = p.Answer.UserId
                    },
                    asked = p.Asked.ToString(),
                    categoryid = p.CategoryId,
                    categoryname = p.Category.Name,
                    email = p.Email,
                    question = p.Question,
                    upvotes = p.UpVotes
                }).ToList();

                return allQuestions; 
            }

            allQuestions = db.Questions.Where(q => q.CategoryId == category && q.Answer != null && !q.DontShowInFaq).Select(p => new Question()
            {
                id = p.Id,
                answer = new Answer(){
                            answer = p.Answer.Answer,
                            id = p.Id,
                            answered = p.Answer.Answered.ToString(),
                            userid = p.Answer.UserId
                        },
                asked = p.Asked.ToString(),
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
                allQuestions = db.Questions.Where(c => c.Answer == null && !c.DontShowInFaq).Select(p => new Question()
                {
                    id = p.Id,
                    answer = new Answer(),
                    asked = p.Asked.ToString(),
                    categoryid = p.CategoryId,
                    categoryname = p.Category.Name,
                    email = p.Email,
                    question = p.Question,
                    upvotes = p.UpVotes
                }).ToList();

                return allQuestions;
            }

            allQuestions = db.Questions.Where(q => q.CategoryId == category && q.Answer == null && !q.DontShowInFaq).Select(p => new Question()
            {
                id = p.Id,
                answer = new Answer(),
                asked = p.Asked.ToString(),
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
            Answer a = new Answer(); 
            if( que.Answer != null)
            {
                a.answer = que.Answer.Answer;
                a.answered = que.Answer.Answered.ToString();
                a.userid = que.Answer.UserId;
            }

            return new Question()
            {
                id = que.Id,
                answer = a,
                asked = que.Asked.ToString(),
                categoryname = que.Category.Name,
                categoryid = que.CategoryId,
                email = que.Email,
                question = que.Question,
                upvotes = que.UpVotes,
                dontshowinfaq = que.DontShowInFaq
            };
        }

        public bool saveNewQuestion(Question inQuestion)
        {
            Questions newQuestion = new Questions()
            {
                Asked = DateTime.Now,
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
        public bool addAnswer(int questionId, Answer inAnswer)
        {
            Questions question = db.Questions.FirstOrDefault(k => k.Id == questionId);
            Answers newAnswer = new Answers()
            {
                Answer = inAnswer.answer,
                Answered = DateTime.Now,
                Question = question,
                UserId = inAnswer.userid
            };
            try
            {
                db.Answers.Add(newAnswer);
                db.SaveChanges();
                return true; 
            }
            catch(Exception e)
            {
                //faen
            }
            return false; 

        }
        public bool updateQuestion(int id, Question inQuestion)
        {
            Questions updatedQuestion = db.Questions.FirstOrDefault(k => k.Id == id);
            if (updatedQuestion == null)
                return false;
            
            //updatedQuestion.Answer = inQuestion.answer;
            updatedQuestion.CategoryId = inQuestion.categoryid;
            updatedQuestion.Email = inQuestion.email;
            updatedQuestion.Question = inQuestion.question;
            updatedQuestion.UpVotes = inQuestion.upvotes;
            updatedQuestion.DontShowInFaq = inQuestion.dontshowinfaq;
             
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