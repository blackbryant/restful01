using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using resultful01.Dtos;
using resultful01.Entity;
using resultful01.QueryParameters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace resultful01.Controllers
{
    [Route("api/[controller]")]
    public class BlogsController : Controller
    {
        private readonly BloggingContext _bloggingContext;
        private readonly IMapper _mapper; 
        /*public BlogsController(BloggingContext bloggingContext) {
            this._bloggingContext = bloggingContext; 
        }
        */
        public BlogsController(BloggingContext bloggingContext, IMapper mapper)
        {
            this._bloggingContext = bloggingContext;
            this._mapper = mapper; 
        }


        // GET: api/Blogs
        [HttpGet]
        public IEnumerable<BlogDto> Get()
        {
            Console.WriteLine("BlogDto");
            var result = from blog in _bloggingContext.Blogs
                         join post in _bloggingContext.Posts on blog.BlogId equals post.BlogId
                         select new BlogDto{
                             BlogId = blog.BlogId,
                             Title = post.Title,
                             Content = post.Title
                         };
                       

            return result; 
             
        }

        //AutoMapper 
        [HttpGet("AutoMapper")]
        public IEnumerable<BlogDto> AutoMapper()
        {
            Console.WriteLine("AutoMapper");
            var result = from blog in _bloggingContext.Blogs
                         join post in _bloggingContext.Posts on blog.BlogId equals post.BlogId
                         select new { blog, post}
                         //select blog
                          ;

       
            List<BlogDto> blogList = new List<BlogDto>();
            foreach (var item in result)
            {
                Blog b = item.blog;
                Post p = item.post;
                BlogDto blogDto = new BlogDto();
                _mapper.Map(b, _mapper.Map(p, blogDto));
                blogList.Add(blogDto);

            }
            return blogList.ToList();

        }

        // GET: api/UploadFiles
        [HttpGet]
        public IEnumerable<BlogDto> UploadFiles()
        {
            Console.WriteLine("BlogDto");
            var result = from blog in _bloggingContext.Blogs
                         join post in _bloggingContext.Posts on blog.BlogId equals post.BlogId
                         select new BlogDto
                         {
                             BlogId = blog.BlogId,
                             Title = post.Title,
                             Content = post.Title
                         };


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


        //get參數查詢，目前不考慮效能寫法
        [HttpGet("GetsCodi")]
        public IEnumerable<BlogDto> GetsCodi(string? category)
        {
            Console.WriteLine("GetPosts=>category:{0}",category);
            var result = from blog in _bloggingContext.Blogs
                         join post in _bloggingContext.Posts on blog.BlogId equals post.BlogId
                         select new BlogDto
                         {
                             BlogId = blog.BlogId,
                             Title = post.Title,
                             Content = post.Title,
                             Category = blog.Category
                         };

            //參數查詢
            if (!string.IsNullOrEmpty(category)) {
                result = result.Where(a => a.Category == category);
            }
            

            return result;
        }

        //get參數查詢，目前不考慮效能寫法
        [HttpGet("GetsCodiObj")]
        public IEnumerable<BlogDto> GetsCodiObj([FromQuery] PostParameters parameters)
        {
            Console.WriteLine("GetPosts=>{0}", parameters.ToString());
            var result = from blog in _bloggingContext.Blogs
                         join post in _bloggingContext.Posts on blog.BlogId equals post.BlogId
                         select new BlogDto
                         {
                             BlogId = blog.BlogId,
                             Title = post.Title,
                             Content = post.Title,
                             Category = blog.Category,
                             Enable = post.Enable,
                             CreateDate = post.CreateDate
                         };

            //參數查詢
            if (!string.IsNullOrEmpty(parameters.Category))
            {
                result = result.Where(a => a.Category == parameters.Category);
            }

            if (parameters.Enable != null)
            {
                result = result.Where(a => a.Enable == parameters.Enable);
            }

            if (parameters.CreateDate != null)
            {
               // DateTime date01 = DateTime.Parse(parameters.CreateDate); 

                result = result.Where(a => a.CreateDate.Equals(parameters.CreateDate)); 
            }


            return result;
        }

        //自訂ＳＱＬ指令，轉成DTO
        [HttpGet("GetSql01")]
        public IEnumerable<BlogDto> GetSql(string? category)
        {
            string sql = "select a.blogId, url, p.Title , p.Content , Category , p.CreateDate, p.Enable " +
                "from blogs a  " +
                "join Posts p on a.BlogId  =p.PostId "+
                "join UploadFile uf on uf.PostId  = p.PostId "+
                "where 1=1 "
                ;

            
            if (!string.IsNullOrEmpty(category))
            {
                sql = sql + "and category like N'%" + category + "%'";

            }

            var result = _bloggingContext.BlogDtos.FromSqlRaw(sql);

            return result; 

        }


        //自訂ＳＱＬ指令，轉成DTO
        [HttpGet("GetSql02")]
        public IEnumerable<BlogDto> GetSql02(string? category)
        {
            string sql = "select a.blogId, url, p.Title , p.Content , Category , p.CreateDate, p.Enable " +
                "from blogs a  " +
                "join Posts p on a.BlogId  =p.PostId " +
                "join UploadFile uf on uf.PostId  = p.PostId " +
                "where 1=1 "
                ;


            if (!string.IsNullOrEmpty(category))
            {
                sql = sql + "and category like N'%" + category + "%'";

            }

            var result = _bloggingContext.ExecSQL<BlogDto>(sql);

            return result;

        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody] Blog blog)
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
