using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlexFacultyOfMed.Models
{
    public class ExpensessType
    {
        public ExpensessType()
        {
            Dailies = new List<Daily>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Daily> Dailies { get; set; }
    }
}