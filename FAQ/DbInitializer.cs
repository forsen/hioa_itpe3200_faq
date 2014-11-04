using FAQ.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FAQ
{
    public class DbInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            var categories = new List<Categories>
            {
                new Categories {Name = "Faktura"},
                new Categories {Name = "Transport"},
                new Categories {Name = "Andre"}
            };
            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();


            var questions = new List<Questions>
            {
                new Questions {Question="Hvor mye koster frakt",Asked=DateTime.Now, CategoriesId=1,Email="erik@forsen.no",Answers=null},
                new Questions {Question="Hva blir det til middag?", Asked=DateTime.Now, CategoriesId=3,Email="lisa@forsen.no",Answers=null},
                new Questions {Question="Når kommer lisa hjem?", Asked=DateTime.Now, CategoriesId=2,Email="tull@ball.no"}
            };
            questions.ForEach(c => context.Questions.Add(c));
            context.SaveChanges();
            var answers = new List<Answers>
            {
                new Answers {Answer="Vet ikke, skal i butikken",Answered = DateTime.Now,UserId=1,Questions=questions[1]}

            };
            answers.ForEach(a => context.Answers.Add(a));

            context.SaveChanges(); 
 
        }
    }
}