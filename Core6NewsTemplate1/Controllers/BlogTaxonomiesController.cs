using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebOS.Data;
using WebOS.Models;

namespace WebOS.Controllers
{
    [Authorize(Roles = RoleName.Admins)]
    public class BlogTaxonomiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogTaxonomiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BlogTaxonomies
        public async Task<IActionResult> Index(int id)
        {           
            ViewBag.taxonomy = _context.BlogTaxonomy.SingleOrDefault(b => b.Id == id);
            return View(await _context.BlogTaxonomy.Where(b => b.Sub == id).ToListAsync());
        }

        public async Task<IActionResult> List(int id)
        {           
            var taxonomies =await _context.BlogTaxonomy.ToListAsync();
            return View(taxonomies);
        }

        // GET: BlogTaxonomies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTaxonomy = await _context.BlogTaxonomy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogTaxonomy == null)
            {
                return NotFound();
            }

            return View(blogTaxonomy);
        }

        // GET: BlogTaxonomies/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: BlogTaxonomies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,EnName,Sub,IsFeatured")] BlogTaxonomy blogTaxonomy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogTaxonomy);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "BlogPosts");
            }
            return View(blogTaxonomy);
        }

        public IActionResult CreateSub(int tid)
        {
            ViewData["tid"] = tid;
            ViewData["taxonomyname"] = _context.BlogTaxonomy.SingleOrDefault(t => t.Id == tid).Name;
            return View();
        }


        // POST: BlogTaxonomies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSub([Bind("Id,Name,Sub")] BlogTaxonomy blogTaxonomy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogTaxonomy);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "BlogTaxonomies", new { id = blogTaxonomy.Sub });
            }
            return View(blogTaxonomy);
        }

        // GET: BlogTaxonomies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTaxonomy = await _context.BlogTaxonomy.FindAsync(id);
            if (blogTaxonomy == null)
            {
                return NotFound();
            }
            return View(blogTaxonomy);
        }

        // POST: BlogTaxonomies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,EnName,Sub,IsFeatured")] BlogTaxonomy blogTaxonomy)
        {
            if (id != blogTaxonomy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogTaxonomy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogTaxonomyExists(blogTaxonomy.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "BlogPosts");
            }
            return View(blogTaxonomy);
        }

        // GET: BlogTaxonomies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTaxonomy = await _context.BlogTaxonomy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogTaxonomy == null)
            {
                return NotFound();
            }

            return View(blogTaxonomy);
        }

        // POST: BlogTaxonomies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogTaxonomy = await _context.BlogTaxonomy.FindAsync(id);
            _context.BlogTaxonomy.Remove(blogTaxonomy);
            await _context.SaveChangesAsync();
            if (blogTaxonomy.Sub > 0)
            {
                return RedirectToAction(nameof(Index), "BlogTaxonomies", new { id = blogTaxonomy.Sub });
            }
            else { return RedirectToAction(nameof(Index), "BlogPosts"); }


        }

        private bool BlogTaxonomyExists(int id)
        {
            return _context.BlogTaxonomy.Any(e => e.Id == id);
        }
    }
}
