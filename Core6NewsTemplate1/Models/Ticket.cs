using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "العنوان")]
        [StringLength(500)]
        [Required(ErrorMessage = "وضع موضوع للتذكرة ضروري ومطلوب")]
        public string Subject { get; set; }

        [Display(Name = "المحتوى")]
        [Required(ErrorMessage = "محتوى التذكرة مطلوب")]
        public string Body { get; set; }

        [Display(Name = "المستخدم")]
        [StringLength(450)]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "تاريخ التذكرة")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime TicketOpenDate { get; set; }

        [Display(Name = "تاريخ الاغلاق")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime TicketCloseDate { get; set; }

        [Display(Name = "حالة التذكرة")]
        public bool Status { get; set; }

        [Display(Name = "القسم")]
        public int TicketCategoryId { get; set; }
        [Display(Name = "القسم")]
        public TicketCategory TicketCategory { get; set; }

             [StringLength(100)]
        [Display(Name = "الملف المرفق")]
        public string File { get; set; }

    }
}
