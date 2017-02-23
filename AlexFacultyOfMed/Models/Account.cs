using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexFacultyOfMed.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }


        [Index("AccountIdIndex")]
        public int? AccountId { get; set; }


        public virtual Account Account1 { get; set; }
        public virtual List<Account> ChildAccounts { get; set; }
        public virtual List<DailyFileDetailsData> DailyFileDetailsDatas { get; set; }
        public Account()
        {
            ChildAccounts = new List<Account>();
            DailyFileDetailsDatas = new List<DailyFileDetailsData>();
        }
    }
}