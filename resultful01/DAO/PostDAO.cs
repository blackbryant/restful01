using System;
using System.Collections;
using System.Collections.Generic;
using resultful01.Dtos;
using resultful01.Entity;
using System.Linq;

namespace resultful01.DAO
{
    public class PostDAO
    {
        public readonly  BloggingContext _bloggingContext;

        public PostDAO(BloggingContext bloggingContext)
        {
            this._bloggingContext = bloggingContext;
             
        }


        public int Create(List<Post> posts)
        {
            int result = 0;
            this._bloggingContext.Posts.AddRange(posts);
            result =  this._bloggingContext.SaveChanges();
           

            return result; 

        }



        public int delete2(int id )
        {
            int result = 0;
            var delete = from post in _bloggingContext.Posts
                         where post.BlogId == id
                         select post;
                          
            if (delete != null)
            {
                _bloggingContext.RemoveRange(delete);
                _bloggingContext.SaveChanges();
                result = delete.Count(); 
            }

            return result; 
        }



    }
}
