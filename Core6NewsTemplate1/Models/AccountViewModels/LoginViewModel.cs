using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "RequiredFieldError")]
        [EmailAddress(ErrorMessage = "EmailError")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "RequiredFieldError")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember")]
        public bool RememberMe { get; set; }        
    }
}
