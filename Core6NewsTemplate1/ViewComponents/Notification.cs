using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using WebOS.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Security.Claims;
using WebOS.Models;
using WebOS.Data;

namespace WebOS.ViewComponents
{
    public class Notification:ViewComponent
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        



        public Notification(RoleManager<IdentityRole> roleMgr,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleMgr;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            //varnotifications = _context.Notifications.Include(m => m.ApplicationUser).Include(m => m.Sender).OrderByDescending(m => m.Id);

            return View(await _context.Notification.OrderByDescending(n=>n.CreationDate).Where(n=>n.ApplicationUserId==_userManager.GetUserId(Request.HttpContext.User)).Take(5).ToListAsync());
        }


    }
}
