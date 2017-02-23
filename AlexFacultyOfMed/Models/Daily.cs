using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexFacultyOfMed.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Daily
    {
        public Daily()
        {
            DailyFiles = new List<DailyFile>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public decimal? TotalAmount { get; set; }
        public int? TotalEmployees { get; set; }
        [StringLength(100)]
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
        [StringLength(128)]
        public string DataEntryId { get; set; }

        public bool Open { get; set; }
        public bool Cach { get; set; }

     //   public ExpensessType ExpensessType { get; set; }
        public virtual ICollection<DailyFile> DailyFiles { get; set; }
    }
}