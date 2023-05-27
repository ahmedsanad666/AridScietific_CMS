using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class BlogPost
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "طول العنوان يجب ان يتراوح بين 25 حرف و 100 حرف", MinimumLength = 25)]
        [Display(Name = "العنوان")]
        public string Title { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "طول العنوان يجب ان يتراوح بين 25 حرف و 100 حرف", MinimumLength = 25)]
        [Display(Name = "العنوان بالانجليزي")]
        public string EnTitle { get; set; }

        [Required]
        [Display(Name = "المحتوى")]
        [StringLength(5000, ErrorMessage = "طول المحتوى يجب ان يتراوح بين 25 حرف و 5000 حرف", MinimumLength = 25)]
        public string Body { get; set; }

        [Required]
        [StringLength(5000, ErrorMessage = "طول المحتوى بالانجليزي يجب ان يتراوح بين 25 حرف و 5000 حرف", MinimumLength = 25)]
        [Display(Name = "المحتوى بالانجليزي")]
        public string EnBody { get; set; }

        [StringLength(1000, ErrorMessage = "طول المقدمة يجب ان يتراوح بين 25 حرف و 1000 حرف", MinimumLength = 25)]
        [Display(Name = "نبذة مختصرة")]
        public string Introduction { get; set; }

        [StringLength(1000, ErrorMessage = "طول المقدمة يجب ان يتراوح بين 25 حرف و 1000 حرف", MinimumLength = 25)]
        [Display(Name = "نبذة مختصرة بالانجليزي")]
        public string EnIntroduction { get; set; }

        [Display(Name = "تاريخ النشر")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime PostDateTime { get; set; }

        [Display(Name = "تاريخ التعديل")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateModified { get; set; }

        [StringLength(100)]
        [Display(Name = "الصورة")]
        public string Image { get; set; }

        [StringLength(100)]
        [Display(Name = "فيديو يوتيوب")]
        public string YouTube { get; set; }

        [StringLength(30, ErrorMessage = "الكاتب يجب ان يتراوح بين 2 حرف و 30 حرف", MinimumLength = 2)]
        [Display(Name = "الكاتب/المصدر")]
        public string Source { get; set; }

        [StringLength(30, ErrorMessage = "الكاتب يجب ان يتراوح بين 2 حرف و 30 حرف", MinimumLength = 2)]
        [Display(Name = "الكاتب/المصدر بالانجليزي")]
        public string EnSource { get; set; }

        [StringLength(100)]
        [Display(Name = "الملف المرفق")]
        public string File { get; set; }

        [Display(Name = "اخفاء المنشور")]
        public bool IsHidden { get; set; }

        [Display(Name = "مميز")]
        public bool IsFeatured { get; set; }

        [Display(Name = "عرض pdf")]
        public bool IsPdf { get; set; }

        [Display(Name = "عدد القراء")]
        public int Reads { get; set; }

        [Display(Name = "حذف المنشور")]
        public bool IsDeleted { get; set; }

        [Display(Name = "الناشر")]
        [StringLength(450)]
        public string ApplicationUserId { get; set; }
        [Display(Name = "الناشر")]
        public ApplicationUser ApplicationUser { get; set; }

        [StringLength(100, ErrorMessage = "الكلمات المفتاحية يجب ان تكون بين 100 و 5 احرف", MinimumLength = 5)]
        [Display(Name = "كلمات مفتاحية")]
        public string Tags { get; set; }

        [StringLength(100, ErrorMessage = "الكلمات المفتاحية يجب ان تكون بين 100 و 5 احرف", MinimumLength = 5)]
        [Display(Name = "كلمات مفتاحية بالانجليزي")]
        public string EnTags { get; set; }

        [Display(Name = "القسم")]
        public int BlogTaxonomyId { get; set; }
        [Display(Name = "القسم")]
        public BlogTaxonomy BlogTaxonomy { get; set; }



    }
}
