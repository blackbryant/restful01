using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using resultful01.Dtos;
using resultful01.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace resultful01.Controllers
{
    [Route("api/[controller]")]
    public class BlogsController : Controller
    {
        private readonly BloggingContext _bloggingContext; 
        public BlogsController(BloggingContext bloggingContext) {
            this._bloggingContext = bloggingContext; 
        }


        // GET: api/Blogs
        [HttpGet]
        public IEnumerable<BlogDto> Get()
        {
            Console.WriteLine("AAAAAA");
            var result = _bloggingContext.Blogs.Select(a=> new BlogDto {
                BlogId = a.BlogId,
                Url = a.Url 

            });

            return result; 
             
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public BlogDto Get(int id)
        {
            Console.WriteLine("id:" + id);
            var result = _bloggingContext.Blogs.Where(a => a.BlogId == id)
                .Select(a => new BlogDto
                {
                    BlogId = a.BlogId,
                    Url = a.Url

                }).SingleOrDefault(); 

            return result;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
