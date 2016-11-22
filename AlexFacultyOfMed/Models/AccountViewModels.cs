using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AlexFacultyOfMed.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "البريد الألكترونى")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "البريد الألكترونى")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة السر")]
        public string Password { get; set; }

        [Display(Name = "تذكرنى ؟")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "البريد الألكترونى")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "يجب الا تقل عن 6  ارقام ", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة السر")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة السر")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "غير مطابقة لكلمة السر")]
        public string ConfirmPassword { get; set; }


        [Required]
        [Display(Name = "الكود السرى")]
        [MaxLength(6, ErrorMessage = "عفوا الكود الذى قمت بإدخالة خطـأ ")]
        [MinLength(6, ErrorMessage = "عفوا الكود الذى قمت بإدخالة خطـأ ")]
        public string Code { get; set; }


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
        public string MobileNumber { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "البريد الألكترونى")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "يجب الا تقل عن 6 احرف و ان تحتوى على ارقام", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة السر")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة السر")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "غير مطابق لكلمة السر ")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "تأكد من ادخال رقم المحمول بشكل صحيح")]
        [MinLength(11, ErrorMessage = "عفوا يجب ادخل 11 رقم ")]
        [MaxLength(11, ErrorMessage = "عفوا يجب ادخل 11 رقم ")]
        [Display(Name = "رقم الهاتف ")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "يجب أدخال البريد الألكترونى")]
        [Display(Name = "البريد الألكترونى")]
        public string Email { get; set; }
    }

    public class ResendConfirmationEmail
    {
        [Required(ErrorMessage = "يجب أدخال عنوان البريد")]
        [EmailAddress(ErrorMessage = "تأكد من  عنوان البريد الألكترونى ")]
        [Display(Name = "البريد الألكترونى")]
        public string Email { get; set; }

        [Required(ErrorMessage = "يجب أدخال الكود السرى")]
        [Display(Name = "الكود السرى")]
        [MaxLength(6, ErrorMessage = "عفوا الكود الذى قمت بإدخالة خطـأ ")]
        [MinLength(6, ErrorMessage = "عفوا الكود الذى قمت بإدخالة خطـأ ")]
        public string Code { get; set; }


        [Required(ErrorMessage = "يجب أدخال الرقم القومى")]
        [Display(Name = "الرقم القومى")]
        public string NationalId { get; set; }
    }
}