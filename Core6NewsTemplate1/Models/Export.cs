using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static WebOS.Models.Common;

namespace WebOS.Models
{
    public class Export
    {
        public string LectureName { get; set; }
        public string Profile { get; set; }
        public string Name { get; set; }
        public DateTime date { get; set; }
        public string Venue { get; set; }

        [Display(Name = "عنوان المشاركة الاضافية")]
        [StringLength(1000)]
        public string SecondaryTitle { get; set; }

        public int CertId { get; set; }

        [StringLength(100)]
        public string Image { get; set; }

        //Certificate of participation / appreciation / Badge certificate
        [StringLength(500)]
        public string HeaderTitle { get; set; }

        [StringLength(500)]
        public string FirstLine { get; set; }

        //Course, workshop, conference title
        [StringLength(500)]
        public string MainTitle { get; set; }

        [StringLength(500)]
        public string SecondLine { get; set; }

        //optional like invited speaker
        [StringLength(500)]
        public string AlternativePrivilege { get; set; }

        //optional
        [StringLength(500)]
        public string ThirdLine { get; set; }

        [Display(Name = "تاريخ الاضافة")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [StringLength(100)]
        public string WrittenDate { get; set; }

        public string ResearcherId { get; set; }
        public Language Language { get; set; }

    }
}
