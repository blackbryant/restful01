using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using resultful01.Entity;

namespace resultful01.Controllers
{
    [Route("api/[controller]")]
    public class UploadController :Controller
    {
        private readonly BloggingContext _bloggingContext;
        private readonly IMapper _mapper;

        public UploadController(BloggingContext bloggingContext, IMapper mapper)
        {
            this._bloggingContext = bloggingContext;
            this._mapper = mapper;

        }

        [HttpGet("{UploadFileId}")]
        public ActionResult<IEnumerable<UploadFile>> Get(Guid UploadFileId)
        {
            Console.WriteLine("UploadFile:"+ UploadFileId);
            Console.WriteLine("UploadFile:" + _bloggingContext.UploadFile.Any(a => a.UploadFileId.Equals(UploadFileId)) );
            if (_bloggingContext.UploadFile.Any(a => a.UploadFileId == UploadFileId)) {
                return NotFound("找不到檔案");
            }

            var result = from a in _bloggingContext.UploadFile
                         
                         select   new {
                            UploadFileId =  a.UploadFileId ,
                            Name = a.Name,
                            Src = a.Src
                         };

            if (result == null || result.Count() == 0)
            {
                return NotFound("找不到檔案2");
            }


            return Ok(result); 

        }

         
    }
}
