using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Introduction { get; set; }
        public string Body { get; set; }
        public DateTime Date_published { get; set; }
        public int CategoryId { get; set; }
        public int SectionId { get; set; }
        public int NewsReader { get; set; }
        public string ImageLink { get; set; }
        public string MetaDescription { get; set; }
    }
}
