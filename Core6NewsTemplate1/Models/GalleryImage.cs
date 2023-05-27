using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class GalleryImage
    {
        [Key]
        public int Id { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Url { get; set; }

        public int GalleryCategoryId { get; set; }
        public GalleryCategory GalleryCategory { get; set; }

    }
}
