using FAQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAQ
{
    public class DBCategory
    {
        DatabaseContext db = new DatabaseContext(); 

        public List<Category> getAllCategories(bool unanswered)
        {
            List<Category> allCategories;
            if (unanswered)
                allCategories = db.Categories.Select(c => new Category()
                    {
                        id = c.Id,
                        name = c.Name,
                        glyph = c.Glyph,
                        questions = c.Questions.Where(p => p.Answer == null && !p.DontShowInFaq).Select(s => new Question()
                        {
                            id = s.Id,
                            question = s.Question,
                            answer = new Answer(),
                            asked = s.Asked.ToString(),
                            categoryid = c.Id,
                            categoryname = c.Name,
                            email = s.Email,
                            upvotes = s.UpVotes
                        }).ToList()
                    }).ToList(); 
            else
                allCategories = db.Categories.Select(c => new Category()
                    {
                        id = c.Id,
                        name = c.Name,
                        glyph = c.Glyph,
                        questions = c.Questions.Where(p => p.Answer != null && !p.DontShowInFaq).Select(s => new Question()
                        {
                            id = s.Id,
                            question = s.Question,
                            answer = new Answer()
                            {
                                answer = s.Answer.Answer,
                                id = s.Id,
                                answered = s.Answer.Answered.ToString(),
                                userid = s.Answer.UserId
                            },
                            asked = s.Asked.ToString(),
                            categoryid = c.Id,
                            categoryname = c.Name,
                            email = s.Email,
                            upvotes = s.UpVotes
                        }).ToList()
                    }).ToList();


            return allCategories;
        }

        public Category getCategory(int id, bool unanswered)
        {
            Category category;
            if (unanswered)
                category = db.Categories.Where(q => q.Id == id).Select(c => new Category()
                {
                    id = c.Id,
                    name = c.Name,
                    glyph = c.Glyph,
                    questions = c.Questions.Where(p => p.Answer == null && !p.DontShowInFaq).Select(s => new Question()
                    {
                        id = s.Id,
                        question = s.Question,
                        answer = new Answer(),
                        asked = s.Asked.ToString(),
                        categoryid = c.Id,
                        categoryname = c.Name,
                        email = s.Email,
                        upvotes = s.UpVotes
                    }).ToList()
                }).FirstOrDefault();
            else
                category = db.Categories.Where(q => q.Id == id).Select(c => new Category()
                {
                    id = c.Id,
                    name = c.Name,
                    glyph = c.Glyph,
                    questions = c.Questions.Where(p => p.Answer != null && !p.DontShowInFaq).Select(s => new Question()
                    {
                        id = s.Id,
                        question = s.Question,
                        answer = new Answer()
                        {
                            answer = s.Answer.Answer,
                            id = s.Id,
                            answered = s.Answer.Answered.ToString(),
                            userid = s.Answer.UserId
                        },
                        asked = s.Asked.ToString(),
                        categoryid = c.Id,
                        categoryname = c.Name,
                        email = s.Email,
                        upvotes = s.UpVotes
                    }).ToList()
                }).FirstOrDefault();


            return category;
        }
    }
}