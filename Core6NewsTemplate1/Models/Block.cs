using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static WebOS.Models.Common;

namespace WebOS.Models
{
    public class Block
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "هذاالحقل يجب أن يتراوح بين 1 الى 100 حرف")]
        [Required]
        [Display(Name = "الإسم")]
        public string Name { get; set; }
        public string secondName { get; set; }

        [Display(Name = "الموقع")]
        public BlockLocation BlockLocation { get; set; }

        [Display(Name = "الحالة")]
        public bool status { get; set; }

    }
}
