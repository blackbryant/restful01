using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using resultful01.DAO;
using resultful01.Dtos;
using resultful01.Entity;
using resultful01.QueryParameters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace resultful01.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class Blogs2Controller : ControllerBase
    {
        //private readonly BloggingContext _bloggingContext;
        private BlogDAO _blogDao;
        private PostDAO _postDao; 
        private readonly IMapper _mapper; 
        /*public BlogsController(BloggingContext bloggingContext) {
            this._bloggingContext = bloggingContext; 
        }
        */
        public Blogs2Controller(BlogDAO blogDao,PostDAO postDAO,IMapper mapper)
        {
            this._postDao = postDAO; 
            this._blogDao = blogDao; 
            this._mapper = mapper; 
        }

        // GET: api/Blogs2
        [HttpGet]
        public IEnumerable<BlogDto> Get()
        {

  
            return this._blogDao.GetBlogDetail(); ;

        }


        //新增父子資料
        [HttpPost]
        public ActionResult Post([FromBody] Blog blog)
        {
            int result = 0;
            if (blog == null)
            {
                Ok("沒有");
            }



            if (blog.Posts == null)
            {
                blog.CreateDate = DateTime.Now;
                Console.WriteLine(blog.ToString());
                result = this._blogDao.Create(blog);

            }
            else
            {
                var posts = blog.Posts;
                foreach (Post post in posts)
                {
                    post.CreateDate = DateTime.Now; 
                }

                Console.WriteLine(blog.ToString());
                result = this._blogDao.Create(blog);

            }

            Console.Write("result:"+result);

            return Ok("新增成功！！ "); 

        }

        //沒設外鍵時同時新增父子資料
        [HttpPost("PostNoFK")]
        public IActionResult PostNoFK([FromBody] Blog blog)
        {
            Console.Write("bnlog:" + blog.ToString());

            int result = 0;
            //先新增父檔
            blog.CreateDate = DateTime.Now;
            this._blogDao.Create(blog);

            List<Post> posts = new List<Post>(); 
            //新增子檔
            foreach (Post post in blog.Posts)
            {
                Post newPost = new Post();

                newPost.Content = post.Content;
                newPost.Memo = post.Memo;
                newPost.Title = post.Title; 
                newPost.BlogId = blog.BlogId;
                newPost.CreateDate = DateTime.Now;
                newPost.Enable = post.Enable; 
                posts.Add(newPost); 


            }
            result += this._postDao.Create(posts);

            Console.Write("result:" + result);

            //return Ok("新增成功！！ "+result);
            //Blog 
            return CreatedAtAction(nameof(GetOne), new { blogId = blog.BlogId }, new Blog());

        }

        [HttpGet("GetOne/{blogId}")]
        public Blog GetOne(int blogId)
        {
            Console.WriteLine(blogId);
            Blog blog = this._blogDao.QueryById(blogId);
            return blog; 
        }


        //更新資料
        [HttpPut("{id}")]
        public Blog Put(int id, [FromBody] Blog value)
        {
            value.CreateDate = DateTime.Now; 
            Blog blog = this._blogDao.UpdateById(id,value);


            return blog; 
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Console.WriteLine(id);

            this._blogDao.Delete(id); 



        }

        //沒關聯刪除資料
        // DELETE api/values/5
        [HttpDelete("Delete2/{id}")]
        public void Delete2(int id)
        {
            Console.WriteLine(id);
            this._blogDao.Delete2(id);
            this._postDao.delete2(id);
        }



        //刪除多筆資料
        [HttpDelete("DeleteN/{ids}")]
        public void DeleteN(string ids)
        {
            Console.WriteLine(ids);
            //將json轉換成list
            List<string> list = JsonSerializer.Deserialize<List<string>>(ids);
            List<int> deleteList = new List<int>();
            foreach (string s in list)
            {
                int id = int.Parse(s);
                deleteList.Add(id);
            }


            this._blogDao.DeleteN(deleteList);


        }
        

    }
}
