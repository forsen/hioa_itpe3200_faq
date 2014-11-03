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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
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
        public String Question { get; set; }
        public String Answer { get; set; }
        public int CategoriesId { get; set; }
        public Categories Categories { get; set; }
    }
}