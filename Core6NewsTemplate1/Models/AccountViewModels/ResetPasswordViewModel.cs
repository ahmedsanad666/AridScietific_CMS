using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "RequiredFieldError")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "RequiredFieldError")]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "MinMaxTextFieldError", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "ConfirmpasswordError")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Code")]
        public string Code { get; set; }
    }
}
