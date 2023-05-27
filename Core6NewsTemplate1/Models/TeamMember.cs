namespace WebOS.Models
{
    using System.ComponentModel.DataAnnotations;
    public class TeamMember
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30,MinimumLength =5,ErrorMessage = "عدد الحروف يجب ان يكون بين 30 و 5 حرف")]
        [Display(Name = "الاسم (بين 30 و 5 حرف)")]
        public string Name { get; set; }

        [StringLength(30,MinimumLength =5,ErrorMessage = "عدد الحروف يجب ان يكون بين 30 و 5 حرف")]
        [Display(Name = "الاسم بالانجليزي (بين 30 و 5 حرف)")]
        public string EnName { get; set; }

        [StringLength(100, MinimumLength = 20, ErrorMessage = "عدد الحروف يجب ان يكون بين 100 و 20 حرف")]
        [Display(Name = "نبذة صغيرة (بين 100 و 20 حرف)")]
        public string BriefDescription { get; set; }

        [StringLength(100, MinimumLength = 20, ErrorMessage = "عدد الحروف يجب ان يكون بين 100 و 20 حرف")]
        [Display(Name = "نبذة صغيرة بالانجليزي (بين 100 و 20 حرف)")]
        public string EnBriefDescription { get; set; }

        [StringLength(40, MinimumLength = 3, ErrorMessage = "عدد الحروف يجب ان يكون بين 20 و 3 حرف")]
        [Display(Name = "الوظيفة (بين 40 و 3 حرف)")]
        public string Job { get; set; }

        [StringLength(40, MinimumLength = 3, ErrorMessage = "عدد الحروف يجب ان يكون بين 20 و 3 حرف")]
        [Display(Name = "الوظيفة بالانجليزي (بين 40 و 3 حرف)")]
        public string EnJob { get; set; }

        [StringLength(100)]
        [Display(Name = "الصورة")]
        public string Image { get; set; }

        [Display(Name = "فعالة؟")]
        public bool IsActive { get; set; }


        [StringLength(50)]
        [Display(Name = "فيسبوك")]
        public string FB { get; set; }

        [StringLength(50)]
        [Display(Name = "تويتر")]
        public string Twitter { get; set; }

        [StringLength(50)]
        [Display(Name = "Linkedin")]
        public string Linkedin { get; set; }

        [StringLength(50)]
        [Display(Name = "Youtube")]
        public string Youtube { get; set; }

        [StringLength(50)]
        [Display(Name = "Instagram")]
        public string Instagram { get; set; }


    }
}
