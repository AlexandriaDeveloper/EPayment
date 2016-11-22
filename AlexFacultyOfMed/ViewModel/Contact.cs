using System.Collections.Generic;
using AlexFacultyOfMed.Models;

namespace AlexFacultyOfMed.ViewModel
{
    public class PaymentInfoVM
    {
        public Employee Employee { get; set; }
        public List<PaymentData> PaymentData { get; set; }
        public int Pages { get; set; }
        public short CurrentPage { get; set; }
    }


    public class PaymentData
    {
        public DailyFileDetails DailyFileDetails { get; set; }
        public DailyFile DailyFile { get; set; }
        public Daily Daily { get; set; }
        //public int MyProperty { get; set; }
        //public string  FileName { get; set; }
        //public decimal Net { get; set; }
    }
}