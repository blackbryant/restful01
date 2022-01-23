using System;
using System.ComponentModel.DataAnnotations;
using resultful01.Entity;
using System.Linq; 

namespace resultful01.EntityValidation
{
    public class BlogParamAttribute : ValidationAttribute
    {

        private string tvalue;
        public string param;

        public BlogParamAttribute(string tvalue="1234")
        {
            this.tvalue = tvalue; 
        }
        

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            return new ValidationResult(tvalue,new string[] {"tvalue" });
    
        }

    }
}
