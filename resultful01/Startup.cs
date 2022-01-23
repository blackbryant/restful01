using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using resultful01.DAO;
using resultful01.Entity;
using resultful01.Service;

namespace resultful01
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*services.AddDbContext<BloggingContext>(
                options => options.UseSqlite($"Data Source=blogging.db")
              );
            */
            services.AddScoped<BlogDAO>();
            services.AddScoped<PostDAO>();
            services.AddScoped<BlogService>(); 
            services.AddEntityFrameworkSqlite().AddDbContext<BloggingContext>();
            services.AddControllers();
            
            services.AddAutoMapper(typeof(Startup));

            //啟用cookie驗證
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                //未登入時會自動導到這個網址
                option.LoginPath = new PathString("/api/LoginCookie/NoLogin");

                //沒有存取權限
                option.AccessDeniedPath = new PathString("/api/LoginCookie/NoAccess");

                //時效性
                option.ExpireTimeSpan = TimeSpan.FromSeconds(2); 

            });

            //將controller都要做驗證才能存取資料
            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //cookie 驗證宣告,順序要一樣
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
