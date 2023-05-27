using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebOS.Models;
using WebOS.Data;
using static WebOS.Models.Common;

namespace WebOS.ViewComponents
{
    public class LayoutFooter : ViewComponent
    {
        private ApplicationDbContext _context;
        public LayoutFooter(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            BlocksViewModel blocksVM = new BlocksViewModel()
            {
                BlockItems = _context.BlockItem.Where(b=>b.Block.BlockLocation== BlockLocation.footer),
                Blocks=_context.Block.Where(c=>c.BlockLocation==BlockLocation.footer)
            };
            return View(blocksVM);
        }

    }
}
