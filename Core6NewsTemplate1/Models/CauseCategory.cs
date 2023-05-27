namespace WebOS.Models
{
    using System.ComponentModel.DataAnnotations;
    public class CauseCategory
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "يجب ان يكون بين 5 و 100 حرف")]
        [Display(Name = "العنوان")]
        public string Title { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "يجب ان يكون بين 5 و 100 حرف")]
        [Display(Name = "العنوان بالانجليزي")]
        public string EnTitle { get; set; }

        [StringLength(200, MinimumLength = 5, ErrorMessage = "يجب ان يكون بين 5 و 200 حرف")]
        [Display(Name = "الوصف")]
        public string Description { get; set; }

        [StringLength(200, MinimumLength = 5, ErrorMessage = "يجب ان يكون بين 5 و 200 حرف")]
        [Display(Name = "الوصف بالانجليزي")]
        public string EnDescription { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "يجب ان يكون بين 5 و 100 حرف")]
        [Display(Name = "الصورة")]
        public string Image { get; set; }
    }
}
