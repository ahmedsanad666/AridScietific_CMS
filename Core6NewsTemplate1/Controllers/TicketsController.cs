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
//using Hangfire;
using WebOS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using WebOS.AuxiliaryClasses;
using X.PagedList;

namespace WebOS.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private int PagSize = 100;
        private readonly IEmailSender _emailSender;
        private IWebHostEnvironment _environment;


        public TicketsController(ApplicationDbContext context, UserManager<ApplicationUser> userMrg, IEmailSender emailSender, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userMrg;
            _emailSender = emailSender;
            _environment = environment;

        }

        // GET: Tickets
        public IActionResult Index(int productPage = 1)
        {
            TicketViewModel ticketVM = new TicketViewModel()
            {
                Tickets = _context.Ticket.Where(a => a.ApplicationUserId == _userManager.GetUserId(User)).OrderByDescending(a => a.Id).Include(a => a.ApplicationUser).Include(a => a.TicketCategory),
            };

            var count = ticketVM.Tickets.Count();
            ticketVM.Tickets = ticketVM.Tickets.OrderByDescending(p => p.Id)
                .Skip((productPage - 1) * PagSize)
                .Take(PagSize).ToList();


            //ticketVM.PagingInfo = new PagingInfo
            //{
            //    CurrentPage = productPage,
            //    ItemsPerPage = PagSize,
            //    TotalItem = count
            //};


            return View(ticketVM);


        }

        public IActionResult IndexByTicketCategoryId(int id)
        {
            TicketViewModel ticketVM = new TicketViewModel();

            ticketVM = new TicketViewModel()
            {
                Tickets = _context.Ticket.Where(a => a.Status == true && a.TicketCategoryId == id).OrderByDescending(a => a.TicketOpenDate).Include(a => a.ApplicationUser).Include(a => a.TicketCategory),
            };


            return View(ticketVM);
        }

        public IActionResult List(int ? page,string ss, string closed, string open)
        {
            var numberofpage = page ?? 1;
            closed = closed ?? "false";
            open = open ?? "false";
            var ApplicationDbContext = _context.Ticket.Where(a => a.ApplicationUserId == _userManager.GetUserId(User)).Include(t => t.ApplicationUser).Include(a => a.TicketCategory).OrderByDescending(a => a.Id);
            ViewBag.ticketcategories = _context.TicketCategory;
            List<Ticket> tickets = new List<Ticket>();
            if (closed == "false" && open == "false" && string.IsNullOrEmpty(ss))
            {
                tickets = _context.Ticket.Include(a => a.ApplicationUser).Include(a => a.TicketCategory).ToList();
            }
            else if (closed == "false" && open == "false" && !string.IsNullOrEmpty(ss))
            {
                tickets = _context.Ticket.OrderByDescending(a => a.TicketOpenDate).Include(a => a.TicketCategory).Include(a => a.ApplicationUser).Include(a => a.TicketCategory).Where(a => a.Subject.Contains(ss) || a.Body.Contains(ss) || a.ApplicationUser.ArName.Contains(ss) || a.ApplicationUser.EnName.Contains(ss)).ToList();
            }

            else if (open == "true" && !string.IsNullOrEmpty(ss))
            {
                tickets = _context.Ticket.Where(T => T.Status).OrderByDescending(a => a.TicketOpenDate).Include(a => a.TicketCategory).Include(a => a.ApplicationUser).Include(a => a.TicketCategory).Where(a => a.Subject.Contains(ss) || a.Body.Contains(ss) || a.ApplicationUser.ArName.Contains(ss) || a.ApplicationUser.EnName.Contains(ss)).ToList();
            }
            else if (open == "true" && string.IsNullOrEmpty(ss))
            {
                tickets = _context.Ticket.Where(a => a.Status).Include(a => a.TicketCategory).Include(a => a.ApplicationUser).ToList();
            }
            else if (closed == "true" && !string.IsNullOrEmpty(ss))
            {
                tickets = _context.Ticket.Where(T => !T.Status).OrderByDescending(a => a.TicketOpenDate).Include(a => a.TicketCategory).Include(a => a.ApplicationUser).Include(a => a.TicketCategory).Where(a => a.Subject.Contains(ss) || a.Body.Contains(ss) || a.ApplicationUser.ArName.Contains(ss) || a.ApplicationUser.EnName.Contains(ss)).ToList();
            }
            else if (closed == "true" && string.IsNullOrEmpty(ss))
            {
                tickets = _context.Ticket.Where(a => !a.Status).OrderByDescending(a => a.TicketOpenDate).Include(a => a.ApplicationUser).Include(a => a.TicketCategory).ToList();
            }
            var onepageoftickets = tickets.ToPagedList(numberofpage, 20);
            ViewBag.onepageoftickets = onepageoftickets;
            ViewData["open"] = open;
            ViewData["closed"] = closed;
            ViewData["ss"] = ss;
            return View(tickets);
        }

        // GET: Tickets/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Ticket = _context.Ticket.Where(a => a.ApplicationUserId == _userManager.GetUserId(User) || User.IsInRole("Admins")).SingleOrDefault(m => m.Id == id);
            if (Ticket.Id != 0)
            {
                TicketViewModel ticketVM = new TicketViewModel()
                {
                    TicketReplys = _context.TicketReply.Where(a => a.TicketId == id).OrderByDescending(a => a.Id).Include(a => a.SupportUser),
                    Ticket = _context.Ticket.Where(a => a.ApplicationUserId == _userManager.GetUserId(User) || User.IsInRole("Admins")).Include(t => t.ApplicationUser).SingleOrDefault(m => m.Id == id),
                };
                return View(ticketVM);


            }
            else
                return RedirectToAction(nameof(Index));
        }

        // GET: Tickets/Create
        public IActionResult Create(int id)
        {
            ViewData["TicketCategoryId"] = new SelectList(_context.TicketCategory.Where(a => a.Status == true), "Id", "Name", id);
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject,Body,ApplicationUserId,TicketOpenDate,TicketCloseDate,Status,TicketCategoryId,File")] Ticket ticket, IFormFile myfile1)
        {
            if (ModelState.IsValid)
            {
                ticket.ApplicationUserId = _userManager.GetUserId(User);
                ticket.TicketOpenDate = DateTime.Now;

                if (ticket.Body != null)
                {
                    ticket.Body = (System.Text.RegularExpressions.Regex.Replace(ticket.Body, @"(?></?\w+)(?>(?:[^>'""]+|'[^']*'|""[^""]*"")*)>", String.Empty)).Replace("\n", "<br/>");
                }
                ticket.File = await UserFile.UploadeNewFileAsync(ticket.File,
       myfile1, _environment.WebRootPath, "TicketFiles");
                ticket.Status = true;
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                //-----------------------------------email content-----------------------------------------
                //var applicationuser = _context.ApplicationUsers.SingleOrDefault(a => a.Id == _userManager.GetUserId(User));

                //StringBuilder Welcome = new StringBuilder("<h3 align ='right'>عزيزي ");
                //Welcome.AppendFormat(string.Format(applicationuser.ArName));
                //Welcome.Append("</h3>");
                //Welcome.Append("</br>");

                //StringBuilder Footer = new StringBuilder("<h3 align ='right'>أطيب التحيات ");
                //Welcome.Append("</h3>");
                //Footer.AppendFormat(string.Format("<h3 align ='right'>فريق  أكاديمية الصفا "));
                //Welcome.Append("</h3>");


                //EmailContent usercontent = _context.EmailContents.Where(m => m.UniqueName.ToString() == "510bce84-7b52-454c-8afd-6d6c2fe73204").SingleOrDefault();
                //StringBuilder MyStringBuilder = new StringBuilder("");
                //MyStringBuilder.AppendFormat(string.Format("<h2 align ='center'><a href='https://portal.WebOS.my/ar-LY/Tickets/Details/{0}' >", ticket.Id));
                //MyStringBuilder.Append("اضغط هنا للاطلاع على تفاصيل التذكرة");
                //MyStringBuilder.Append("</a></h2>");

                //BackgroundJob.Schedule(() => _emailSender.SendEmailAsync(applicationuser.Email, usercontent.ArSubject + "- رقم التذكرة #[" + ticket.Id + "]", Welcome.ToString() + usercontent.ArContent + MyStringBuilder + Footer.ToString()), TimeSpan.FromSeconds(3));

                //-----------------------------------email content-----------------------------------------

                if (_context.TicketCategory.SingleOrDefault(a => a.Id == ticket.TicketCategoryId).NotifyEmail != null)
                {
                    //  BackgroundJob.Schedule(() => _emailSender.SendEmailAsync(_context.TicketCategory.SingleOrDefault(a => a.Id == ticket.TicketCategoryId).NotifyEmail, usercontent.ArSubject + "- رقم التذكرة #[" + ticket.Id + "]", Welcome.ToString() + usercontent.ArContent + MyStringBuilder + Footer.ToString()), TimeSpan.FromSeconds(3));
                }
                return RedirectToAction(nameof(Index));
            }

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.SingleOrDefaultAsync(m => m.Id == id);
            ViewData["TicketCategoryId"] = new SelectList(_context.TicketCategory.Where(t=>t.Id==ticket.TicketCategoryId), "Id", "Name", ticket.TicketCategoryId);
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers.Where(a=>a.Id==ticket.ApplicationUserId), "Id", "ArName", ticket.ApplicationUserId);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Subject,Body,ApplicationUserId,TicketOpenDate,TicketCloseDate,Status,TicketCategoryId,File")] Ticket ticket, IFormFile myfile1)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }
            //var ticketitem = await _context.Ticket.SingleOrDefaultAsync(m => m.Id == id);
            if (ModelState.IsValid)
            {
                try
                {
                    ticket.ApplicationUserId = ticket.ApplicationUserId;
                    ticket.TicketOpenDate = DateTime.Now;
                    ticket.TicketCloseDate = DateTime.Now;
                    ticket.Status = true;
                    ticket.File = await UserFile.UploadeNewFileAsync(ticket.File,
             myfile1, _environment.WebRootPath, "TicketFiles");
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            return View(ticket);
        }

        [Authorize(Roles = RoleName.Admins)]
        public async Task<IActionResult> Close(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Id == id & (m.ApplicationUserId == _userManager.GetUserId(User) || User.IsInRole("Admins")));
            if (ticket == null)
            {
                return NotFound();
            }
            ticket.Status = false;
            _context.SaveChanges();

            return RedirectToAction("Index", "Tickets");
        }

        // GET: Tickets/Delete/5
        [Authorize(Roles = RoleName.Admins)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.ApplicationUser)
                .SingleOrDefaultAsync(m => m.Id == id & (m.ApplicationUserId == _userManager.GetUserId(User) || User.IsInRole("Admins")));
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.SingleOrDefaultAsync(m => m.Id == id);
            var ticketreplies = _context.TicketReply.Where(m => m.TicketId == id).ToList();
            _context.TicketReply.RemoveRange(ticketreplies);
            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            UserFile.DeleteOldFile(_environment.WebRootPath, "Secured", ticket.File);
            //UserFile.DeleteOldFile(_environment.WebRootPath, Properties.Resources.Secured, ticketreplies.File);

            return RedirectToAction(nameof(List));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }
    }
}
