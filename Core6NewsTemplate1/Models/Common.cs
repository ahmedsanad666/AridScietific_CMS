using WebOS.Controllers;
//using FluentEmail.Core;
//using FluentEmail.Mailgun;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Reflection;

namespace WebOS.Models
{


    public class Common
    {
        public static string WEBOSFormat(int a)
        {
            string ss = a.ToString("D8");
            ss = string.Format("{0}-{1}", ss.Substring(0, 4), ss.Substring(4, 4));
            return ss;
        }

        public static string GetYoutubeIframe(string youtubelink)
        {
            string s;
            if (youtubelink.Contains("&"))
            {
                s = youtubelink.Substring(youtubelink.IndexOf("v=") + 2, youtubelink.IndexOf("&") - youtubelink.IndexOf("v=") - 2);
            }
            else{
                s = youtubelink.Substring(youtubelink.IndexOf("v=") + 2);
            }
            return "https://www.youtube.com/embed/"+s;

                
        }

        public static string substringc(string ss, int firstindx, int lastindx)
        {
            string returnedstring;
            if (ss.Length - 1 < lastindx)
            {
                returnedstring = ss;
            }
            else
            {
                returnedstring = ss.Substring(firstindx, lastindx) + "...";
            }

            return (returnedstring);
        }
        public static string GetRemainingTime(DateTime yourDate)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(yourDate.Ticks - DateTime.Now.Ticks);
            double delta = ts.TotalSeconds;
            if (delta < 1)
                return "إنتهى الوقت";

            if (delta < 2 * MINUTE)
                return "دقيقة ";

            if (delta < 45 * MINUTE)
                return "" + ts.Minutes + " دقيقة";

            if (delta < 90 * MINUTE)
                return "ساعة";

            if (delta < 24 * HOUR)
                return "" + ts.Hours + " ساعة";

            if (delta < 48 * HOUR)
                return "يوم";

