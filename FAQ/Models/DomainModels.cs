using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FAQ.Models
{
    public class Question
    {
        public int id { get; set; }
        public String asked { get; set; }
        [Required]
        public String question { get; set; }
        public Answer answer { get; set; }
        [Required]
        [RegularExpression("^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")]
        public String email { get; set; }
        [Required]
        [RegularExpression("^[1-9]+[0-9]*")]
        public int categoryid { get; set; }
        public String categoryname { get; set; }
        public int upvotes { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string glyph { get; set; }
        public List<Question> questions { get; set; }
    }

    public class Answer
    {
        public int id { get; set; }
        public String answered { get; set; }
        public int userid { get; set; }
        public string answer { get; set; }
    }
}