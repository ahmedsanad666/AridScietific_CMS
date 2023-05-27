using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class BlogTaxonomy
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "اسم القسم ")]
        [StringLength(100, ErrorMessage = "طول المحتوى يجب ان يتراوح بين 3 حرف و 100 حرف", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "اسم القسم بالانجليزي ")]
        [StringLength(100, ErrorMessage = "طول المحتوى يجب ان يتراوح بين 3 حرف و 100 حرف", MinimumLength = 3)]
        public string EnName { get; set; }

        [Display(Name = "القسم الفرعي")]
        public int Sub { get; set; }

        [Display(Name = "مميز")]
        public bool IsFeatured { get; set; }

    }
}
