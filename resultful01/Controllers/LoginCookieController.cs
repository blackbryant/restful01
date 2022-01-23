using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using resultful01.Dtos;
using resultful01.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace resultful01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginCookieController : ControllerBase
    {

        private readonly BloggingContext _blogingContext;

        public LoginCookieController(BloggingContext bloggingContext)
        {
            this._blogingContext = bloggingContext; 
        }


        [HttpPost("Login")]
        public IActionResult Login(LoginInfo value)
        {
            var user = (from a in _blogingContext.Employees
                        where a.Account == value.Account
                        && a.Password == value.Password
                        select a).SingleOrDefault();

            if (user == null)
            {
                return Content("帳號密碼錯誤");
            }
            else
            {
                Console.WriteLine(user.ToString());
                //驗證
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("FullName",user.Account+"-"+user.Name)
                    //new Claim(ClaimTypes.Role, user.Role)

                };

                //Role
                var roles = from a in _blogingContext.Roles
                            where a.EmployId == user.Id
                            select a; 


                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleAuth));
                    Console.WriteLine(role);
                }


                //設定失效日期
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }

            return Ok("成功登入cookies"); 
        }


        [HttpPost("Logout")]
        public void  Logout()
        {

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        }

        [HttpGet("NoLogin")]
        public IActionResult NoLogin()
        {
            return Content("未登入"); 

        }

        [HttpGet("NoAccess")]
        public IActionResult NoAccess()
        {
            return Content("沒有權限");

        }


    }
}
