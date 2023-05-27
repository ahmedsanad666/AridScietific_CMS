using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebOS.Data;
using WebOS.Models;
using Microsoft.AspNetCore.Identity;
using System.Text;
using WebOS.Services;
using Microsoft.AspNetCore.Authorization;
using WebOS.AuxiliaryClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace WebOS.Controllers
{
    [Authorize]
    public class TicketRepliesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
                private IWebHostEnvironment _environment;


        public TicketRepliesController(ApplicationDbContext context, UserManager<ApplicationUser> userMrg, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender,IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userMrg;
            _signInManager = signInManager;
            _emailSender = emailSender;
                        _environment = environment;

        }

        // GET: TicketReplies
        public async Task<IActionResult> Index(int? tid)
        {
            var ApplicationDbContext = _context.TicketReply.Include(t => t.SupportUser).Where(a => a.TicketId == tid).OrderBy(a => a.Id);
            ViewData["Subject"] = _context.Ticket.SingleOrDefault(a => a.Id == tid).Subject;
            ViewData["Body"] = _context.Ticket.SingleOrDefault(a => a.Id == tid).Body;
            ViewData["Id"] = tid;
            return View(await ApplicationDbContext.ToListAsync());
        }

        // GET: TicketReplies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketReply = await _context.TicketReply
                .Include(t => t.SupportUser)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ticketReply == null)
            {
                return NotFound();
            }

            return View(ticketReply);
        }

        // GET: TicketReplies/Create
        public IActionResult Create(int tid)
        {
            ViewData["TicketId"] = new SelectList(_context.Set<Ticket>().Where(a => a.Id == tid).OrderByDescending(a => a.Id), "Id", "Subject");
            return View();
        }

        // POST: TicketReplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int tid, [Bind("Id,Body,SupportUserId,TicketId,ReplyDate,File")] TicketReply ticketReply,IFormFile myfile1)
        {
            if (ModelState.IsValid)
            {

                var ticket = await _context.Ticket.Include(a=>a.ApplicationUser)
                         .SingleOrDefaultAsync(m => m.Id == ticketReply.TicketId);
              
                if (User.IsInRole("Admins"))
                {
                    ticket.Status = false;
                    ticket.TicketCloseDate = DateTime.Now;
                    var applicationuser = _context.ApplicationUsers.SingleOrDefault(a => a.Id == ticket.ApplicationUserId);
                    _context.Update(ticket);

                    //-----------------------------------email content-----------------------------------------

                    //StringBuilder Welcome = new StringBuilder("<h3 align ='right'>عزيزي ");
                    //Welcome.AppendFormat(string.Format(applicationuser.ArName));
                    //Welcome.Append("</h3>");
                    //Welcome.Append("</br>");

                    //StringBuilder Footer = new StringBuilder("<h3 align ='right'>أطيب التحيات ");
                    //Welcome.Append("</h3>");
                    //Footer.AppendFormat(string.Format("<h3 align ='right'>فريق  أكاديمية الصفا "));

                    //Welcome.Append("</h3>");

                    //EmailContent usercontent = _context.EmailContents.Where(m => m.UniqueName.ToString() == "8725ae1e-bff6-4a96-8be7-0a020f99b780").SingleOrDefault();
                    //StringBuilder MyStringBuilder = new StringBuilder("");
                    //MyStringBuilder.Append("<p align ='right'>" + ticketReply.Body + "</p>");
                    //MyStringBuilder.Append("<br/>");
                    //MyStringBuilder.AppendFormat(string.Format("<h2 align ='center'><a href='https://portal.WebOS.my/ar-LY/Tickets/Details/{0}' >", ticket.Id));
                    //MyStringBuilder.Append("اضغط هنا للاطلاع على تفاصيل التذكرة والرد عليها");
                    //MyStringBuilder.Append("</a></h2>");


                    //BackgroundJob.Schedule(() => _emailSender.SendEmailAsync(applicationuser.Email, "الرد على استفساركم في منصة اريد - رقم التذكرة " + "#[" + ticket.Id + "]", Welcome.ToString() + usercontent.ArContent + MyStringBuilder.ToString() + Footer.ToString()), TimeSpan.FromSeconds(1));
                   
                    //-----------------------------------email content-----------------------------------------

                }

                else
                {
                    ticket.Status = true;
                  
                    _context.Update(ticket);
                }
                if (ticketReply.Body != null)
                {
                    ticketReply.Body = (System.Text.RegularExpressions.Regex.Replace(ticketReply.Body, @"(?></?\w+)(?>(?:[^>'""]+|'[^']*'|""[^""]*"")*)>", String.Empty)).Replace("\n", "<br/>");
                }
                ticketReply.ReplyDate = DateTime.Now;
                ticketReply.SupportUserId = _userManager.GetUserId(User);
                ticketReply.File = await UserFile.UploadeNewFileAsync(ticketReply.File,
     myfile1, _environment.WebRootPath, "TicketFiles");
                _context.Add(ticketReply);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", "Tickets",new { id=ticketReply.TicketId});
            }
            ViewData["TicketId"] = new SelectList(_context.Set<Ticket>().Where(a => a.Id == tid).OrderByDescending(a => a.Id), "Id", "Subject");
            return View(ticketReply);
        }

        [Authorize(Roles = RoleName.Admins)]
        // GET: TicketReplies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketReply = await _context.TicketReply.SingleOrDefaultAsync(m => m.Id == id);
            if (ticketReply == null)
            {
                return NotFound();
            }
            ViewData["SupportUserId"] = new SelectList(_context.ApplicationUsers.Where(a => a.Id == _userManager.GetUserId(User)), "Id", "Id", ticketReply.SupportUserId);
            return View(ticketReply);
        }

        // POST: TicketReplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Body,SupportUserId,TicketId,ReplyDate")] TicketReply ticketReply)
        {
            if (id != ticketReply.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketReply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketReplyExists(ticketReply.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction( "Details","Tickets" , new {id=ticketReply.TicketId });
            }
            ViewData["SupportUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", ticketReply.SupportUserId);
            return View(ticketReply);
        }

        // GET: TicketReplies/Delete/
        [Authorize(Roles = RoleName.Admins)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketReply = await _context.TicketReply
                .Include(t => t.SupportUser)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ticketReply == null)
            {
                return NotFound();
            }

            return View(ticketReply);
        }

        // POST: TicketReplies/Delete/5
        [Authorize(RoleName.Admins)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketReply = await _context.TicketReply.SingleOrDefaultAsync(m => m.Id == id);
            _context.TicketReply.Remove(ticketReply);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Tickets", new { id = ticketReply.TicketId });

        }

        private bool TicketReplyExists(int id)
        {
            return _context.TicketReply.Any(e => e.Id == id);
        }
    }
}
