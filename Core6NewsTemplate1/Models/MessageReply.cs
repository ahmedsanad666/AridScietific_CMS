using WebOS.Messages.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class MessageReply
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "الرسالة")]
        public int MessageId { get; set; }
        public Message Message { get; set; }

        [Display(Name = "صاحب الرد")]
        [StringLength(450)]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


        [StringLength(5000)]
        [Display(Name = "محتوى الرد")]
        public string Content { get; set; }

        [Display(Name = "تاريخ الانشاء")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime DateOfRecord { get; set; }

        [Display(Name = "تاريخ القراءة")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime DateOfRead { get; set; }

        [Display(Name = "مقروءة")]
        public bool IsRead { get; set; }

        [Display(Name = "محذوفة")]
        public bool IsDeleted { get; set; }

        [Display(Name = "مبلغ عنها")]
        public bool IsReported { get; set; }

        [StringLength(50)]
        [Display(Name = "ملف مرفق")]
        public string Attachment { get; set; }

    }
}
