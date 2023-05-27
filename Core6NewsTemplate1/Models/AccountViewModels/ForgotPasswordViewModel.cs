using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "RequiredFieldError")]
        [EmailAddress(ErrorMessage = "EmailError")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
