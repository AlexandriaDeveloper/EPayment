using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexFacultyOfMed.ViewModel
{
    using System.ComponentModel.DataAnnotations;

    public class ComplainViewModel
    {

        public int ComplainCounter {
            get
            {
                return ComplainItemsViewModels.Count;
            }
            set
            {

            }
        }

        public ComplainViewModel()
        {
            ComplainItemsViewModels = new List<ComplainItemsViewModel>();
        }
        public List<ComplainItemsViewModel> ComplainItemsViewModels { get; set; }
    }

    public class ComplainItemsViewModel
    {
        public int ComplainId { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool ComplainStatus { get; set; }

        public string UserName { get; set; }


    }

    public class ComplainDetailsViewModel
    {

        public int ComplainDetailsId { get; set; }

        public int ComplainId { get; set; }

        public string AskedBy { get; set; }

        [StringLength(500)]
        public string ComplainQ { get; set; }

        public DateTime? ComplainQDate { get; set; }
        [StringLength(500)]

        public string ComplainA { get; set; }

        public DateTime? ComplainADate { get; set; }


    }



    public class PostNewComplain
    {
        [StringLength(500,ErrorMessage = "عفوا يجب ان لا تزيد الرسالة عن 500 حرف فقط ")]
        public string ComplainQ { get; set; }
        [StringLength(500, ErrorMessage = "عفوا يجب ان لا تزيد الرسالة عن 500 حرف فقط ")]
        public string ComplainA { get; set; }
    }
}