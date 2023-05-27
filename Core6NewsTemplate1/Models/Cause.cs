namespace WebOS.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Cause
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100,MinimumLength =5,ErrorMessage ="يجب ان يكون بين 5 و 100 حرف")]
        [Display(Name = "العنوان (بين 100 و 5 حرف)")]
        public string Title { get; set; }

        [StringLength(100,MinimumLength =5,ErrorMessage ="يجب ان يكون بين 5 و 100 حرف")]
        [Display(Name = "العنوان بالانجليزية (بين 100 و 5 حرف)")]
        public string EnTitle { get; set; }

        [StringLength(10, MinimumLength = 5, ErrorMessage = "يجب ان يكون بين 3 و 10 حرف")]
        [Display(Name = "المجال (بين 10 و 3 حرف)")]
        public string Field { get; set; }

        [StringLength(10, MinimumLength = 5, ErrorMessage = "يجب ان يكون بين 3 و 10 حرف")]
        [Display(Name = "المجال بالانجليزية (بين 10 و 3 حرف)")]
        public string EnField { get; set; }

        [StringLength(500, MinimumLength = 20, ErrorMessage = "عدد الحروف يجب ان يكون بين 500 و 20 حرف")]
        [Display(Name = "نبذة صغيرة (بين 500 و 20 حرف)")]
        public string BriefDescription { get; set; }

        [StringLength(500, MinimumLength = 20, ErrorMessage = "عدد الحروف يجب ان يكون بين 500 و 20 حرف")]
        [Display(Name = "نبذة صغيرة بالانجليزي (بين 500 و 20 حرف)")]
        public string EnBriefDescription { get; set; }

        [StringLength(2000, MinimumLength = 20, ErrorMessage = "عدد الحروف يجب ان يكون بين 2000 و 20 حرف")]
        [Display(Name = "نص طويل (بين 2000 و 20 حرف)")]
        public string Body { get; set; }

        [StringLength(2000, MinimumLength = 20, ErrorMessage = "عدد الحروف يجب ان يكون بين 2000 و 20 حرف")]
        [Display(Name = "نص طويل بالانجليزي (بين 2000 و 20 حرف)")]
        public string EnBody { get; set; }

        [StringLength(100)]
        [Display(Name = "الصورة")]
        public string Image { get; set; }

        [Display(Name = "فعالة؟")]
        public bool IsActive { get; set; }

        [Display(Name = "القسم")]
        public int CauseCategoryId { get; set; }
        [Display(Name = "القسم")]
        public CauseCategory CauseCategory { get; set; }

        [Display(Name = "تاريخ النشر")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime PostDateTime { get; set; }


    }
}
