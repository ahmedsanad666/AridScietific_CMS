using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.Models
{
    public class BlogViewModel
    {
        public BlogPost BlogPost { get; set; }
        public BlogPost PostForReading { get; set; }
        public BlogTaxonomy BlogTaxonomy { get; set; }
        public SystemSettings SystemSettings { get; set; }
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<BlogPost> LastPosts { get; set; }
        public IEnumerable<BlogPost> MostReadPosts { get; set; }
        public IEnumerable<BlogPost> SliderBlogPosts { get; set; }
        public IEnumerable<BlogPost> FirstCategoryPosts { get; set; }
        public IEnumerable<BlogPost> SeconedCategoryPosts { get; set; }
        public IEnumerable<BlogPostImage> BlogPostImages { get; set; }
        public IEnumerable<BlogPost> ThirdCategoryPosts { get; set; }
        public IEnumerable<BlogPost> FourthCategoryPosts { get; set; }
        public IEnumerable<BlogPost> FifthCategoryPosts { get; set; }
        public IEnumerable<BlogPost> SixthCategoryPosts { get; set; }
        public IEnumerable<BlogPost> SeventhCategoryPosts { get; set; }
        public IEnumerable<BlogPost> EighthCategoryPosts { get; set; }
        public IEnumerable<BlogPost> NinthCategoryPosts { get; set; }
        public IEnumerable<NavMenu> NavMenus { get; set; }
        public IEnumerable<NavMenuItem>  NavMenuItems { get; set; }
        public IEnumerable<NavMenuItemSub> NavMenuItemSubs { get; set; }
        public IEnumerable<BlogPost> FeaturedBlogPosts { get; set; }
        public IEnumerable<BlogPost> FeaturedBlogPostsOthers { get; set; }
        public IEnumerable<BlogPost> AllBlogPosts { get; set; }
        public IEnumerable<BlogTaxonomy> BlogTaxonomies { get; set; }
        public IEnumerable<BlogPostComment> BlogPostComments { get; set; }
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<HomeCard> HomeCards { get; set; }
        public IEnumerable<Block> Blocks { get; set; }
        public IEnumerable<BlockItem> BlockItems { get; set; }
      
     


        public int BlogPostsCount { get; set; }
    }
}
