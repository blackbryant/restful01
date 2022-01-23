using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using resultful01.Entity;
using resultful01.QueryParameters;

namespace resultful01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly BloggingContext _bloggingContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env; 

        public UploadController(BloggingContext bloggingContext, IMapper mapper, IWebHostEnvironment env)
        {
            this._bloggingContext = bloggingContext;
            this._mapper = mapper;
            this._env = env; 

        }

        [HttpPost]
        public IActionResult Post(IEnumerable<IFormFile> files, [FromForm]Guid id)
        {
            
            string root = _env.ContentRootPath+Path.DirectorySeparatorChar+@"files2"+ Path.DirectorySeparatorChar;
            Console.WriteLine("root:" + root);
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root); 
            }
            try
            {
                foreach (var formFile in files)
                {
                    string fileName = formFile.FileName;
                    Console.WriteLine("fileName:" + fileName);
                    using (var stream = System.IO.File.Create(root + fileName))
                    {
                        formFile.CopyTo(stream);
                        UploadFile insert = new UploadFile
                        {
                            UploadFileId = id,
                            Src = root + fileName,
                            Name = fileName

                        };

                        _bloggingContext.UploadFile.Add(insert);
                    }
                    _bloggingContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return Content("發生錯誤"+ex.Message);
            }



            return Ok();

        }

        [HttpPost("PostJson")]
        public IActionResult PostJson([FromForm]UploadParam uploadParam)
        {
            Console.WriteLine("json:" + uploadParam.Form); 
            //先轉成物件
           // UploadFile uploadFile = JsonSerializer.Deserialize<UploadFile>(form);



            string root = _env.ContentRootPath + Path.DirectorySeparatorChar + @"files2" + Path.DirectorySeparatorChar;
            Console.WriteLine("root:" + root);
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            try
            {
                foreach (var formFile in uploadParam.Files)
                {
                    string fileName = formFile.FileName;
                    Console.WriteLine("fileName:" + fileName);
                    using (var stream = System.IO.File.Create(root + fileName))
                    {
                        formFile.CopyTo(stream);
                        UploadFile insert = new UploadFile
                        {
                            UploadFileId = uploadParam.Form.UploadFileId,
                            Src = root + fileName,
                            Name = fileName

                        };

                        _bloggingContext.UploadFile.Add(insert);
                    }
                    _bloggingContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return Content("發生錯誤" + ex.Message);
            }



            return Ok();

        }


    }
}
