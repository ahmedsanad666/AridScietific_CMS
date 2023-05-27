using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebOS.Data;
using WebOS.Models;

namespace WebOS.ViewComponents
{
    public class DynamicNav : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public DynamicNav(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            NavViewModel NavVM = new NavViewModel()
            {
                NavMenuItemSubs = _context.NavMenuItemSub.Where(n => n.IsActive),
                NavMenuItems = _context.NavMenuItems.Where(n => n.IsActive),
                NavMenus = _context.NavMenus.Where(n=>n.IsActive).OrderBy(a=>a.indx),
            };
            return View(NavVM);
        }



    }
}
