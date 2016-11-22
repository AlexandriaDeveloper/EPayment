using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace EPayment.Models
{
    public class Daily
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal? TotalAmount { get; set; }

        public string CheckGP { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DailyDay { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DateTime? dueDate { get; set; }
        public int ExpensessTypeId{ get; set; }
        public string FilePath { get; set; }
        public bool Open { get; set; }


        public ExpensessType ExpensessType { get; set; }
        public virtual ICollection<DailyFile> DailyFiles { get; set; }

        public Daily()
        {
            DailyFiles= new List<DailyFile>();
        }
    }

   
}