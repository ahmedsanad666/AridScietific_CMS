using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class ApplicationUserViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }

        public IEnumerable<BlogPost>  BlogPosts{ get; set; }
    }
}
