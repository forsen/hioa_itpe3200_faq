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

        public List<Category> getAllCategories()
        {
            List<Category> allCategories = db.Categories.Select(c => new Category()
                {
                    id = c.Id,
                    name = c.Name,
                    glyph = c.Glyph,
                    questions = c.Questions.Where(p=>p.Answer != null).Select(s => new Question()
                    {
                        id = s.Id,
                        question = s.Question,
                        answer = new Answer(){
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
    }
}