using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexFacultyOfMed.Models
{
    public class Daily
    {
        public Daily()
        {
            DailyFiles = new List<DailyFile>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal? TotalAmount { get; set; }

        public string CheckGP { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        public DateTime CreatedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        public DateTime? DailyDay { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        public DateTime? ClosedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        public DateTime? DueDate { get; set; }

        public int ExpensessTypeId { get; set; }
        public string FilePath { get; set; }
        public bool Open { get; set; }


        public ExpensessType ExpensessType { get; set; }
        public virtual ICollection<DailyFile> DailyFiles { get; set; }
    }
}