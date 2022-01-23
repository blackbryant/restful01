using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using resultful01.EntityValidation;
using System.Linq;

namespace resultful01.Entity
{ 
    public class Employee  
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        //public string Role { get; set; }
       // public List<string> Roles { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Id:"+this.Account);
            sb.Append("Account:" + this.Account);
            sb.Append("Password:" + this.Password);
            //sb.Append("Role:" + this.Role);

            return base.ToString();
        }

    }
}