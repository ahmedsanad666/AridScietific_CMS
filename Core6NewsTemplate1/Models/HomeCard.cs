using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebOS.Models
{
    public class HomeCard
    {
        [Key]
        public int Id { get; set; }


        [StringLength(20, ErrorMessage = "طول الاسم يجب ان يتراوح بين 1 و 20 حرف")]
        [Display(Name = "الإسم")]
        [Required]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "الوصف")]
        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "طول الاسم يجب ان يتراوح بين 1 و 20 حرف")]
        [Display(Name = "الصورة")]
        public string Image { get; set; }


        [Required]
        [Display(Name = "التسلسل")]
        public int indx { get; set; }

        [Required]
        [Url]
        [Display(Name = "الرابط")]
        [StringLength(100)]
        public string Url { get; set; }

        [Required]
        [Display(Name = "نص الرابط")]
        [StringLength(100)]
        public string UrlText { get; set; }

        [Display(Name = "الحالة")]
        public bool Status { get; set; }
    }
}
