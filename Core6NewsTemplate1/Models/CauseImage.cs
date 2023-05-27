namespace WebOS.Models
{
    using System.ComponentModel.DataAnnotations;
    public class CauseImage
    {
        public int Id { get; set; }

        [Display(Name = "المشروع")]
        public int CauseId { get; set; }
        public Cause Cause { get; set; }

        [StringLength(100)]
        [Display(Name = "الصورة")]
        public string Image { get; set; }
    }
}
