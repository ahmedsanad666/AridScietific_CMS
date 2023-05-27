using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "RequiredFieldError")]
        [EmailAddress(ErrorMessage = "EmailError")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(450)]
        [Display(Name = "ReferredById")]
        public string ReferredById { get; set; }

        [Required(ErrorMessage = "RequiredFieldError")]
        [EmailAddress(ErrorMessage = "EmailError")]
        [Display(Name = "ConfirmEmail")]
        [Compare("Email", ErrorMessage = "ConfirmEmailError")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "RequiredFieldError")]
        [StringLength(100, ErrorMessage = "MinMaxTextFieldError", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmpassword")]
        [Compare("Password", ErrorMessage = "ConfirmpasswordError")]
        public string ConfirmPassword { get; set; }

        // Added by Alrshah ===========================================================================================
        //Added by  saif https://stackoverflow.com/questions/12518689/regular-expression-not-to-allow-numbers-just-arabic-letters/12518814
        [Required(ErrorMessage = "الاسم مطلوب لاكمال التسجيل")]
        [RegularExpression(@"^[\u0621-\u064A\040]+$", ErrorMessage = "الاسم يجب ان يكون باللغة العربية")]
        [StringLength(50, ErrorMessage = "يجب كتابة الاسم الثلاثي", MinimumLength = 10)]
        [Display(Name = "ArName")]
        public string ArName { get; set; }

        [StringLength(50)]
        [Display(Name = "EnName")]
        public string EnName { get; set; }

        [Required(ErrorMessage = "RequiredFieldError")]
        [Display(Name = "UILanguage")]
        public string UILanguage { get; set; }

        // Below commented by Saif
        //[Required(ErrorMessage = "RequiredFieldError")]
        //[DataType(DataType.Date)]
        //[Display(Name = "DateofBirth")]
        //public DateTime DateofBirth { get; set; }

        //[Required(ErrorMessage = "RequiredFieldError")]
        //[DefaultValue(GenderType.Unknown)]
        //[Display(Name = "Gender")]
        //public GenderType Gender { get; set; }



        [Required(ErrorMessage = "RequiredFieldError")]
        [Display(Name = "CountryId")]
        public int CountryId { get; set; }

        //[Required(ErrorMessage = "RequiredFieldError")]
        //[Display(Name = "CityId")]
        //public int CityId { get; set; }

        //[Required(ErrorMessage = "RequiredFieldError")]
        //[Display(Name = "UniversityId")]
        //public int UniversityId { get; set; }

        //[Required(ErrorMessage = "RequiredFieldError")]
        //[Display(Name = "FacultyId")]
        //public int FacultyId { get; set; }
        // Added by Alrshah ===========================================================================================

    }
}
