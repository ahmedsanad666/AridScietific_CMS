using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebOS.AuxiliaryClasses;
using WebOS.Data;
using WebOS.Models;
using X.PagedList;

namespace WebOS.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signinmanager;
        private UserManager<ApplicationUser> _usermanager;
        private IWebHostEnvironment _environment;
        private IEmailSender _emailSender;

        public ApplicationUsersController(ApplicationDbContext context, SignInManager<ApplicationUser> signinmamger, UserManager<ApplicationUser> usermanager, IWebHostEnvironment environment, IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _environment = environment;
            _usermanager = usermanager;
            _signinmanager = signinmamger;
            _context = context;
        }
        public async Task<IActionResult> CreateMessage(IFormFile file, string message, string title, string appuser)
        {
            ApplicationUser currentUser = _context.ApplicationUsers.SingleOrDefault(a => a.Id == appuser);
            if (file != null)
            {
                _context.Messages.Add(new Messages.Models.Message
                {
                    Content = message,
                    Subject = title,
                    Attachment = await UserFile.UploadeNewFileAsync("", file, _environment.WebRootPath, Properties.Resources.Secured),
                    FromApplicationUserId = _usermanager.GetUserId(User),
                    DateOfRecord = DateTime.Now,
                    ToApplicationUserId = appuser,
                    IsDeleted = false,
                    IsRead = false,
                    IsReported = false,
                    LastActivitydate = DateTime.Now,
                });
            }
            else
            {
                _context.Messages.Add(new Messages.Models.Message
                {
                    Content = message,
                    Subject = title,
                    FromApplicationUserId = _usermanager.GetUserId(User),
                    DateOfRecord = DateTime.Now,
                    ToApplicationUserId = appuser,
                    IsDeleted = false,
                    IsRead = false,
                    IsReported = false,
                    LastActivitydate = DateTime.Now,
                });
            }
            _context.SaveChanges();

            //StringBuilder Welcome = new StringBuilder("<h3 align ='right'>سعادة ");
            //Welcome.AppendFormat(string.Format(currentUser.ArName));
            //Welcome.Append("</h3>");
            //Welcome.Append("</br>");

            //StringBuilder Footer = new StringBuilder("<h3 align ='right'>أطيب التحيات</h3>");
            //Footer.Append("<h3 align ='right'>فريق  أكاديمية الصفا");

            //Footer.Append("</h3>");

            //EmailContent content2 = _context.EmailContents.Where(m => m.UniqueName.ToString() == "a5a6a6a4-c7f5-46f6-aea7-c27e5ea82d4c").SingleOrDefault();
            ////BackgroundJob.Schedule(() => _emailSender.SendEmailAsync(email, content2.ArSubject, content2.ArContent), TimeSpan.FromSeconds(1));


            ////         StringBuilder MyStringBuilder = new StringBuilder("");
            ////MyStringBuilder.AppendFormat(string.Format("<h2 align ='center'><a href='https://portal.arid.my/ar-LY/Account/Register/' >"));
            ////MyStringBuilder.Append("اضغط هنا للتسجيل في منصة اريد ");
            ////MyStringBuilder.Append("</a></h2>");

            //string link = "https://portal.arid.my/ar-LY/Account/DAL/" + currentUser.DAL;

            //BackgroundJob.Schedule(() => _emailSender.SendEmailAsync(currentUser.Email, currentUser.ArName + " تلقيت رسالة جديدة عبر منصة اريد بعنوان :" + title, Welcome.ToString() + content2.ArContent +
            //       $"<center><b><a href='{HtmlEncoder.Default.Encode(link)}'>رابط الدخول المباشر الى حسابكم في منصة أُريد </a> </p></b></center> <br/>" + Footer.ToString()), TimeSpan.FromSeconds(3));

            //if (currentUser.SecondEmail != null)
            //{
            //    BackgroundJob.Schedule(() => _emailSender.SendEmailAsync(currentUser.SecondEmail, currentUser.ArName + " تلقيت رسالة جديدة عبر منصة اريد بعنوان :" + title, Welcome.ToString() + content2.ArContent +
            //     $"<center><b><a href='{HtmlEncoder.Default.Encode(link)}'>رابط الدخول المباشر الى حسابكم في منصة أُريد </a> </p></b></center> <br/>" + Footer.ToString()), TimeSpan.FromSeconds(3));

            //}

            return RedirectToAction("Details", "FreelancerProjects", new { id = appuser });
        }
        public async Task<IActionResult> ControlPanel(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers
            .SingleOrDefaultAsync(m => m.Id == id);

            if (applicationUser == null)
            {
                return NotFound();
            }

            //await _signInManager.SignInAsync(user, isPersistent: false);
            await _signinmanager.SignInAsync(applicationUser, isPersistent: true);
            //if (result.IsCompletedSuccessfully)
            //{

            //  return RedirectToLocal(returnUrl);
            return RedirectToAction("Index", "Home");
            //}

            //   return View(applicationUser);
        }

        [Authorize(Roles ="Admins")]
        public IActionResult Control()
        {
            ViewData["systemSettingId"] = _context.SystemSettings.FirstOrDefault().Id;
            return View();
        }

        public async Task<IActionResult> Last100()
        {
            var last100 = _context.ApplicationUsers.OrderByDescending(a => a.RegistrationDate).Take(100);
            return View(await last100.ToListAsync());
        }

        public async Task<IActionResult> FastSearch(string keyword)
        {
            ViewData["keyword"] = keyword;
            var users = _context.ApplicationUsers.Where(a => a.ArName.Contains(keyword) || a.Id.Contains(keyword) || a.EnName.Contains(keyword) || a.Email.Contains(keyword) || a.PhoneNumber.Contains(keyword) || a.SecondEmail.Contains(keyword)).Include(a => a.Country).Include(a => a.City);
            return View(await users.ToListAsync());
        }

        public IActionResult Details(string id)
        {
            ApplicationUserViewModel applicationUserVM = new ApplicationUserViewModel()
            {
                ApplicationUser = _context.ApplicationUsers.SingleOrDefault(a => a.Id == id),
                BlogPosts = _context.BlogPost.Where(b => b.ApplicationUserId == id),

            };

            return View(applicationUserVM);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _usermanager.Users
                .Include(c => c.Country)
                .Include(c => c.City)
                .Include(a => a.ReferredBy)
                .SingleOrDefaultAsync(u => u.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.City.Where(c => c.CountryId == applicationUser.CountryId), "Id", "ArCityName", applicationUser.CityId);
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "ArCountryName", applicationUser.CountryId);
            ViewData["ReferredById"] = new SelectList(_context.ApplicationUsers.Where(a => a.Id == applicationUser.ReferredById), "Id", "Id", applicationUser.ReferredById);

            return View(applicationUser);
        }

        [Authorize(Roles = RoleName.Admins)]
        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUsers.Any(e => e.Id == id);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admins)]
        public async Task<IActionResult> Edit(string id, [Bind("ReferredById,ArName,EnName,OtherNames,DateofBirth,Gender,SecondEmail,RegDate,WebOS,WebOSDate,Status,UILanguage,ProfileImage,FeaturedImage,CVURL,Summary,ContactMeDetail,CountryId,CityId,UniversityId,FacultyId,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,Visitors,ImageProfile")] ApplicationUser applicationUser, IFormFile myfile)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var x = applicationUser.ImageProfile;
                    ApplicationUser thisUser = _usermanager.FindByIdAsync(applicationUser.Id).Result;
                    thisUser.UserName = applicationUser.UserName;
                    thisUser.Email = applicationUser.Email;
                    thisUser.ImageProfile = await UserFile.UploadeNewImageAsync(applicationUser.ImageProfile,
                    myfile, _environment.WebRootPath, Properties.Resources.ProfileImages, 400, 300);
                    //thisUser.RegDate = applicationUser.RegDate; // because it is desabled in the view
                    //thisUser.WebOS = applicationUser.WebOS; // because it is desabled in the view
                    //thisUser.WebOSDate = applicationUser.WebOSDate; // because it is desabled in the view
                    //thisUser.ReferredById = applicationUser.ReferredById; // because it is desabled in the view
                    thisUser.Status = applicationUser.Status;
                    thisUser.SecondEmail = applicationUser.SecondEmail;
                    thisUser.PhoneNumber = applicationUser.PhoneNumber;
                    thisUser.ArName = applicationUser.ArName;
                    thisUser.EnName = applicationUser.EnName;
                    thisUser.DateofBirth = applicationUser.DateofBirth;
                    thisUser.Gender = applicationUser.Gender;
                    thisUser.UILanguage = applicationUser.UILanguage;
                    thisUser.CountryId = applicationUser.CountryId;
                    thisUser.CityId = applicationUser.CityId;

                    await _usermanager.UpdateAsync(thisUser);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "ApplicationUsers", new { id });
            }
            ViewData["CityId"] = new SelectList(_context.City.Where(c => c.CountryId == applicationUser.CountryId), "Id", "ArCityName", applicationUser.CityId);
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "ArCountryName", applicationUser.CountryId);
            ViewData["ReferredById"] = new SelectList(_context.ApplicationUsers, "Id", "Id", applicationUser.ReferredById);

            return View(applicationUser);
        }

        // GET: ApplicationUsers/Delete/5
        [Authorize(Roles = RoleName.Admins)]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers
                .Include(c => c.Country)
                .Include(c => c.City)
                .Include(a => a.ReferredBy)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admins)]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.Id == id);
            var blogposts = _context.BlogPost.Where(b => b.ApplicationUserId == id);

            //_context.Update(ApplicationUsers);
            _context.BlogPost.RemoveRange(blogposts);
            _context.ApplicationUsers.Remove(applicationUser);

            UserFile.DeleteOldImage(_environment.WebRootPath, "ProfileImage", applicationUser.ImageProfile);



            //-----------------------------------email content-----------------------------------------
            //BackgroundJob.Schedule(() => _emailSender.SendEmailAsync(applicationUser.Email, "جاري حذف حسابكم بالكامل في منصة أُريد", Welcome.ToString() + content2.ToString() + Footer.ToString()), TimeSpan.FromSeconds(10));
            await _emailSender.SendEmailAsync(
                applicationUser.Email, "حذف حساب في أكاديمية الصفا", "تم حذف حسابكم بالكامل في أكاديمية الصفا");
            //-----------------------------------email content-----------------------------------------

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index(int? page, string keyword)
        {
            ViewData["keyword"] = keyword ?? null;
            List<ApplicationUser> users = new List<ApplicationUser>();
            var numberofpage = page ?? 1;
            if (keyword == null)
            {
                users = _context.ApplicationUsers.Include(a => a.Country).Include(a => a.City).Include(a => a.Nationality).ToList();
            }
            else
            {
                users = _context.ApplicationUsers.Include(a => a.Country).Include(a => a.City).Include(a => a.Nationality).Where(a => a.ArName.Contains(keyword) || a.Email.Contains(keyword) || a.Id.Contains(keyword)).ToList();
            }
            var onepageofusers = users.ToPagedList(numberofpage, 50);
            ViewBag.onepageofusers = onepageofusers;
            return View();
        }


    }
}
