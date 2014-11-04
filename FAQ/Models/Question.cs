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
        public DateTime asked { get; set; }
        [Required]
        public String question { get; set; }
        public String answer { get; set; }
        [Required]
        [RegularExpression("^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")]
        public String email { get; set; }
        [Required]
        [RegularExpression("^[1-9]+[0-9]*")]
        public int categoryid { get; set; }
        public String category { get; set; }
    }
}