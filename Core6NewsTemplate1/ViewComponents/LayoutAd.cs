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
    public class LayoutAd : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public LayoutAd(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var rand = new Random();
            IEnumerable<BlockItem> blockitems = _context.BlockItem.Where(a => a.BlockId == 1);
            NavViewModel NavVM = new NavViewModel()
            {
                BlockItem = blockitems.ElementAt(rand.Next(blockitems.Count())),
            };
            return View(NavVM);
        }



    }
}
