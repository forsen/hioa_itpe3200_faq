using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAQ.Models
{
    public class Question
    {
        public int id { get; set; }
        public DateTime asked { get; set; }
        public String question { get; set; }
        public String answer { get; set; }
        public String email { get; set; }
        public int categoryid { get; set; }
        public String category { get; set; }
    }
}