            if (delta < 30 * DAY)
                return "" + ts.Days + "يوم";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "شهر" : "" + months + " شهر";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "سنة" : "" + years + " سنة";
            }
        }
        public static string GetDate(DateTime yourDate)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - yourDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "الان" : "منذ " + ts.Seconds + " ث";

            if (delta < 2 * MINUTE)
                return "منذ دقيقة ";

            if (delta < 45 * MINUTE)
                return "منذ " + ts.Minutes + " د";

            if (delta < 90 * MINUTE)
                return "منذ ساعة";

            if (delta < 24 * HOUR)
                return "منذ " + ts.Hours + " ساعة";

            if (delta < 48 * HOUR)
                return "منذ الأمس";

            if (delta < 30 * DAY)
                return "منذ " + ts.Days + " يوم";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "منذ شهر" : "منذ " + months + " شهر";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "منذ سنة" : "منذ " + years + " سنة";
            }
        }




        public enum JobName
        {
            [Display(Name = "غير محدد")]
            Other = 0,
            [Display(Name = "موظف")]
            Employee = 1,
            [Display(Name = "مدير")]
            Manager = 2,
            [Display(Name = "مستقل")]
            Freelancer = 3,
            [Display(Name = "طالب")]
            Student = 4,
            [Display(Name = "عاطل")]
            Idle = 5,
            [Display(Name = "تاجر")]
            Merchant = 6
        };

        public enum ResellerStatus
        {
            [Display(Name = "قيد الموافقة")]
            UnderProcessing = 0,
            [Display(Name = "موافقة")]
            Approved = 1,
            [Display(Name = "مرفوض")]
            Rejected = 2,
          
        };
        public enum BalanceType
        {
            [Display(Name = "الرصيد المؤقت")]
            holdingbalance=1, 
            [Display(Name = "الرصيد الحالي")]
             currentbalance = 2,
        };

        public static string GetDisplayName(Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }

        public enum NavMenuPosision
        {
            [Display(Name = "الاعلى")]
            Top=1, 
            //[Display(Name = "الوسط")]
            // Middle = 2,
            [Display(Name = "السفلي")]
             Footer = 3,
            //[Display(Name = "Mobile")]
            // Mobile = 4,
            //[Display(Name = "SideMobile")]
            //SideMobile = 5
        };

        public enum CoreTechnology
        {
            [Display(Name = "Asp.Net Core")]
            AspNetCore = 1,
            [Display(Name = "Asp.Net")]
            AspNet = 2,
            [Display(Name = "Asp")]
            Asp = 3,
            [Display(Name = "PHP")]
            PHP = 4,
            [Display(Name = "Python")]
            Python = 5,
            [Display(Name = "Ruby on Rail")]
            Ruby = 6,
            [Display(Name = "Java")]
            Java = 7,
            [Display(Name = "Others")]
            Others = 0,

        };
        


        public enum VerificationStatus
        {
            [Display(Name = "جديد")]
            New = 0,
            [Display(Name = "محقق")]
            Verified = 1,
            [Display(Name = "رفض")]
            Rejected = 2
                  };

        public enum DocumentType
        {
            [Display(Name = "اخرى")]
            Other = 0,
            [Display(Name = "الجواز")]
            Passport = 1,
            [Display(Name = "اجازة السوق")]
            DrivingLicense = 2,
            [Display(Name = "فاتورة")]
            Bill = 3,
            [Display(Name = "الجواز مع الصورة")]
            PassportWithFace = 4,
            [Display(Name = "الهوية الوطنية")]
            PersonalId = 5
        };


        


        public enum ContactType
        {
            [Display(Name = "أخرى")]
            Other = 0,
            [Display(Name = "البريد الالكتروني")]
            Email = 1,
            [Display(Name = "الاتصال")]
            Call = 2,
            [Display(Name = "محادثة نصية")]
            Chat = 3,
            [Display(Name = "لقاء")]
            Meeting = 4
                        };

        


             public enum MerchantCategory
        {
            [Display(Name = "Others")]
            Others = 0,
            [Display(Name = "Gaming &amp; MMORPGs")]
            Gaming = 1,
            [Display(Name = "Booking, Entertainment &amp; Lifestyle")]
            BookingEntertainmentLifeStype = 2,
            [Display(Name = "Communications &amp; VoIP")]
            CommunicationsVoip = 3,
            [Display(Name = "Trading &amp; Business")]
            Trading = 4,
            [Display(Name = "IT &amp; Web Hosting Services")]
            ITWebHosting = 5,
            [Display(Name = "Online Shopping")]
            OnlineSHopping = 6,
            [Display(Name = "Social Media Networks")]
            SocialMediaNetworks = 7
        };

        public enum SecurityQuestion
        {
            //Case 1
            //    lbQuestion.Text = "Your mother's maiden name?"

            //Case 2
            //    lbQuestion.Text = "Your favorite football team"

            //Case 3
            //    lbQuestion.Text = "The last school you attended"

            //Case 4
            //    lbQuestion.Text = "The name of your first school"

            //Case 5
            //    lbQuestion.Text = "The first car or bile's make"

            //Case 6
            //    lbQuestion.Text = "Your father's middle name"

            //Case 7
            //    lbQuestion.Text = "Where you first met your spouse"

            //Case 8
            //    lbQuestion.Text = "Your pet's name"

            //Case 9
            //    lbQuestion.Text = "Your favorite food"

            //Case 10
            //    lbQuestion.Text = "Your city of birth"

            //Case 11
            //    lbQuestion.Text = "Your exact time of birth"

            //Case 12
            //    lbQuestion.Text = "Your childhood hero"


        };

        

        public enum Language
        {
            [Display(Name = "العربية")]
            Arabic = 0,
            [Display(Name = "الانجليزية")]
            English = 1,
            [Display(Name = "الماليزية")]
            Malay = 2,
            [Display(Name = "التركية")]
            Turkish = 3,
            [Display(Name = "الفارسية")]
            Persian = 4
        };

        public enum ResellereType
        {
            [Display(Name = "أُونلاين")]
            Online = 0,
            [Display(Name = "مكتب")]
            Office = 1,
        };

        public enum GenderType
        {
            [Display(Name = "غير محدد")]
            Unknown = 0,
            [Display(Name = "أنثى")]
            Female = 1,
            [Display(Name = "ذكر")]
            Male = 2,
        };

        public enum StatusType
        {
            [Display(Name = "New")]
            New = 0,
            [Display(Name = "Active")]
            Active = 1,
            [Display(Name = "Inactive")]
            Inactive = 2,
            [Display(Name = "Suspended")]
            Suspended = 3,
            [Display(Name = "Suspicious")]
            Suspicious = 4
        };

    
        internal static void SendEmailAsync()
        {
            throw new NotImplementedException();
        }

     

        public enum AccountType
        {
            [Display(Name = "أولي")] //new System users who just started to learn potential of FilsPay World
            Alias = 0,

            [Display(Name = "رسمي")] //active payment systems users and freelancers
            Formal = 1,

            [Display(Name = "مبدئي")] //small business and internet companies employees
            Initial = 2,

            [Display(Name = "احترافي")] //business and advanced System users 
            professional = 3
        };


        public enum CertificateType
        {
            [Display(Name = "لا توجد")]
            None = 0,
            [Display(Name = "شهادة مدفوعة الثمن")]
            Paid = 1,
            [Display(Name = "شهادة مجانية")]
            Free = 2,
        };
        public enum CertificateTemplateFolder
        {
            [Display(Name = "PortraitCourse")]
            PortraitCourse = 0,
        }

        public enum ContentType
        {
            [Display(Name = "مقطع فيديو (فيميو) ")]
            vimeo = 0,
            [Display(Name = "مقطع فيديو (يوتيوب)")]
            youtube = 1,
            [Display(Name = "ملف")]
            file = 2,
            [Display(Name = "رابط")]
            website = 3,
            [Display(Name = "مقطع فيديو (loom) ")]
            loom = 4,
        };

        public enum Courier
        {
            [Display(Name = "PostLaju")]
            PostLaju = 1,
            [Display(Name = "Gdex")]
            Gdex = 2,
        };

        public enum CourseAccessType
        {
            [Display(Name = "Paid")]
            Paid = 0,
            [Display(Name = "Grant")]
            Grant = 1,
            [Display(Name = "DuePayment")]
            DuePayment = 2
        };

        public enum BriefLanguage
        {
            [Display(Name = "اللغة العربية")]
            Arabic = 0,
            [Display(Name = "اللغة الإنجليزية")]
            English = 1,
        }

        public enum ContentAlignment
        {
            [Display(Name = "اليمين")]
            right = 0,
            [Display(Name = "اليسار")]
            left = 1,
            [Display(Name = "منتصف")]
            center = 2,
        };

        public enum BlockLocation
        {
            [Display(Name = "التذييل")]
            footer = 0,
            //[Display(Name = "اليسار")]
            //left = 1,
            //[Display(Name = "اليمين")]
            //right = 2,
        };

        public enum ProjectStatus
        {
            [Display(Name = "مشروع جديد")]
            New = 0,
            [Display(Name = "بدا الاستقبال")]
            Initiated = 1,
            [Display(Name = "اكتمل")]
            Completed = 2,
        };

    }
}
