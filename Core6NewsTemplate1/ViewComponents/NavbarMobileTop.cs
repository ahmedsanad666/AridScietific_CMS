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
    public class NavbarMobileTop : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public NavbarMobileTop(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            NavViewModel NavVM = new NavViewModel()
            {
                NavMenus = _context.NavMenus.Where(n=>n.IsActive&&n.NavMenuPosision== NavMenuPosision.Top),
            };
            return View(NavVM);
        }
    }
}
