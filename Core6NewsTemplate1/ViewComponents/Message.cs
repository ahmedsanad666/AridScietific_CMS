using WebOS.Data;
using WebOS.Models;
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
//using Hangfire;
using System.Text.Encodings.Web;
using System.Security.Claims;

namespace WebOS.ViewComponents
{
    public class Message : ViewComponent
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public Message(RoleManager<IdentityRole> roleMgr,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleMgr;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            MessageViewModel messageVM = new MessageViewModel()
            {
                Messages = _context.Messages.Include(m => m.FromApplicationUser).Include(m => m.ToApplicationUser).Where(m => m.ToApplicationUserId == _userManager.GetUserId(Request.HttpContext.User) || m.FromApplicationUserId == _userManager.GetUserId(Request.HttpContext.User)).OrderByDescending(m => m.LastActivitydate).Take(5),
                messageReplies = _context.MessageReplies.Include(m => m.Message).Where(m => m.ApplicationUserId != _userManager.GetUserId(Request.HttpContext.User) && m.IsRead == false).Where(m => m.Message.ToApplicationUserId == _userManager.GetUserId(Request.HttpContext.User) || m.Message.FromApplicationUserId == _userManager.GetUserId(Request.HttpContext.User)),
                AllMessageReplies = _context.MessageReplies.Include(m => m.Message).Where(m => m.Message.ToApplicationUserId == _userManager.GetUserId(Request.HttpContext.User) || m.Message.FromApplicationUserId == _userManager.GetUserId(Request.HttpContext.User)),
                CurrentUser = _context.ApplicationUsers.SingleOrDefault(a => a.Id == _userManager.GetUserId(Request.HttpContext.User)),
            };
            //var messagesreplies = _context.MessageReplies.Include(m => m.Message.FromApplicationUser).Include(m => m.Message.ToApplicationUser).Where(m => m.ApplicationUserId != _userManager.GetUserId(Request.HttpContext.User) && m.IsRead == false && (m.Message.ToApplicationUserId == _userManager.GetUserId(Request.HttpContext.User) || m.Message.FromApplicationUserId == _userManager.GetUserId(Request.HttpContext.User)));
            //var messages = _context.Messages.Include(m => m.FromApplicationUser).Where(m => m.ToApplicationUserId == _userManager.GetUserId(Request.HttpContext.User)).Take(7).OrderByDescending(m => m.Id).ToList();

            //foreach (var item in messagesreplies)
            //{
            //    item.Message.Content = "s";
            //}

            //foreach (var item in messagesreplies)
            //{
            //    if (messages.Where(m => m.Id == item.MessageId).Count() == 0)
            //    {
            //        messages.Add(item.Message);
            //    }
            //}
            ////return Json(new { list = messages.OrderByDescending(m => m.LastActivitydate) });
            return View(messageVM);
        }


    }
}
