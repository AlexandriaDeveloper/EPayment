namespace AlexFacultyOfMed.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DailyFileDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public decimal Net { get; set; }
        [Index("DailyFileIdIndex")]
        public int DailyFileId { get; set; }
        [Index("EmployeeIdIndex")]
        public int EmployeeId { get; set; }
        public bool IsAtm { get; set; }
        public bool HasData { get; set; }
        public DailyFile DailyFile { get; set; }



        public virtual Employee Employee { get; set; }

        public virtual ICollection<DailyFileDetailsData> DailyFileDetailsDatas { get; set; }
    }





















}