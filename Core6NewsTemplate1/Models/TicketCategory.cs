using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class TicketCategory
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Display(Name = "القسم")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "الايميل الذي يرسل له الاشعار")]
        public string NotifyEmail { get; set; }

        [Display(Name= "إشعاري بالإيميل")]
        public bool Status { get; set; }
    }
}
