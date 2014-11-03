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
        protected override void Seed(DatabaseContext context)
        {
            var categories = new List<Categories>
            {
                new Categories {Name = "Faktura"},
                new Categories {Name = "Transport"}
            };
            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            var questions = new List<Questions>
            {
                new Questions {Question="Usikkert", Answer="42",CategoriesId=1},
                new Questions {Question="Hva blir det til middag?", Answer="Fårikål",CategoriesId=2}
            };
            questions.ForEach(c => context.Questions.Add(c));
            context.SaveChanges(); 
        }
    }
}