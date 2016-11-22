using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexFacultyOfMed.Models
{
    public class DailyFile
    {
        public DailyFile()
        {
            DailyFileDetailses = new List<DailyFileDetails>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string DataEntryName { get; set; }
        public string FileNumberInfo { get; set; }
        public int? EmployeesNumber { get; set; }

        public decimal? FileTotalAmount { get; set; }
        public string FilePath { get; set; }

        public DateTime CreatedDate { get; set; }
        public int DailyId { get; set; }

        public Daily Daily { get; set; }
        public virtual ICollection<DailyFileDetails> DailyFileDetailses { get; set; }
    }
}