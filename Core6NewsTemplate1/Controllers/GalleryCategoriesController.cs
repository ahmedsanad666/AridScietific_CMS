using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebOS.Data;
using WebOS.Models;

namespace WebOS.Controllers
{
    public class GalleryCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GalleryCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GalleryCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.GalleryCategory.ToListAsync());
        }

        // GET: GalleryCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryCategory = await _context.GalleryCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (galleryCategory == null)
            {
                return NotFound();
            }

            return View(galleryCategory);
        }

        // GET: GalleryCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GalleryCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] GalleryCategory galleryCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(galleryCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(galleryCategory);
        }

        // GET: GalleryCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryCategory = await _context.GalleryCategory.FindAsync(id);
            if (galleryCategory == null)
            {
                return NotFound();
            }
            return View(galleryCategory);
        }

        // POST: GalleryCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] GalleryCategory galleryCategory)
        {
            if (id != galleryCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galleryCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryCategoryExists(galleryCategory.Id))
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
            return View(galleryCategory);
        }

        // GET: GalleryCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryCategory = await _context.GalleryCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (galleryCategory == null)
            {
                return NotFound();
            }

            return View(galleryCategory);
        }

        // POST: GalleryCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var galleryCategory = await _context.GalleryCategory.FindAsync(id);
            _context.GalleryCategory.Remove(galleryCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalleryCategoryExists(int id)
        {
            return _context.GalleryCategory.Any(e => e.Id == id);
        }
    }
}
