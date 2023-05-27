using WebOS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Messages.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        [Display(Name = "العنوان")]
        public string Subject { get; set; }

        [StringLength(5000)]
        [Display(Name = "محتوى الرسالة")]
        public string Content { get; set; }

        [Display(Name = "تاريخ الانشاء")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime DateOfRecord { get; set; }

        [Display(Name = "تاريخ القراءة")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime DateOfRead { get; set; }

        [Display(Name = "تاريخ اخر نشاط")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime LastActivitydate { get; set; }


        [Display(Name = "المرسل")]
        [StringLength(450)]
        public string FromApplicationUserId { get; set; }
        public ApplicationUser FromApplicationUser { get; set; }

        [Display(Name = "المستقبل")]
        [StringLength(450)]
        public string ToApplicationUserId { get; set; }
        public ApplicationUser ToApplicationUser { get; set; }

        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsReported { get; set; }

        [StringLength(50)]
        [Display(Name = "ملف مرفق")]
        public string Attachment { get; set; }
    }
}
