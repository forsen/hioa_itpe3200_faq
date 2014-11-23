using FAQ.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FAQ
{

    public class DbInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        private Random prodGen = new Random();
        private Random prodDate = new Random();
        private Random prodQuant = new Random();
        DateTime start = new DateTime(2014, 8, 1);
        int dateRange;
        protected override void Seed(DatabaseContext context)
        {
            dateRange = (DateTime.Today - start).Days;
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
                new Questions {Question="Hvor lang tid tar det før varene blir levert?",Asked=RandomDay(start), CategoryId=2,Email="erik@forsen.no",UpVotes=4},
                new Questions {Question="Hva skjer om jeg ikke er hjemme når bestillingen kommer?", Asked=RandomDay(start), CategoryId=2,Email="lisa@forsen.no",UpVotes=9},
                new Questions {Question="Kan jeg betale med BitCoins?", Asked=RandomDay(start), CategoryId=1,Email="tull@ball.no",UpVotes=5},
                new Questions {Question="Er det mulig å betale med forskuddsfaktura?", Asked=RandomDay(start), CategoryId=1, Email="ball@tull.no",UpVotes=3},
                new Questions {Question="Hva er aldersgrensen for å handle hos Snublevann.no?", Asked=RandomDay(start),CategoryId=3,Email="per@hansen.no",UpVotes=8},
                new Questions {Question="Er det lovlig å selge alkohol på nett?", Asked=RandomDay(start), CategoryId=3, Email="tull@ball.no",UpVotes=9},
                new Questions {Question="Jeg finner ikke vare X i nettbutikken, er det mulig å bestille opp?",Asked=RandomDay(start), CategoryId=4,Email="lyst@på.mere.no",UpVotes=2},
                new Questions {Question="Jeg er rå på programmering, ansetter dere nye utviklere?",Asked=RandomDay(start), CategoryId=5,Email="ing@cognito.no",UpVotes=1},
                new Questions {Question="Hvilken plattform er nettbutikken utviklet på?", Asked=RandomDay(start), CategoryId=5, Email="epost@navn.no"},
                new Questions {Question="Vinen jeg bestilte har korksmak, hvordan kan jeg få returnert / erstattet denne?",Asked=RandomDay(start),CategoryId=5,Email="vin@og.mat"},
                new Questions {Question="Flasken var knust da de ble levert, hva nå?", Asked=RandomDay(start),CategoryId=2,Email="skuffet@kunde.no"},
                new Questions {Question="Hvor mye koster det å få en bestilling tilsendt?",Asked=RandomDay(start),CategoryId=2,Email="per.hansen@gmail.com",UpVotes=8},
                new Questions {Question="Har jeg angrerett etter å ha lagt inn bestilling?",Asked=RandomDay(start),CategoryId=4,Email="hansen.per@gmail.com",UpVotes=33}
            };
            //questions.ForEach(c => context.Questions.Add(c));
            //context.SaveChanges();
            var answers = new List<Answers>
            {
                new Answers {Answer="Leveranse av varer avhenger av hvor du bor. Varene pakkes og sendes stort sett neste arbeidsdag, og man kan forvente å få dem levert på døren 1-2 dager etter det igjen.",Answered = RandomDay(questions[0].Asked),UserId=1,Question=questions[0]},
                new Answers {Answer="Dersom ingen personer over 18 år er hjemme for å ta i mot varene når de ankommer, så blir de sendt til nærmeste postkontor der de kan hentes av personer over 18 år.",Answered = RandomDay(questions[1].Asked),UserId=1,Question=questions[1]},
                new Answers {Answer="Vi tar i mot BitCoin som betalingsmiddel.",Answered=RandomDay(questions[2].Asked),UserId=1,Question=questions[2]},
                new Answers {Answer="Det går helt fint å betale med forskuddsfaktura. Merk at det da tar noe lenger tid før varene blir sendt og levert.",Answered=RandomDay(questions[3].Asked),UserId=2,Question=questions[3]},
                new Answers {Answer="Det er ingen aldersgrense for å handle hos oss, men leveringen kan kun mottas av personer over 18 år (20 år dersom leveransen inneholder varer med høyere enn 4,7% alkoholinnhold",Answered=RandomDay(questions[4].Asked),UserId=4,Question=questions[4]},
                new Answers {Answer="Nei, det er det ikke.",Answered=RandomDay(questions[5].Asked),UserId=2,Question=questions[5]},
                new Answers {Answer="What you see is what you get! Neida, vi er ikke tilhenger av WYSIWYG, så vi kan selvfølgelig bestille opp varer som ikke er i vårt sortiment.",Answered=RandomDay(questions[6].Asked),UserId=1,Question=questions[6]},
                new Answers {Answer="Kom med minimum tre forslag til forbedringer av denne FAQ'en, så vil du bli vurdert på bakgrunn av det.",Answered=RandomDay(questions[7].Asked),UserId=3,Question=questions[7]},
                new Answers {Answer="Fraktprisen er avhengig av hvordan du ønsker å få tilsendt pakken og hvordan du vil betale. Vi tilbyr å betale med bankkort i nettbutikken, få faktura vedlagt pakken eller betale ved hjelp av oppkrav når du henter pakken. I tillegg tilbyr vi også delbetaling fra Klarna. Vi har avtale med Posten og Bring for sending av våre bestillinger.",Answered=RandomDay(questions[11].Asked),UserId=3,Question=questions[11]},
                new Answers {Answer="Denne nettbutikken følger norsk lov (så langt det lar seg gjøre), så du har derfor 14 dagers angrerett. Benytt det medfølgende angrerettsskjemaet",Answered=RandomDay(questions[12].Asked),UserId=1,Question=questions[12]}
            };

            //answers.ForEach(a => context.Answers.Add(a));
            foreach(var answer in answers){
                context.Answers.Add(answer);
                answer.Question.Answer = answer; 
            }
            questions.ForEach(c => context.Questions.Add(c));

            context.SaveChanges(); 
 
        }

        DateTime RandomDay(DateTime start)
        {
            return start.AddDays(prodDate.Next(dateRange));
        }
    }
}