using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebOS.Data;
using WebOS.Messages.Models;
using Microsoft.AspNetCore.Identity;
using WebOS.Models;
using Microsoft.AspNetCore.Http;
using WebOS.AuxiliaryClasses;
using Microsoft.AspNetCore.Hosting;
//using Hangfire;
using WebOS.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebOS.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _environment;
        private readonly IEmailSender _emailSender;


        private UserManager<ApplicationUser> _userManager;

        public MessagesController(IWebHostEnvironment environment, ApplicationDbContext context, UserManager<ApplicationUser> userMrg, IEmailSender emailSender)
        {
            _context = context;
            _environment = environment;
            _userManager = userMrg;
            _emailSender = emailSender;
        }

        // GET: Messages
        public IActionResult Index(string id, int currentmessageid, string ss,string num)
        {

            if (id != _userManager.GetUserId(User) && !User.IsInRole(RoleName.Admins))
            {
                return NotFound();
            }
            MessageViewModel messageVM = new MessageViewModel();
            ViewData["currentmessageid"] = currentmessageid;

            var messages = _context.Messages.Include(m => m.FromApplicationUser).Include(m => m.ToApplicationUser).Where(m => m.FromApplicationUserId == id || m.ToApplicationUserId == id).OrderByDescending(m => m.Id);
            var messagereplies = _context.MessageReplies.Include(m => m.Message).Include(m => m.ApplicationUser).Where(m => m.MessageId != 0 && messages.Where(e => e.Id == m.MessageId).Any());
            var Num = 10;

            if (num != null)
            {
                Num = Int16.Parse(num);
                ViewData["sentorinbox"] = num;
                Num +=10;
            }
            if (string.IsNullOrEmpty(ss))
            {
                ViewData["ss"] = "";
                messageVM = new MessageViewModel()
                {
                    //FromApplicationUsers=_context.Messages.
                    //FromApplicationUsers = fromapplicationusers,
                    InboxMessages = _context.Messages.Include(m => m.FromApplicationUser).Include(m => m.ToApplicationUser).Where(m => m.ToApplicationUserId == id).OrderByDescending(m => m.Id).Take(Num),
                    InboxMessagesCount = _context.Messages.Include(m => m.FromApplicationUser).Include(m => m.ToApplicationUser).Where(m => m.ToApplicationUserId == id).OrderByDescending(m => m.Id).Count(),
                    SentMessages = _context.Messages.Include(m => m.FromApplicationUser).Include(m => m.ToApplicationUser).Where(m => m.FromApplicationUserId == id).OrderByDescending(m => m.Id).Take(Num),
                    SentMessagesCount = _context.Messages.Include(m => m.FromApplicationUser).Include(m => m.ToApplicationUser).Where(m => m.FromApplicationUserId == id).OrderByDescending(m => m.Id).Count(),
                    messageReplies = _context.MessageReplies.Include(m => m.Message).Include(m => m.ApplicationUser).Where(m => m.MessageId != 0 && messages.Where(e => e.Id == m.MessageId).Any()),
                    Messages = _context.Messages.Include(m => m.FromApplicationUser).Include(m => m.ToApplicationUser).Where(m => m.FromApplicationUserId == id || m.ToApplicationUserId == id).OrderByDescending(m => m.Id).Take(Num*2),                   //var noDistinct = items.GroupBy(x => x.Prop1).All(x => x.Count() == 1);
                };
            }
            else if (!string.IsNullOrEmpty(ss))
            {
                ViewData["ss"] = ss;
                messageVM = new MessageViewModel()
                {
                    //FromApplicationUsers=_context.Messages.
                    //FromApplicationUsers = fromapplicationusers,
                    InboxMessages = _context.Messages.Include(m => m.FromApplicationUser).Include(m => m.ToApplicationUser).Where(m => m.ToApplicationUserId == id).OrderByDescending(m => m.Id).Where(m => m.ToApplicationUser.ArName.Contains(ss) || m.FromApplicationUser.ArName.Contains(ss) || m.Attachment.Contains(ss) || m.Content.Contains(ss) || m.Subject.Contains(ss) || _context.MessageReplies.Where(n => n.MessageId == m.Id).Where(n => n.Content.Contains(ss) || n.Attachment.Contains(ss)).Any()).Take(Num),
                    InboxMessagesCount = _context.Messages.Include(m => m.FromApplicationUser).Include(m => m.ToApplicationUser).Where(m => m.ToApplicationUserId == id).OrderByDescending(m => m.Id).Where(m => m.ToApplicationUser.ArName.Contains(ss) || m.FromApplicationUser.ArName.Contains(ss) || m.Attachment.Contains(ss) || m.Content.Contains(ss) || m.Subject.Contains(ss) || _context.MessageReplies.Where(n => n.MessageId == m.Id).Where(n => n.Content.Contains(ss) || n.Attachment.Contains(ss)).Any()).Count(),
                    SentMessages = _context.Messages.Include(m => m.FromApplicationUser).Include(m => m.ToApplicationUser).Where(m => m.FromApplicationUserId == id).OrderByDescending(m => m.Id).Where(m => m.ToApplicationUser.ArName.Contains(ss) || m.FromApplicationUser.ArName.Contains(ss) || m.Attachment.Contains(ss) || m.Content.Contains(ss) || m.Subject.Contains(ss) || _context.MessageReplies.Where(n => n.MessageId == m.Id).Where(n => n.Content.Contains(ss) || n.Attachment.Contains(ss)).Any()).Take(Num),
                    SentMessagesCount = _context.Messages.Include(m => m.FromApplicationUser).Include(m => m.ToApplicationUser).Where(m => m.FromApplicationUserId == id).OrderByDescending(m => m.Id).Where(m => m.ToApplicationUser.ArName.Contains(ss) || m.FromApplicationUser.ArName.Contains(ss) || m.Attachment.Contains(ss) || m.Content.Contains(ss) || m.Subject.Contains(ss) || _context.MessageReplies.Where(n => n.MessageId == m.Id).Where(n => n.Content.Contains(ss) || n.Attachment.Contains(ss)).Any()).Count(),
                    messageReplies = _context.MessageReplies.Include(m => m.Message).Include(m => m.ApplicationUser).Where(m => m.MessageId != 0 && messages.Where(e => e.Id == m.MessageId).Any()),
                    Messages = _context.Messages.Include(m => m.FromApplicationUser).Include(m => m.ToApplicationUser).Where(m => m.FromApplicationUserId == id || m.ToApplicationUserId == id).OrderByDescending(m => m.Id).Where(m => m.ToApplicationUser.ArName.Contains(ss) || m.FromApplicationUser.ArName.Contains(ss) || m.Attachment.Contains(ss) || m.Content.Contains(ss) || m.Subject.Contains(ss) || _context.MessageReplies.Where(n => n.MessageId == m.Id).Where(n => n.Content.Contains(ss) || n.Attachment.Contains(ss)).Any()).Take(Num*2),
                    //var noDistinct = items.GroupBy(x => x.Prop1).All(x => x.Count() == 1);
                };
                //_context.SearchLog.Add(new SearchLog
                //{
                //    ApplicationUserId = _userManager.GetUserId(User),
                //    Date = DateTime.Now,
                //    Keyword = ss,
                //    SearchLocation = SearchLocation.Messeges
                //});
                _context.SaveChanges();
            }   



            var x = _context.Messages.Include(m => m.FromApplicationUser).Include(m => m.ToApplicationUser).Where(m => m.ToApplicationUserId == id || m.FromApplicationUserId == id).GroupBy(m => m.FromApplicationUserId).All(m => m.Count() == 1);
            return View(messageVM);
        }
        [Authorize(Roles =RoleName.Admins)]
        public IActionResult GetUser(string q)
        {
            IEnumerable<ApplicationUser> applicationusers;
             applicationusers = _context.ApplicationUsers.Where(a => a.ArName.Contains(q) || a.EnName.Contains(q) || a.Email.Contains(q));
            return View(applicationusers);
        }


        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ValidateMessage = _context.Messages.SingleOrDefault(m => m.Id == id && (m.ToApplicationUserId == _userManager.GetUserId(User) || m.FromApplicationUserId == _userManager.GetUserId(User)));

            if (ValidateMessage == null)
            {
                return NotFound();
            }
            var message = _context.Messages.SingleOrDefault(m => m.Id == id && m.ToApplicationUserId == _userManager.GetUserId(User));
            var messagereplies = _context.MessageReplies.Where(m => m.MessageId == id && m.ApplicationUserId != _userManager.GetUserId(User));
            if (message != null)
            {
                message.IsRead = true;
                message.DateOfRead = DateTime.Now;
                _context.Messages.Update(message);
            }

            if (messagereplies != null)
            {
                foreach (var item in messagereplies)
                {
                    item.IsRead = true;
                    item.DateOfRead = DateTime.Now;
                }
                _context.MessageReplies.UpdateRange(messagereplies);
            }
            _context.SaveChanges();

            MessageViewModel messageVM = new MessageViewModel()
            {
                Message = await _context.Messages
                .Include(m => m.FromApplicationUser)
                .Include(m => m.ToApplicationUser)
                .SingleOrDefaultAsync(m => m.Id == id),

                messageReplies = _context.MessageReplies.Where(m => m.MessageId == id).OrderBy(m => m.DateOfRecord),
            };


            return View(messageVM);
        }
        // GET: Messages/Details/5
        public IActionResult Test()
        {

            return View();
        }

        // GET: Messages/Create
        public IActionResult Create(string SearchString)
        {
            MessageViewModel MessageViewModel = new MessageViewModel()
            {
                //ApplicationUsers = _context.ApplicationUsers.Where(a => a.ArName.Contains(SearchString) || a.EnName.Contains(SearchString) || a.Email.Contains(SearchString) || a.Id.Contains(SearchString) || a.UserName.Contains(SearchString)),

            };
            //ViewData["FromApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            //ViewData["ToApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View(MessageViewModel);
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject,Content,DateOfRecord,DateOfRead,FromApplicationUserId,ToApplicationUserId,IsRead,IsDeleted,IsReported,Attachment")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.DateOfRecord = DateTime.Now;
                message.FromApplicationUserId = _userManager.GetUserId(User);
                message.IsDeleted = false;
                message.IsRead = false;
                message.IsReported = false;
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FromApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", message.FromApplicationUserId);
            ViewData["ToApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", message.ToApplicationUserId);
            return View(message);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.SingleOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["FromApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", message.FromApplicationUserId);
            ViewData["ToApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", message.ToApplicationUserId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Subject,Content,DateOfRecord,DateOfRead,FromApplicationUserId,ToApplicationUserId,IsRead,IsDeleted,IsReported,Attachment")] Message message)
        {
            if (id != message.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FromApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", message.FromApplicationUserId);
            ViewData["ToApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", message.ToApplicationUserId);
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.FromApplicationUser)
                .Include(m => m.ToApplicationUser)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _context.Messages.SingleOrDefaultAsync(m => m.Id == id);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public JsonResult GetReplies(int id)
        {
            var messagesreplies = _context.MessageReplies.Include(m => m.Message).Where(m => m.MessageId == id);
            //ViewData["Count"] = _context.PostMetric.Count(a => a.PostId == id);
            return Json(new { list = messagesreplies });
        }

        public async Task<IActionResult> AddReply(IFormFile file, string reply, int messageid)
        {
            Message message = _context.Messages.SingleOrDefault(m => m.Id == messageid);
            if (messageid != 0)
            {
                message.LastActivitydate = DateTime.Now;
                message.IsRead = false;
                _context.Messages.Update(message);
            }
            if (file != null)
            {
                _context.MessageReplies.Add(new MessageReply
                {
                    ApplicationUserId = _userManager.GetUserId(User),
                    Content = reply.Replace("\n", "<br/>"),
                    Attachment = await UserFile.UploadeNewFileAsync("", file, _environment.WebRootPath, Properties.Resources.Secured),
                    MessageId = messageid,
                    DateOfRecord = DateTime.Now,
                    IsRead = false,
                    IsDeleted = false,
                    IsReported = false,
                });
            }
            else
            {
                _context.MessageReplies.Add(new MessageReply
                {
                    ApplicationUserId = _userManager.GetUserId(User),
                    Content = reply.Replace("\n", "<br/>"),
                    MessageId = messageid,
                    DateOfRecord = DateTime.Now,
                    IsRead = false,
                    IsDeleted = false,
                    IsReported = false,
                });
            }
            _context.SaveChanges();

            //ApplicationUser currentUser = _context.ApplicationUsers.SingleOrDefault(a => a.Id == message.ToApplicationUserId);
            //StringBuilder Welcome = new StringBuilder("<h3 align ='right'>سعادة ");
            //Welcome.AppendFormat(string.Format(currentUser.ArName));
            //Welcome.Append("</h3>");
            //Welcome.Append("</br>");

            //StringBuilder Footer = new StringBuilder("<h3 align ='right'>أطيب التحيات</h3>");
            //Footer.Append("<h3 align ='right'>فريق  أكاديمية الصفا");

            //Footer.Append("</h3>");

            //EmailContent content2 = _context.EmailContents.Where(m => m.UniqueName.ToString() == "3a6ac0b2-304f-49f0-8db8-87a11f76e63a").SingleOrDefault();
            ////BackgroundJob.Schedule(() => _emailSender.SendEmailAsync(email, content2.ArSubject, content2.ArContent), TimeSpan.FromSeconds(1));


            ////         StringBuilder MyStringBuilder = new StringBuilder("");
            ////MyStringBuilder.AppendFormat(string.Format("<h2 align ='center'><a href='https://portal.WebOS.my/ar-LY/Account/Register/' >"));
            ////MyStringBuilder.Append("اضغط هنا للتسجيل في منصة اريد ");
            ////MyStringBuilder.Append("</a></h2>");

            //string link = "https://portal.WebOS.my/ar-LY/Account/DAL/" + currentUser.DAL;

            //BackgroundJob.Schedule(() => _emailSender.SendEmailAsync(currentUser.Email, "رد جديد:" + message.Subject, Welcome.ToString() + content2.ArContent +
            //       $"<center><b><a href='{HtmlEncoder.Default.Encode(link)}'>رابط الدخول المباشر الى حسابكم في منصة أُريد </a> </p></b></center> <br/>" + Footer.ToString()), TimeSpan.FromSeconds(3));


            return RedirectToAction("Index", "Messages", new { id = messageid });
        }
        public async Task<IActionResult> AddReplyDetails(IFormFile file, string reply, int mid)
        {
            Message message = _context.Messages.SingleOrDefault(m => m.Id == mid);
            message.LastActivitydate = DateTime.Now;
            message.IsRead = false;
            _context.Messages.Update(message);
            _context.MessageReplies.Add(new MessageReply
            {
                ApplicationUserId = _userManager.GetUserId(User),
                Content = reply.Replace("\n", "<br/>"),
                Attachment = await UserFile.UploadeNewFileAsync("", file, _environment.WebRootPath, Properties.Resources.Secured),
                MessageId = mid,
                DateOfRecord = DateTime.Now,
                IsRead = false,
                IsDeleted = false,
                IsReported = false,
            });
            _context.SaveChanges();

            return RedirectToAction("Details", "Messages", new { id = mid });
        }
        public IActionResult IsRead(int mid, string cuid)
        {
            var message = _context.Messages.SingleOrDefault(m => m.Id == mid && m.ToApplicationUserId == cuid);
            var messagereplies = _context.MessageReplies.Where(m => m.MessageId == mid && m.ApplicationUserId != cuid);
            if (message != null)
            {
                message.IsRead = true;
                message.DateOfRead = DateTime.Now;
                _context.Messages.Update(message);
            }
            if (messagereplies != null)
            {
                foreach (var item in messagereplies)
                {
                    item.IsRead = true;
                    item.DateOfRead = DateTime.Now;
                }
                _context.MessageReplies.UpdateRange(messagereplies);
            }
            _context.SaveChanges();
            return RedirectToAction("Details", "FreelancerProjects", new { id = mid });

        }

        public async Task<IActionResult> AddFile(IFormFile file)
        {
            string upload =await UserFile.UploadeNewFileAsync("",file,_environment.WebRootPath,Properties.Resources.Files);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult DeleteFile(string file)
        {
            UserFile.DeleteOldFile(_environment.WebRootPath, Properties.Resources.Files , file);
            return RedirectToAction(nameof(Details));
        }


        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}
