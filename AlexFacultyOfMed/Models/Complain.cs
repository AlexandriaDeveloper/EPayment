using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using AlexFacultyOfMed.Models;

namespace AlexFacultyOfMed.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Complain
    {
        public int Id { get; set; }
        [DataType("varchar")]
        [StringLength(128)]
        public string UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public bool Status { get; set; }

        public virtual ICollection<ComplainDetails> ComplainDetailses { get; set; }

        public Complain()
        {
            ComplainDetailses= new List<ComplainDetails>();
        }
    }
}

public class ComplainDetails
{
    public int Id { get; set; }

    public int ComplainId { get; set; }
    [StringLength(500)]
    [Required]
    public string ComplainQ { get; set; }
    
    public DateTime? ComplainQDate { get; set; }
    [StringLength(500)]
    public string ComplainA { get; set; }

    public DateTime? ComplainADate { get; set; }

    public Complain Complain { get; set; }
}