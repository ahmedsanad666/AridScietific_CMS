using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using static WebOS.Models.Common;

namespace WebOS.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser :IdentityUser
    {
        [StringLength(14)]
        [Display(Name = "رقم الحساب")]
        public string Account { get; set; }

        [StringLength(4)]
        [Display(Name = "رمز للدعم الفني")]
        public string SupportPin { get; set; }

        [Display(Name = "الاشتراك بالقائمة البريدية")]
        public bool IsNewsletter { get; set; }

        [Display(Name = "نوع الحساب")]
        [Required(ErrorMessage = "RequiredFieldError")]
        public AccountType AccountType { get; set; }

        [Display(Name = "تاريخ التسجيل")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "الرصيد")]
        public decimal Balance { get; set; }

        [Display(Name = "نقاط الولاء")]
        public int IncentivePoints { get; set; }

        [StringLength(500)]
        [Display(Name = "الصورة الشخصية")]
        public string ImageProfile { get; set; }

        [Display(Name = "الجنسية")]
        //[Required(ErrorMessage = "RequiredFieldError")]
        public int NationalityId { get; set; }
        public Country Nationality { get; set; }

        [Display(Name = "البلد")]
        [DefaultValue(1)]
        //[Required(ErrorMessage = "RequiredFieldError")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        [Display(Name = "المدينة")]
        //[Required(ErrorMessage = "RequiredFieldError")]
        [DefaultValue(1)]
        public int CityId { get; set; }
        public City City { get; set; }

        [Display(Name = "الاسم العربي")]
        [StringLength(50, ErrorMessage = "الاسم قصير", MinimumLength = 6)]
        public string ArName { get; set; }

        [Display(Name = "الاسم بالانجليزي")]
        [StringLength(50, ErrorMessage = "الاسم قصير", MinimumLength = 6)]
        public string EnName { get; set; }

        [StringLength(450)]
        [Display(Name = "مسجل بواسطة")]
        public string ReferredById { get; set; }
        public virtual ApplicationUser ReferredBy { get; set; }

        [Display(Name = "تلأريخ الولادة")]
        [Required(ErrorMessage = "RequiredFieldError")]
        [DataType(DataType.Date)]
        public DateTime DateofBirth { get; set; }

        [Display(Name = "الجنس")]
        [Required(ErrorMessage = "RequiredFieldError")]
        [DefaultValue(GenderType.Unknown)]
        public GenderType Gender { get; set; }

        [Display(Name = "بريد الكتروني ثانوي")]
        [EmailAddress(ErrorMessage = "EmailError")]
        [StringLength(100)]
        public string SecondEmail { get; set; }

        [Display(Name = "الحالة")]
        [DefaultValue(StatusType.New)]
        public StatusType Status { get; set; }

        [Display(Name = "Job")]
        [DefaultValue(JobName.Other)]
        public JobName JobId { get; set; }

        [Display(Name = "رابط الدخول المباشر")]
        [StringLength(450)]
        public string DAL { get; set; }

        [Display(Name = "لغة الواجهة")]
        [Required(ErrorMessage = "RequiredFieldError")]
        public Language UILanguage { get; set; }

        [Display(Name = "تفعيل رابط الدخول المباشر")]
        //[Required(ErrorMessage = "RequiredFieldError")]
        public bool DALEnabled { get; set; }
    }
}
