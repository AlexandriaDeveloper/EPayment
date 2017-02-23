using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexFacultyOfMed.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class DailyFile
    {
        public DailyFile()
        {
            DailyFileDetailses = new List<DailyFileDetails>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }
        [StringLength(128)]
        public string DataEntryId { get; set; }
        [StringLength(50)]
        public string FileNumberInfo { get; set; }

        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Computed)]
        public int? EmployeesNumber { get; set; }
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Computed)]
        public int? EmployeesCashNumber { get; set; }
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Computed)]

        public decimal? FileTotalAmount { get; set; }

        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Computed)]

        public decimal? FileTotalCashAmount { get; set; }


        [StringLength(200)]
        public string FilePath { get; set; }

        public DateTime CreatedDate { get; set; }
        [Index("DailyIdIndex")]
        public int DailyId { get; set; }

        public Daily Daily { get; set; }
        public virtual ICollection<DailyFileDetails> DailyFileDetailses { get; set; }
    }

    /*


        [Required]
        public string Name { get; set; }
        [Required]

        public string DataEntryId { get; set; }
      
        public string FileNumberInfo { get; set; }
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Computed)]
        public int? EmployeesNumber { get; set; }
        [DatabaseGenerated(databaseGeneratedOption:DatabaseGeneratedOption.Computed)]
        public decimal? FileTotalAmount { get; set; }
        public string FilePath { get; set; }

        public DateTime CreatedDate { get; set; }
        [Index("DailyIdIndex")]
        public int DailyId { get; set; }

        public Daily Daily { get; set; }
        public virtual List<DailyFileDetails> DailyFileDetailses { get; set; }
     */

}