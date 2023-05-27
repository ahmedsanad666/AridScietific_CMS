using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class PostMeta
    {
        [Key]
        public Guid Id { get; set; }

        public int PostId { get; set; }
        public BlogPost Post { get; set; }

        [StringLength(50)]
        public string Key { get; set; }

        [StringLength(500)]
        public string Value { get; set; }
    }
}
