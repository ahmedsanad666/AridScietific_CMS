using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required(ErrorMessage = "RequiredFieldError")]
        [StringLength(7, ErrorMessage = "MinMaxTextFieldError", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "TwoFactorCode")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "RememberMachine")]
        public bool RememberMachine { get; set; }

        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }
}
