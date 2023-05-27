using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebOS.Models
{
    public class Page
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "العنوان")]
        [StringLength(300)]
        public string Title { get; set; }

        [Display(Name = "العنوان بالانجليزي")]
        [StringLength(300)]
        public string EnTitle { get; set; }

        [Display(Name = "المحتوى")]
        public string Body { get; set; }

        [Display(Name = "المحتوى بالانجليزي")]
        public string EnBody { get; set; }

        [StringLength(500)]
        public string PageName { get; set; }

        [Display(Name = "تاريخ الانشاء")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime DateOfRecord { get; set; }

    }
}
