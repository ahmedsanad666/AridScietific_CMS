using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static WebOS.Models.Common;

namespace WebOS.Models
{
    public class NavMenu
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="الإسم")]
        [StringLength(20,ErrorMessage ="طول الإسم يجب أن يتراوح بين 2 الى 20 حرف"),MinLength(2)]
        public string Name { get; set; }
        
        [Display(Name ="الإسم بالانجليزي")]
        [StringLength(20,ErrorMessage ="طول الإسم يجب أن يتراوح بين 2 الى 20 حرف"),MinLength(2)]
        public string EnName { get; set; }

        [Required]
        [Display(Name = "الرابط")]
        [StringLength(100, ErrorMessage = "طول الإسم يجب أن يتراوح بين 2 الى 100 حرف"), MinLength(2)]
        public string Url { get; set; }

        [Display(Name = "الرمز")]
        [StringLength(20, ErrorMessage = "طول الإسم يجب أن يتراوح بين 2 الى 20 حرف"), MinLength(2)]
        public string Icon { get; set; }

        [Display(Name = "الموقع")]
        public NavMenuPosision NavMenuPosision { get; set; }
        //[Required]
        //[Display(Name = "العملية")]
        //[StringLength(50, ErrorMessage = "طول الإسم يجب أن يتراوح بين 2 الى 50 حرف"), MinLength(2)]
        //public string Action { get; set; }

        //[Required]
        //[Display(Name ="الموجه")]
        //[StringLength(50,ErrorMessage ="طول الإسم يجب أن يتراوح بين 2 الى 50 حرف"),MinLength(2)]
        //public string Controller { get; set; }

        [Required]
        [Display(Name = "الترتيب")]
        public int indx { get; set; }


        [Display(Name = "فعالة؟")]
        public bool IsActive { get; set; }
    }
}
