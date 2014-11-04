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
                    questions = c.Questions
                }).ToList();
            return allCategories;
        }
    }
}