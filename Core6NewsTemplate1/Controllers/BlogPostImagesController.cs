using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebOS.AuxiliaryClasses;
using WebOS.Data;
using WebOS.Models;

namespace WebOS.Controllers
{
    public class BlogPostImagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _environment;
        public BlogPostImagesController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: BlogPostImages
        public async Task<IActionResult> Index(Guid postid)
        {
            ViewData["postid"] = postid;
            var applicationDbContext = _context.BlogPostImage.Include(b => b.BlogPost).Where(b=>b.BlogPostId==postid);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BlogPostImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BlogPostImage == null)
            {
                return NotFound();
            }

            var blogPostImage = await _context.BlogPostImage
                .Include(b => b.BlogPost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPostImage == null)
            {
                return NotFound();
            }

            return View(blogPostImage);
        }

        // GET: BlogPostImages/Create
        public IActionResult Create()
        {
            ViewData["BlogPostId"] = new SelectList(_context.BlogPost, "Id", "Body");
            return View();
        }

        // POST: BlogPostImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid BlogPostId,string Image, IFormFile myfile)
        {
            var blogPostImage = new BlogPostImage();
            if (ModelState.IsValid)
            {
                blogPostImage.BlogPostId = BlogPostId;
                blogPostImage.Image = await UserFile.UploadeNewFileAsync(blogPostImage.Image,
myfile, _environment.WebRootPath, Properties.Resources.Pictures);

                _context.Add(blogPostImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {postid=BlogPostId});
            }
            ViewData["BlogPostId"] = new SelectList(_context.BlogPost, "Id", "Body", blogPostImage.BlogPostId);
            return View(blogPostImage);
        }

        // GET: BlogPostImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BlogPostImage == null)
            {
                return NotFound();
            }

            var blogPostImage = await _context.BlogPostImage.FindAsync(id);
            if (blogPostImage == null)
            {
                return NotFound();
            }
            ViewData["BlogPostId"] = new SelectList(_context.BlogPost, "Id", "Body", blogPostImage.BlogPostId);
            return View(blogPostImage);
        }

        // POST: BlogPostImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BlogPostId,Image")] BlogPostImage blogPostImage)
        {
            if (id != blogPostImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPostImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostImageExists(blogPostImage.Id))
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
            ViewData["BlogPostId"] = new SelectList(_context.BlogPost, "Id", "Body", blogPostImage.BlogPostId);
            return View(blogPostImage);
        }

        // GET: BlogPostImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BlogPostImage == null)
            {
                return NotFound();
            }

            var blogPostImage = await _context.BlogPostImage
                .Include(b => b.BlogPost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPostImage == null)
            {
                return NotFound();
            }

            return View(blogPostImage);
        }

        // POST: BlogPostImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BlogPostImage == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BlogPostImage'  is null.");
            }
            var blogPostImage = await _context.BlogPostImage.FindAsync(id);
            if (blogPostImage != null)
            {
                _context.BlogPostImage.Remove(blogPostImage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostImageExists(int id)
        {
          return _context.BlogPostImage.Any(e => e.Id == id);
        }
    }
}
