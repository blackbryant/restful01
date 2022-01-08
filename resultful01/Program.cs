using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using resultful01.Entity;

namespace resultful01
{
    public class Program
    {
        public static void Main(string[] args)
        {
           // blog();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        public static void blog() {
            using (var db = new BloggingContext())
            {
                // Note: This sample requires the database to be created before running.
                Console.WriteLine($"Database path: {db.DbPath}");

                // Create
                Console.WriteLine("Inserting a new blog");
                db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                db.Add(new Blog { Url = "http://blogs2.msdn.com/adonet" });
                db.Add(new Blog { Url = "http://blogs3.msdn.com/adonet" });
                db.Add(new Blog { Url = "http://blogs4.msdn.com/adonet" });
                db.Add(new Blog { Url = "http://blogs23434.msdn.com/adonet" });
                db.Add(new Post {  Title = "桃機群聚增1保全確診 1計程車司機疑染疫待複驗", Content = "（中央社記者江慧珺台北7日電）桃園機場COVID-19染疫案擴大，疫情指揮中心指揮官陳時中今天晚間證實，又增1名機場保全確診，另1名防疫計程車司機疑染疫待複驗，目前桃園機場確定染疫人數達13人", BlogId = 1 });
                db.Add(new Post {  Title = "合歡山降雪霰如灑糖霜 追雪車輛須加掛雪鍊", Content = "（中央社記者蕭博陽南投縣8日電）溫度與水氣配合，合歡山昨天晚間到今天凌晨間歇固態降水（霰或雪），整個山頭今天清晨彷彿灑上薄薄白色糖霜，讓過夜遊客好開心，還有追雪族自備塑膠板在邊坡享受滑雪樂趣。", BlogId = 1 });
                db.Add(new Post {  Title = " 為林靜儀站台 賴副總統：政治人物不應謀取私利", Content = "中央社記者蘇木春台中7日電）台中市第2選區立委補選進入倒數，民進黨籍候選人林靜儀今晚在大肚區舉辦造勢晚會，副總統賴清德到場站台，他表示，政治人物不應該謀取私利，呼籲用選票支持林靜儀", BlogId = 2});



                db.SaveChanges();

                // Read
                /*
                Console.WriteLine("Querying for a blog");
                var blog = db.Blogs
                    .OrderBy(b => b.BlogId)
                    .First();

                // Update
                Console.WriteLine("Updating the blog and adding a post");
                blog.Url = "https://devblogs.microsoft.com/dotnet";
                blog.Posts.Add(
                    new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
                db.SaveChanges();

                // Delete
                Console.WriteLine("Delete the blog");
                db.Remove(blog);
                db.SaveChanges();
               */
            }
        }
    


    }
}
