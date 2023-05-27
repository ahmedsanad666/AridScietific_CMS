using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static WebOS.Models.Common;

namespace WebOS.Models
{
    public class Statement
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "تاريخ الإضافة")]
        //[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date, ErrorMessage = "DateError")]
        public DateTime RecordDate { get; set; }

        [Display(Name = "التفاصيل")]
        [StringLength(450)]
        public string Details { get; set; } //(CardId ,  ServiceId , ToUserId)

        [Display(Name = "المبلغ")]
        public decimal Amount { get; set; }

        [Display(Name = "من/الى")]
        public bool Destination { get; set; }

        [Display(Name = " المستخدم")]
        [StringLength(450)]
        public string ApplicationUserId { get; set; }
        [Display(Name = " المستخدم")]
        public ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "نوع الرصيد")]
        public BalanceType BalanceType { get; set; } //1 holding balance, 2 = current balance
    }
}
