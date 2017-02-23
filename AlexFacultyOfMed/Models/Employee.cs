using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlexFacultyOfMed.Models
{
    public class Employee
    {
        public Employee()
        {
            DailyFileDetails = new List<DailyFileDetails>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Code { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(150)]
        public string Nickname { get; set; }
        public int? Gender { get; set; }
        [StringLength(50)]

        public string NationalId { get; set; }


        //[Required]
        //public int Code { get; set; }
        [StringLength(100)]
        public string PositionName { get; set; }
        public int? DepartmentId { get; set; }
        // public int? PositionId { get; set; }
        [StringLength(100)]
        public string Phone { get; set; }
        [StringLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "تأكد من أدخال الأميل بشكل صحيح")]
        public string Email { get; set; }

        public bool Sallary { get; set; }
        public bool Other { get; set; }

     //   public Department Department { get; set; }
        //   public Position Position { get; set; }

        public virtual ICollection<DailyFileDetails> DailyFileDetails { get; set; }
    }
}