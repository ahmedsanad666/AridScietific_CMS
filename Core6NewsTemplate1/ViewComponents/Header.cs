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
    public class Header : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public Header(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            NavViewModel NavVM = new NavViewModel()
            {

                //MostReadPosts = _context.BlogPost.OrderBy(b => b.Reads).Take(3),
                //BlogTaxonomies = _context.BlogTaxonomy.Take(5),
                SystemSettings=_context.SystemSettings.FirstOrDefault(),

            };
            return View(NavVM);
        }
    }
}
