using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebOS.Models
{
    public class BlogPostComment
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(2000, ErrorMessage = "عدد كلمات التعليق يجب ان تتراوح بين 5 - 400 كلمة")]
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "التعليق")]
        public string Comment { get; set; }

        [Display(Name = "تاريخ النشر")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateTime { get; set; }

        [Display(Name = "اخفاء التعليق")]
        public bool IsHidden { get; set; }

        [Display(Name = "مميز")]
        public bool IsFeatured { get; set; }

        [Display(Name = "حذف التعليق")]
        public bool IsDeleted { get; set; }

        [Display(Name = "الموضوع")]
        public Guid BlogPostId { get; set; }
        [Display(Name = "الموضوع")]
        public BlogPost BlogPost { get; set; }

        [Display(Name = "اسم المستخدم")]
        public string UserName { get; set; }

        [StringLength(100)]
        [Display(Name = "البريد الالكتروني للمستخدم")]
        public string Email { get; set; }

    }
}
