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
    public class CauseCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _environment;
        public CauseCategoriesController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: CauseCategories
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.CauseCategory = _context.CauseCategory.SingleOrDefault(c => c.Id == id);
            ViewBag.CauseImages = _context.CauseImage.Where(c => c.CauseId == id);
            return View(await _context.Cause.Include(c => c.CauseCategory).Where(c => c.CauseCategoryId == id).ToListAsync());
        }

        // GET: CauseCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CauseCategory == null)
            {
                return NotFound();
            }

            var causeCategory = await _context.CauseCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (causeCategory == null)
            {
                return NotFound();
            }

            return View(causeCategory);
        }

        // GET: CauseCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CauseCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,EnTitle,EnDescription,Image")] CauseCategory causeCategory, IFormFile myfile)
        {
            if (ModelState.IsValid)
            {
                causeCategory.Image = await UserFile.UploadeNewFileAsync(causeCategory.Image,
myfile, _environment.WebRootPath, Properties.Resources.Pictures);

                _context.Add(causeCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Causes");
            }
            return View(causeCategory);
        }

        // GET: CauseCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CauseCategory == null)
            {
                return NotFound();
            }

            var causeCategory = await _context.CauseCategory.FindAsync(id);
            if (causeCategory == null)
            {
                return NotFound();
            }
            return View(causeCategory);
        }

        // POST: CauseCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,EnTitle,EnDescription,Image")] CauseCategory causeCategory, IFormFile myfile)
        {
            if (id != causeCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    causeCategory.Image = await UserFile.UploadeNewFileAsync(causeCategory.Image,
myfile, _environment.WebRootPath, Properties.Resources.Pictures);


                    _context.Update(causeCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauseCategoryExists(causeCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "Causes");
            }
            return View(causeCategory);
        }

        // GET: CauseCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CauseCategory == null)
            {
                return NotFound();
            }

            var causeCategory = await _context.CauseCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (causeCategory == null)
            {
                return NotFound();
            }

            return View(causeCategory);
        }

        // POST: CauseCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CauseCategory == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CauseCategory'  is null.");
            }
            var causeCategory = await _context.CauseCategory.FindAsync(id);
            if (causeCategory != null)
            {
                _context.CauseCategory.Remove(causeCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Causes");
        }

        private bool CauseCategoryExists(int id)
        {
            return _context.CauseCategory.Any(e => e.Id == id);
        }
    }
}
