namespace WebOS.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Display(Name = "العنوان (بين 100 و 20 حرف)")]
        public string Title { get; set; }

        [StringLength(100)]
        [Display(Name = "العنوان بالانجليزي (بين 100 و 20 حرف)")]
        public string EnTitle { get; set; }

        [StringLength(500, MinimumLength = 20, ErrorMessage = "عدد الحروف يجب ان يكون بين 2000 و 20 حرف")]
        [Display(Name = "نبذة صغيرة (بين 500 و 20 حرف)")]
        public string BriefDescription { get; set; }

        [StringLength(500, MinimumLength = 20, ErrorMessage = "عدد الحروف يجب ان يكون بين 2000 و 20 حرف")]
        [Display(Name = "نبذة صغيرة بالانجليزي (بين 500 و 20 حرف)")]
        public string EnBriefDescription { get; set; }

        [StringLength(2000,MinimumLength =20,ErrorMessage ="عدد الحروف يجب ان يكون بين 2000 و 20 حرف")]
        [Display(Name = "نص طويل (بين 2000 و 20 حرف)")]
        public string Body { get; set; }

        [StringLength(2000,MinimumLength =20,ErrorMessage ="عدد الحروف يجب ان يكون بين 2000 و 20 حرف")]
        [Display(Name = "نص طويل بالانجليزي (بين 2000 و 20 حرف)")]
        public string EnBody { get; set; }

        [StringLength(100)]
        [Display(Name = "الصورة")]
        public string Image { get; set; }

        [Display(Name = "فعالة؟")]
        public bool IsActive { get; set; }


    }
}
