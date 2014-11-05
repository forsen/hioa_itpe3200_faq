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
                new Categories {Name = "Juridisk"},
                new Categories {Name = "Andre"}
            };
            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();


            var questions = new List<Questions>
            {
                new Questions {Question="Hvor mye koster frakt",Asked=DateTime.Now, CategoriesId=1,Email="erik@forsen.no", Answers = null},
                new Questions {Question="Hva blir det til middag?", Asked=DateTime.Now, CategoriesId=4,Email="lisa@forsen.no", Answers = null},
                new Questions {Question="Når kommer lisa hjem?", Asked=DateTime.Now, CategoriesId=2,Email="tull@ball.no", Answers = null},
                new Questions {Question="Hva er aldersgrensen for å handle hos Snublevann.no?", Asked=DateTime.Now,CategoriesId=3,Email="per@hansen.no", Answers = null},
                new Questions {Question="Er det lovlig å selge alkohol på nett?", Asked=DateTime.Now, CategoriesId=3, Email="tull@ball.no", Answers = null}
            };
            //questions.ForEach(c => context.Questions.Add(c));
            //context.SaveChanges();
            var answers = new List<Answers>
            {
                new Answers {Answer="Vet ikke, skal i butikken",Answered = DateTime.Now,UserId=1,Questions=questions[1]},
                new Answers {Answer="Vi har ingen aldersgrense",Answered = DateTime.Now,UserId=1,Questions=questions[3]},
                new Answers {Answer="Nei, men vi gjør det likevel",Answered = DateTime.Now,UserId=1,Questions=questions[4]}
            };

            //answers.ForEach(a => context.Answers.Add(a));
            foreach(var answer in answers){
                context.Answers.Add(answer);
                answer.Questions.Answers = answer; 
            }
            questions.ForEach(c => context.Questions.Add(c));

            context.SaveChanges(); 
 
        }
    }
}