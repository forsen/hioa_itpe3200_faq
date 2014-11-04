using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FAQ.Models
{
    public class Category
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public List<Questions> questions { get; set; }
    }
}