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
                new Categories {Name = "Betaling",Glyph="glyphicon-shopping-cart"},
                new Categories {Name = "Transport",Glyph="glyphicon-road"},
                new Categories {Name = "Juridisk",Glyph="glyphicon-book"},
                new Categories {Name = "Sortiment",Glyph="glyphicon-list"},
                new Categories {Name = "Andre",Glyph="glyphicon-info-sign"}
            };
            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();


            var questions = new List<Questions>
            {
                new Questions {Question="Hvor lang tid tar det før varene blir levert?",Asked=DateTime.Now, CategoryId=2,Email="erik@forsen.no"},
                new Questions {Question="Hva skjer om jeg ikke er hjemme når bestillingen kommer?", Asked=DateTime.Now, CategoryId=2,Email="lisa@forsen.no"},
                new Questions {Question="Kan jeg betale med BitCoins?", Asked=DateTime.Now, CategoryId=1,Email="tull@ball.no"},
                new Questions {Question="Er det mulig å betale med forskuddsfaktura?", Asked=DateTime.Now, CategoryId=1, Email="ball@tull.no"},
                new Questions {Question="Hva er aldersgrensen for å handle hos Snublevann.no?", Asked=DateTime.Now,CategoryId=3,Email="per@hansen.no"},
                new Questions {Question="Er det lovlig å selge alkohol på nett?", Asked=DateTime.Now, CategoryId=3, Email="tull@ball.no"},
                new Questions {Question="Jeg finner ikke vare X i nettbutikken, er det mulig å bestille opp?",Asked=DateTime.Now, CategoryId=4,Email="lyst@på.mere.no"},
                new Questions {Question="Jeg er rå på programmering, ansetter dere nye utviklere?",Asked=DateTime.Now, CategoryId=5,Email="ing@cognito.no"}
            };
            //questions.ForEach(c => context.Questions.Add(c));
            //context.SaveChanges();
            var answers = new List<Answers>
            {
                new Answers {Answer="Leveranse av varer avhenger av hvor du bor. Varene pakkes og sendes stort sett neste arbeidsdag, og man kan forvente å få dem levert på døren 1-2 dager etter det igjen.",Answered = DateTime.Now,UserId=1,Question=questions[0]},
                new Answers {Answer="Dersom ingen personer over 18 år er hjemme for å ta i mot varene når de ankommer, så blir de sendt til nærmeste postkontor der de kan hentes av personer over 18 år.",Answered = DateTime.Now,UserId=1,Question=questions[1]},
                new Answers {Answer="Vi tar i mot BitCoin som betalingsmiddel.",Answered=DateTime.Now,UserId=1,Question=questions[2]},
                new Answers {Answer="Det går helt fint å betale med forskuddsfaktura. Merk at det da tar noe lenger tid før varene blir sendt og levert.",Answered=DateTime.Now,UserId=2,Question=questions[3]},
                new Answers {Answer="Det er ingen aldersgrense for å handle hos oss, men leveringen kan kun mottas av personer over 18 år (20 år dersom leveransen inneholder varer med høyere enn 4,7% alkoholinnhold",Answered=DateTime.Now,UserId=4,Question=questions[4]},
                new Answers {Answer="Nei, det er det ikke.",Answered=DateTime.Now,UserId=2,Question=questions[5]},
                new Answers {Answer="What you see is what you get! Neida, vi er ikke tilhenger av WYSIWYG, så vi kan selvfølgelig bestille opp varer som ikke er i vårt sortiment.",Answered=DateTime.Now,UserId=1,Question=questions[6]},
                new Answers {Answer="Kom med minimum tre forslag til forbedringer av denne FAQ'en, så vil du bli vurdert på bakgrunn av det.",Answered=DateTime.Now,UserId=3,Question=questions[7]}
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