using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebOS.AuxiliaryClasses;
using WebOS.Data;
using WebOS.Models;
using Microsoft.AspNetCore.Hosting;

namespace WebOS.Controllers
{
    public class GalleryImagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        [Obsolete]
        private readonly IWebHostEnvironment _environment;

        [Obsolete]
        public GalleryImagesController(ApplicationDbContext context,IWebHostEnvironment environment)
        {
            _context = context;

            _environment = environment;
        }

        // GET: GalleryImages
        public async Task<IActionResult> Index(int id)
        {
            var applicationDbContext = _context.GalleryImage.Where(a=>a.GalleryCategoryId == id).Include(g => g.GalleryCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GalleryImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryImage = await _context.GalleryImage
                .Include(g => g.GalleryCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (galleryImage == null)
            {
                return NotFound();
            }

            return View(galleryImage);
        }

        // GET: GalleryImages/Create
        public IActionResult Create(int CatId)
        {
            ViewData["GalleryCategoryId"] = new SelectList(_context.GalleryCategory.Where(a=>a.Id == CatId), "Id", "Name");
            return View();
        }

        // POST: GalleryImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Url,GalleryCategoryId")] GalleryImage galleryImage,IFormFile myfile)
        {
            if (ModelState.IsValid)
            {

                galleryImage.Url = await UserFile.UploadeNewFileAsync(galleryImage.Url,
myfile, _environment.WebRootPath, Properties.Resources.Pictures);

                _context.Add(galleryImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GalleryCategoryId"] = new SelectList(_context.GalleryCategory, "Id", "Id", galleryImage.GalleryCategoryId);
            return View(galleryImage);
        }

        // GET: GalleryImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryImage = await _context.GalleryImage.FindAsync(id);
            if (galleryImage == null)
            {
                return NotFound();
            }
            ViewData["GalleryCategoryId"] = new SelectList(_context.GalleryCategory, "Id", "Id", galleryImage.GalleryCategoryId);
            return View(galleryImage);
        }

        // POST: GalleryImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Url,GalleryCategoryId")] GalleryImage galleryImage)
        {
            if (id != galleryImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galleryImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryImageExists(galleryImage.Id))
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
            ViewData["GalleryCategoryId"] = new SelectList(_context.GalleryCategory, "Id", "Id", galleryImage.GalleryCategoryId);
            return View(galleryImage);
        }

        // GET: GalleryImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galleryImage = await _context.GalleryImage
                .Include(g => g.GalleryCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (galleryImage == null)
            {
                return NotFound();
            }

            return View(galleryImage);
        }

        // POST: GalleryImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var galleryImage = await _context.GalleryImage.FindAsync(id);
            _context.GalleryImage.Remove(galleryImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalleryImageExists(int id)
        {
            return _context.GalleryImage.Any(e => e.Id == id);
        }
    }
}
