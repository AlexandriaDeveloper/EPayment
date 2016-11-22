using System;
using System.ComponentModel.DataAnnotations;

namespace AlexFacultyOfMed.ViewModel.AdminVM
{
    public class IndexVm
    {
        [Required]
        [Display(Name = "الرقم القومى")]
        [MaxLength(14, ErrorMessage = "تأكد من أدخال 14 رقم فقط")]
        [MinLength(14, ErrorMessage = "تأكد من أدخال 14 رقم فقط")]
        public string NationalId { get; set; }
    }


    public class UserDetailsVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "البريد الألكترونى")]
        public string Email { get; set; }

        [Required]
        [MinLength(14, ErrorMessage = "عفوا يجب ادخل 14 رقم ")]
        [MaxLength(14, ErrorMessage = "عفوا يجب ادخل 14 رقم ")]
        [Display(Name = "الرقم القومى")]
        public string NationalId { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "تأكد من ادخال رقم المحمول بشكل صحيح")]
        [MinLength(11, ErrorMessage = "عفوا يجب ادخل 11 رقم ")]
        [MaxLength(11, ErrorMessage = "عفوا يجب ادخل 11 رقم ")]
        [Display(Name = "رقم الهاتف ")]
        public string PhoneNumber { get; set; }

        [Display(Name = "تفعيل الحساب  ")]
        public bool EmailConfirmed { get; set; }


        [Display(Name = "قفل الحساب")]
        public bool LockoutEnabled { get; set; }


        [Display(Name = "أعادة تفعيل الحساب")]
        [DataType(DataType.DateTime)]
        public DateTime? LockoutEndDateUtc { get; set; }
    }
}