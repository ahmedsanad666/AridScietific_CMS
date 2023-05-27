using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebOS.Models
{
    public class BlockItem
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="ال block")]
        public int BlockId { get; set; }
        public Block Block { get; set; }

        [StringLength(100, ErrorMessage = "هذاالحقل يجب أن يتراوح بين 1 الى 100 حرف")]
        [Required]
        [Display(Name = "العنوان")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "الصورة")]
        public string Image { get; set; }


        [StringLength(100, ErrorMessage = "هذاالحقل يجب أن يتراوح بين 1 الى 100 حرف")]
        [Required]
        [Display(Name = "الرابط")]
        public string link { get; set; }

    }
}
