namespace WebOS.Models
{
    using System.ComponentModel.DataAnnotations;
    public class BlogPostImage
    {
        public int Id { get; set; }

        [Display(Name = "المنشور")]
        public Guid BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }

        [StringLength(100)]
        [Display(Name = "الصورة")]
        public string Image { get; set; }

    }
}
