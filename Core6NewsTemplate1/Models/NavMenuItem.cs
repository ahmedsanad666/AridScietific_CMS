using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace WebOS.Models
{
    public class NavMenuItem
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "القسم")]
        public int NavMenuId { get; set; }
        public NavMenu NavMenu { get; set; }


        [Required]
        [Display(Name = "الرابط")]
        [StringLength(50, ErrorMessage = "طول الإسم يجب أن يتراوح بين 2 الى 50 حرف"), MinLength(2)]
        public string Url { get; set; }

        [Display(Name = "الرمز")]
        [StringLength(20, ErrorMessage = "طول الإسم يجب أن يتراوح بين 2 الى 20 حرف"), MinLength(2)]
        public string Icon { get; set; }

        [Display(Name = "الإسم")]
        [StringLength(50, ErrorMessage = "طول الإسم يجب أن يتراوح بين 2 الى 20 حرف"), MinLength(2)]
        public string Name { get; set; }

        [Display(Name = "الإسم بالانجليزي")]
        [StringLength(50, ErrorMessage = "طول الإسم يجب أن يتراوح بين 2 الى 20 حرف"), MinLength(2)]
        public string EnName { get; set; }

        [Display(Name = "الترتيب")]
        public int indx { get; set; }

        [Display(Name = "فعالة؟")]
        public bool IsActive { get; set; }

    }
}
