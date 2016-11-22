using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPayment.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }


        public virtual ICollection<Employee> Employees  { get; set; }


        public Department()
        {
            Employees= new List<Employee>();
        }

    }
}