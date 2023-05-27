using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebOS.Data;
using WebOS.Models;
using static WebOS.Models.Common;

namespace WebOS.ViewComponents
{
    public class FlashNews : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public FlashNews(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            NavViewModel NavVM = new NavViewModel()
            {
                SliderBlogPosts = _context.BlogPost.Where(b => b.BlogTaxonomyId == 1).OrderByDescending(a => a.PostDateTime).Take(5),
            };
            return View(NavVM);
        }
    }
}
