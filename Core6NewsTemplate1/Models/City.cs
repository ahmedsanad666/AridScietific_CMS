using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebOS.Models
{
    public partial class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "إسم المدينة بالعربي")]
        public string ArCityName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "إسم المدينة بالإنجليزي")]
        public string EnCityName { get; set; }

        [Required]
        [Display(Name = "البلد")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }      

          
    }
}