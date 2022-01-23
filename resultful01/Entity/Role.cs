using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using resultful01.EntityValidation;
using System.Linq;

namespace resultful01.Entity
{ 
    public class Role  
    {
        public int Id { get; set; }
        public string RoleAuth { get; set; }
        public int EmployId { get; set; }
        //public Employee employee { get; set; }
        
       

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Id:"+this.Id);
            sb.Append("Role:" + this.RoleAuth);
            //sb.Append("EmployId:" + this.EmployId);

            return sb.ToString();
        }

    }
}