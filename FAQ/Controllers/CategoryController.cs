﻿using FAQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace FAQ.Controllers
{
   
    public class CategoryController : ApiController
    {
        DBCategory db = new DBCategory(); 

        // GET api/Category
        public HttpResponseMessage Get([FromUri] string unanswered)
        {

            List<Category> allCategories = db.getAllCategories(unanswered.Equals("true")); 
            if (allCategories == null)
                return new HttpResponseMessage()
                {
                    Content = new StringContent("Dette gikk galt", Encoding.UTF8, "application/json"),
                    StatusCode = HttpStatusCode.NoContent
                };
            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(allCategories);

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        public HttpResponseMessage Get(int id, [FromUri] string unanswered)
        {
            Category category = db.getCategory(id, unanswered.Equals("true"));
            if(category == null)
                return new HttpResponseMessage()
                {
                    Content = new StringContent("Dette gikk galt", Encoding.UTF8, "application/json"),
                    StatusCode = HttpStatusCode.NoContent
                };
            var Json = new JavaScriptSerializer();
            string JsonString = Json.Serialize(category);
            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonString, Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
