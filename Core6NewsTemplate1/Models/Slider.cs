using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WebOS.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace WebOS.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="العنوان")]
        [StringLength(30,ErrorMessage ="العنوان يجب ان يكون بين 0 الى 30 حرف")]
        public string Title { get; set; }

        [Display(Name ="العنوان بالانجليزي بين 5  و 30 حرف")]
        [StringLength(30,ErrorMessage ="العنوان يجب ان يكون بين 0 الى 30 حرف")]
        public string EnTitle { get; set; }

        [Display(Name = "نبذة مختصرة")]
        [StringLength(60,MinimumLength =3, ErrorMessage = "نبذة مختصرة يجب ان يكون بين 0 الى 60 حرف")]
        public string SubTitle { get; set; }

        [Display(Name = "نبذة مختصرة بالانجليزي")]
        [StringLength(60,MinimumLength =3, ErrorMessage = "نبذة مختصرة بالانجليزي يجب ان يكون بين 0 الى 60 حرف")]
        public string EnSubTitle { get; set; }


        [StringLength(100)]
        [Display(Name = "الصورة")]
        public string Image { get; set; }

        [Display(Name = "التسلسل")]
        public int indx { get; set; }


        [Display(Name = "تاريخ الاضافة")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime AddDate { get; set; }

        [Display(Name = "موقع النص")]
        public ContentAlignment ContentAlignment { get; set; }

        [Display(Name = "الرابط")]
        [StringLength(100)]
        public string Url { get; set; }

        [Display(Name = "نص الرابط")]
        [StringLength(30,MinimumLength =5,ErrorMessage ="نص الرابط يجب ان يكون بين 5 و30 حرف")]
        public string UrlText { get; set; }

        [Display(Name = "نص الرابط بالانجليزي")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "نص الرابط بالانجليزي يجب ان يكون بين 5 و30 حرف")]
        public string EnUrlText { get; set; }

        [Display(Name = "الحالة")]
        public bool Status { get; set; }


    }
}
