using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace EPayment.Models
{
    public class ExpensessType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Daily> Dailies { get; set; }

        public ExpensessType()
        {
            Dailies= new List<Daily>();
        }
    }
}