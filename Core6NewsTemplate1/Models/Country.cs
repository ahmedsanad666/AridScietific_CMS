using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebOS.Models
{
    public partial class Country
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="الإٍسم بالعربي")]
        public string ArCountryName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "الإٍسم بالانجليزي")]
        public string EnCountryName { get; set; }

        [StringLength(10)]
        [Display(Name = "رمز البلد")]
        public string CountryCode { get; set; }

        [StringLength(5, MinimumLength =2)]
        [Display(Name = "الإسم المصغر")]
        public string ShortName { get; set; }

        [StringLength(500)]
        [Display(Name ="العلم")]
        public string Flag { get; set; }

     
    }
}