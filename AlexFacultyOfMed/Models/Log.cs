using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexFacultyOfMed.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Log
    {
        [Key]
        
        public int Id { get; set; }

        public DateTime LogDate { get; set; }

        public string ExceptionMessage { get; set; }
    }
}