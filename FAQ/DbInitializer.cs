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
                new Questions {Question="Hvor mye koster frakt",Asked=DateTime.Now, CategoryId=1,Email="erik@forsen.no"},
                new Questions {Question="Hva blir det til middag?", Asked=DateTime.Now, CategoryId=4,Email="lisa@forsen.no"},
                new Questions {Question="Når kommer lisa hjem?", Asked=DateTime.Now, CategoryId=2,Email="tull@ball.no"},
                new Questions {Question="Hva er aldersgrensen for å handle hos Snublevann.no?", Asked=DateTime.Now,CategoryId=3,Email="per@hansen.no"},
                new Questions {Question="Er det lovlig å selge alkohol på nett?", Asked=DateTime.Now, CategoryId=3, Email="tull@ball.no"}
            };
            //questions.ForEach(c => context.Questions.Add(c));
            //context.SaveChanges();
            var answers = new List<Answers>
            {
                new Answers {Answer="Vet ikke, skal i butikken",Answered = DateTime.Now,UserId=1,Question=questions[1]},
                new Answers {Answer="Vi har ingen aldersgrense",Answered = DateTime.Now,UserId=1,Question=questions[3]},
                new Answers {Answer="Nei, men vi gjør det likevel",Answered = DateTime.Now,UserId=1,Question=questions[4]}
            };

            //answers.ForEach(a => context.Answers.Add(a));
            foreach(var answer in answers){
                context.Answers.Add(answer);
                answer.Question.Answer = answer; 
            }
            questions.ForEach(c => context.Questions.Add(c));

            context.SaveChanges(); 
 
        }
    }
}