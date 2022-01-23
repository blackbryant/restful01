using System;
using System.ComponentModel.DataAnnotations;
using resultful01.Entity;
using System.Linq; 

namespace resultful01.EntityValidation
{
    public class BlogValueAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            //取的物件
            var createDate = DateTime.Parse(value.ToString());
            

            if (createDate <= DateTime.Now)
            {

                return new ValidationResult("不能小於現在時間",new string[] {"creatDate" });
            }


            return ValidationResult.Success; 



        }



    }
}
