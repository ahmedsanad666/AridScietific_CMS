using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "المحتوى")]
        [StringLength(500)]
        public string Content { get; set; }

        [Display(Name = "الرابط")]
        [StringLength(100)]
        public string Url { get; set; }

        [Display(Name = "تاريخ الإضافة")]
        [DataType(DataType.Date, ErrorMessage = "DateError")]
        public DateTime CreationDate { get; set; }

        [StringLength(450)]
        [Display(Name = "المستخدم")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [StringLength(450)]
        [Display(Name = "المستخدم")]
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        [Display(Name = "مقروءة")]
        public bool IsRead { get; set; }

        [Display(Name = "محذوفة")]
        public bool IsDeleted { get; set; }

        [Display(Name = "تأريخ القراءة")]
        [DataType(DataType.Date, ErrorMessage = "DateError")]
        public DateTime ReadDate { get; set; }

    }
}
