using System;
using System.ComponentModel.DataAnnotations;

namespace ARID.Models
{
    public class CalendarEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }


        [Display(Name = "تاريخ البدء ")]
        [DataType(DataType.DateTime)]
        public DateTime StartingDate { get; set; }

        [Display(Name = "تاريخ الانتهاء ")]
        [DataType(DataType.DateTime)]
        public DateTime EndingDate { get; set; }

        [Display(Name = "القسم")]
        public int CalendarEventCategoryId { get; set; }
        public CalenderEventCategory CalenderEventCategory { get; set; }

    }
}
