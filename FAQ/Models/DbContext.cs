using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace FAQ.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base ("name=faq")
        {

        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Answers> Answers {get; set;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Answers>()
                .HasRequired(a => a.Questions)
                .WithOptional(q => q.Answers);
            modelBuilder.Entity<Questions>()
                .HasOptional(pi => pi.Answers )
                .WithRequired(lu => lu.Questions);
        }

    }

    public class Categories
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public virtual List<Questions> Questions { get; set; }
    }

    public class Questions
    {
        [Key]
        public int Id { get; set; }
        public DateTime Asked { get; set; }
        public String Question { get; set; }
        public Answers Answers { get; set; }
        public String Email { get; set; }
        public int CategoriesId { get; set; }
        public Categories Categories { get; set; }

    }

    public class Answers
    {
        [Key]
        public int Id { get; set; }
        public DateTime Answered { get; set; }
        public int UserId { get; set; }
        public String Answer { get; set; }
        public Questions Questions { get; set; }
    }
}