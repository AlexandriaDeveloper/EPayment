using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexFacultyOfMed.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DailyFileDetailsData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int DailyFileDetailsId { get; set; }

        public int AccountId { get; set; }
        [StringLength(150)]
        public string Value { get; set; }

        public virtual Account Account { get; set; }

        public virtual DailyFileDetails DailyFileDetails { get; set; }

        public DailyFileDetailsData()
        {

        }
    }
}