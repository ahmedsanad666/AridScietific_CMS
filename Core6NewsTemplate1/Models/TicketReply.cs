using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class TicketReply
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "الرد")]
        [StringLength(1000)]
        public string Body { get; set; }

        [Display(Name = "المستخدم")]
        [StringLength(450)]
        public string SupportUserId { get; set; }
        [Display(Name = "المستخدم")]
        public  ApplicationUser SupportUser { get; set; }

        [Display(Name = "التذكرة")]
        public int TicketId { get; set; }
        public  Ticket Ticket { get; set; }

        [Display(Name = "تاريخ الرد")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ReplyDate { get; set; }

        [StringLength(100)]
        [Display(Name = "الملف المرفق")]
        public string File { get; set; }
    }
}
