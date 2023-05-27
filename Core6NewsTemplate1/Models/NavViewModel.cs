using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class NavViewModel
    {
        public SystemSettings SystemSettings { get; set; }
        public NavMenu NavMenu { get; set; }
        public NavMenuItem NavMenuItem { get; set; }
        public NavMenuItemSub NavMenuItemSub { get; set; }
        public BlockItem BlockItem { get; set; }

        public IEnumerable<BlogPost> MostReadPosts;
        public IEnumerable<BlogPost> SliderBlogPosts;
        public IEnumerable<BlogTaxonomy> BlogTaxonomies;
        public IEnumerable<NavMenu> NavMenus;
        public IEnumerable<NavMenuItem> NavMenuItems;
        public IEnumerable<NavMenuItemSub> NavMenuItemSubs;
    }
}
