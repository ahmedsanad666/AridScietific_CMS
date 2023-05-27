using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebOS.AuxiliaryClasses;
using WebOS.Data;
using WebOS.Models;
using X.PagedList;

namespace WebOS.Controllers
{

    //[Authorize(Roles = RoleName.Admins)]
    public class BlogPostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IWebHostEnvironment _environment;

        public BlogPostsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _environment = environment;
            _userManager = userManager;
            _context = context;
        }

        // GET: BlogPosts
        [Authorize(Roles = RoleName.Admins)]
        public IActionResult Index(int? page)
        {
            var pagenumber = page ?? 1;
            var blogposts = _context.BlogPost.Include(b => b.ApplicationUser).Include(b => b.BlogTaxonomy).OrderByDescending(b => b.PostDateTime).OrderByDescending(b => b.PostDateTime);
            BlogViewModel blogVM = new BlogViewModel()
            {
                BlogPosts = _context.BlogPost.Include(b => b.BlogTaxonomy).OrderByDescending(b => b.PostDateTime),
                BlogTaxonomies = _context.BlogTaxonomy.Where(b => b.Sub == 0),

            };
            var onePageOfBlogPosts = blogposts.ToPagedList(pagenumber, 20);
            ViewBag.onePageOfBlogPosts = onePageOfBlogPosts;

            //var applicationDbContext = _context.BlogPost.Include(b => b.ApplicationUser).Include(b => b.BlogTaxonomy);
            return View(blogVM);
        }
        public IActionResult Search(int? page,string q)
        {
            var pagenumber = page ?? 1;
            ViewData["q"] = q;
            var blogposts = _context.BlogPost.Include(b => b.BlogTaxonomy).Where(b => b.Title.Contains(q)).OrderByDescending(b => b.PostDateTime).OrderByDescending(b => b.PostDateTime);

            var onePageOfBlogPosts = blogposts.ToPagedList(pagenumber, 100);
            ViewBag.onePageOfBlogPosts = onePageOfBlogPosts;

            //var applicationDbContext = _context.BlogPost.Include(b => b.ApplicationUser).Include(b => b.BlogTaxonomy);
            return View();
        }

        public IActionResult BlogsSearch(int? page, string q)
        {
            var pagenumber = page ?? 1;
            var blogposts = _context.BlogPost.Include(b => b.ApplicationUser).Include(b => b.BlogTaxonomy).OrderByDescending(b => b.PostDateTime).OrderByDescending(b => b.PostDateTime).Where(c => c.Title.Contains(q) || c.Body.Contains(q) || c.ApplicationUser.ArName.Contains(q));
            BlogViewModel blogVM = new BlogViewModel()
            {
                BlogPosts = _context.BlogPost.Include(b => b.ApplicationUser).Include(b => b.BlogTaxonomy).OrderByDescending(b => b.PostDateTime),

            };
            var onePageOfBlogPosts = blogposts.ToPagedList(pagenumber, 20);
            ViewBag.onePageOfBlogPosts = onePageOfBlogPosts;

            //var applicationDbContext = _context.BlogPost.Include(b => b.ApplicationUser).Include(b => b.BlogTaxonomy);
            return View(blogVM);
        }

        public IActionResult Wall(int? page)
        {
            var pagenumber = page ?? 1;
            var allblogposts = _context.BlogPost.Include(b => b.ApplicationUser).Include(b => b.BlogTaxonomy).OrderByDescending(b => b.PostDateTime);
            var blogposts = _context.BlogPost.Include(b => b.ApplicationUser).Include(b => b.BlogTaxonomy).OrderByDescending(b => b.PostDateTime);
            var onePageOfBlogPosts = blogposts.ToPagedList(pagenumber, 10);
            ViewBag.onePageOfBlogPosts = onePageOfBlogPosts;

            //var applicationDbContext = _context.BlogPost.Include(b => b.ApplicationUser).Include(b => b.BlogTaxonomy);
            return View(allblogposts);
        }

        public IActionResult TaxonomyIndex(int? page, int taxonomyid)
        {
            var pagenumber = page ?? 1;
            var blogposts = _context.BlogPost.Include(b => b.BlogTaxonomy).Where(b => b.BlogTaxonomyId == taxonomyid || b.BlogTaxonomy.Sub == taxonomyid).OrderByDescending(b => b.PostDateTime);
            ViewData["postscount"] = blogposts.Count();
            ViewData["taxonomyname"] = _context.BlogTaxonomy.SingleOrDefault(b => b.Id == taxonomyid).Name;
            var onePageOfBlogPosts = blogposts.ToPagedList(pagenumber, 30);
            ViewBag.onePageOfBlogPosts = onePageOfBlogPosts;

            //var applicationDbContext = _context.BlogPost.Include(b => b.ApplicationUser).Include(b => b.BlogTaxonomy);
            return View();
        }

        // GET: BlogPosts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var post = _context.BlogPost.SingleOrDefault(b => b.Id == id);

            if (HttpContext.Session.GetString("BlogId") != id.ToString())
            {
                post.Reads++;
                HttpContext.Session.SetString("BlogId", id.ToString());
            }
            _context.Update(post);
            _context.SaveChanges();

            if (id == null)
            {
                return NotFound();
            }
            BlogViewModel blogVM = new BlogViewModel()
            {
                SystemSettings = _context.SystemSettings.FirstOrDefault(),
                FeaturedBlogPosts = _context.BlogPost.Include(b=>b.BlogTaxonomy).Where(b=>b.Id!=id).OrderByDescending(b => b.PostDateTime).Take(4),
                BlogPost = await _context.BlogPost.Include(b => b.BlogTaxonomy).FirstOrDefaultAsync(m => m.Id == id),
                //PostForReading = await _context.BlogPost.Include(b => b.ApplicationUser).Include(b => b.BlogTaxonomy).OrderBy(b => b.Reads).FirstOrDefaultAsync(),
               // BlogPostComments = _context.BlogPostComment.Include(b => b.BlogPost).Where(b => b.BlogPostId == id && !b.IsHidden),
                //BlogPosts = _context.BlogPost.Include(b => b.ApplicationUser).Include(b => b.BlogTaxonomy).Where(b => b.Id != id).Where(b => b.BlogTaxonomyId == post.BlogTaxonomyId || b.BlogTaxonomyId == post.BlogTaxonomy.Sub),
                BlogPosts = _context.BlogPost.Include(b => b.BlogTaxonomy).Where(b => b.Title.Contains(post.Title) || post.Title.Contains(b.Title)).Take(5),
                BlogTaxonomies = _context.BlogTaxonomy.Where(b => b.Sub == 0),
                BlogPostImages = _context.BlogPostImage.Where(b => b.BlogPostId == id),
            };

            return View(blogVM);
        }

        // GET: BlogPosts/Create
        [Authorize(Roles = RoleName.Admins)]
        public IActionResult Create()
        {
            //ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["BlogTaxonomyId"] = new SelectList(_context.Set<BlogTaxonomy>().Where(b => b.Sub == 0), "Id", "Name");
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admins)]
        public async Task<IActionResult> Create([Bind("Id,Title,Body,Introduction,PostDateTime,DateModified,IsCommentsAllowed,Image,Source,File,IsApproved,IsHidden,IsFeatured,Reads,IsDeleted,ApplicationUserId,Tags,BlogTaxonomyId,EnTitle,EnBody,EnIntroduction,EnSource,EnTags,YouTube,IsPdf")] BlogPost blogPost, IFormFile myfile, IFormFile myfile1, int subvalue)
        {
            if (blogPost.Title != null)
            {
                if (subvalue != 0)
                {
                    blogPost.BlogTaxonomyId = subvalue;
                }


                blogPost.Image = await UserFile.UploadeNewFileAsync(blogPost.Image,
myfile, _environment.WebRootPath, Properties.Resources.Pictures);
                blogPost.File = await UserFile.UploadeNewFileAsync(blogPost.File,
myfile1, _environment.WebRootPath, Properties.Resources.Files);
                blogPost.IsHidden = false;
                blogPost.IsDeleted = false;
                blogPost.Reads = 0;
                blogPost.PostDateTime = DateTime.Now;
                blogPost.ApplicationUserId = _userManager.GetUserId(User);
                blogPost.Id = Guid.NewGuid();
                _context.Add(blogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", blogPost.ApplicationUserId);
            ViewData["BlogTaxonomyId"] = new SelectList(_context.Set<BlogTaxonomy>(), "Id", "Name", blogPost.BlogTaxonomyId);
            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        [Authorize(Roles = RoleName.Admins)]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers.Where(b => b.Id == blogPost.ApplicationUserId), "Id", "ArName", blogPost.ApplicationUserId);
            var blogtaxonomy = _context.BlogTaxonomy.SingleOrDefault(b => b.Id == blogPost.BlogTaxonomyId);
            if (blogtaxonomy.Sub == 0)
            {
                ViewData["BlogTaxonomyId"] = new SelectList(_context.Set<BlogTaxonomy>().Where(b => b.Sub == 0), "Id", "Name");
                ViewData["BlogTaxonomysubId"] = new SelectList(_context.Set<BlogTaxonomy>().Where(b => b.Sub == blogtaxonomy.Id), "Id", "Name");

            }
            else
            {
                var parent = _context.BlogTaxonomy.SingleOrDefault(b => b.Id == blogtaxonomy.Sub);

                ViewData["BlogTaxonomysubId"] = new SelectList(_context.Set<BlogTaxonomy>().Where(b => b.Sub == blogtaxonomy.Sub), "Id", "Name", blogtaxonomy.Id);
                ViewData["BlogTaxonomyId"] = new SelectList(_context.Set<BlogTaxonomy>().Where(b => b.Sub == 0), "Id", "Name", parent.Id);
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admins)]

        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,YouTube,Body,Introduction,PostDateTime,DateModified,IsCommentsAllowed,Image,Source,File,IsApproved,IsHidden,IsFeatured,Reads,IsDeleted,ApplicationUserId,Tags,BlogTaxonomyId,EnTitle,EnBody,EnIntroduction,EnSource,EnTags,IsPdf")] BlogPost blogPost, IFormFile myfile, IFormFile myfile1, int subvalue, int taxonomyvalue)
        {
            if (id != blogPost.Id)
            {
                return NotFound();
            }

            if (id != null)
            {
                try
                {
                    if (subvalue != 0)
                    {
                        blogPost.BlogTaxonomyId = subvalue;
                    }
                    else
                    {
                        blogPost.BlogTaxonomyId = taxonomyvalue;
                    }



                    blogPost.Image = await UserFile.UploadeNewFileAsync(blogPost.Image,
    myfile, _environment.WebRootPath, Properties.Resources.Pictures);
                    blogPost.File = await UserFile.UploadeNewFileAsync(blogPost.File,
    myfile1, _environment.WebRootPath, "Files");
                    blogPost.PostDateTime = DateTime.Now;

                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", blogPost.ApplicationUserId);
            ViewData["BlogTaxonomyId"] = new SelectList(_context.Set<BlogTaxonomy>(), "Id", "Id", blogPost.BlogTaxonomyId);
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        [Authorize(Roles = RoleName.Admins)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost
                .Include(b => b.ApplicationUser)
                .Include(b => b.BlogTaxonomy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [Authorize(Roles = RoleName.Admins)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var blogPost = await _context.BlogPost.FindAsync(id);
            _context.BlogPost.Remove(blogPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(Guid id)
        {
            return _context.BlogPost.Any(e => e.Id == id);
        }
        public JsonResult BlogPostsSearch(string ss, int page = 1)
        {
            var pagesize = 10;
            var BlogPosts = _context.BlogPost.Include(b => b.ApplicationUser).Include(b => b.BlogTaxonomy).Where(b => b.Title.Contains(ss) || b.Body.Contains(ss) || b.ApplicationUser.ArName.Contains(ss) || b.ApplicationUser.EnName.Contains(ss)).OrderByDescending(f => f.PostDateTime).Skip((page - 1) * pagesize).Take(pagesize);
            BlogViewModel blogVM = new BlogViewModel()
            {
                BlogPosts = BlogPosts,
                BlogPostsCount = _context.BlogPost.Include(b => b.ApplicationUser).Include(b => b.BlogTaxonomy).Where(b => b.Title.Contains(ss) || b.Body.Contains(ss) || b.ApplicationUser.ArName.Contains(ss) || b.ApplicationUser.EnName.Contains(ss)).Count(),
            };

            //ViewData["Count"] = _context.PostMetric.Count(a => a.PostId == id);
            return Json(blogVM);
        }

        public IActionResult AddComment(Guid blogpostid, string commenttext, string username, string useremail)
        {

            _context.BlogPostComment.Add(new BlogPostComment
            {
                Id = Guid.NewGuid(),
                Comment = commenttext,
                BlogPostId = blogpostid,
                IsDeleted = false,
                IsFeatured = false,
                IsHidden = true,
                DateTime = DateTime.Now,
            });

            _context.SaveChanges();
            return Ok();
        }

        public IActionResult UpdatePosts()
        {
            var News = _context.News.Where(a => a.Id < 10);
            foreach (var item in News)
            {
                _context.BlogPost.Add(new BlogPost
                {
                    Id = Guid.NewGuid(),
                    Title = item.Subject,
                    Body = item.Body,
                    Introduction = item.Introduction,
                    PostDateTime = item.Date_published,
                    DateModified = item.Date_published,
                    Image = item.ImageLink,
                    IsHidden = false,
                    IsFeatured = false,
                    Reads = item.NewsReader,
                    IsDeleted = false,
                    Tags = item.MetaDescription,
                    BlogTaxonomyId = item.CategoryId,
                    ApplicationUserId = _userManager.GetUserId(User),
                });
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult DeleteComment(Guid id)
        {

            var comment = _context.BlogPostComment.SingleOrDefault(b => b.Id == id);
            comment.IsHidden = true;
            _context.Update(comment);


            _context.SaveChanges();
            return RedirectToAction(nameof(Details), new { id = comment.BlogPostId });
        }
    }
}
