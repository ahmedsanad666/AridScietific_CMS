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
    public class CauseImagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _environment;
        public CauseImagesController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: CauseImages
        public async Task<IActionResult> Index(int id)
        {
            ViewData["causeid"] = id;
            var applicationDbContext = _context.CauseImage.Include(c => c.Cause).Where(c=>c.CauseId==id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CauseImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CauseImage == null)
            {
                return NotFound();
            }

            var causeImage = await _context.CauseImage
                .Include(c => c.Cause)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (causeImage == null)
            {
                return NotFound();
            }

            return View(causeImage);
        }

        // GET: CauseImages/Create
        public IActionResult Create()
        {
            ViewData["CauseId"] = new SelectList(_context.Cause, "Id", "Id");
            return View();
        }

        // POST: CauseImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int CauseId, string Image, IFormFile myfile)
        {
            var causeImage = new CauseImage();
            if (ModelState.IsValid)
            {
                causeImage.CauseId = CauseId;
                causeImage.Image = await UserFile.UploadeNewFileAsync(causeImage.Image,
myfile, _environment.WebRootPath, Properties.Resources.Pictures);


                _context.Add(causeImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),new { id=CauseId});
            }
            ViewData["CauseId"] = new SelectList(_context.Cause, "Id", "Id", causeImage.CauseId);
            return View(causeImage);
        }

        // GET: CauseImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CauseImage == null)
            {
                return NotFound();
            }

            var causeImage = await _context.CauseImage.FindAsync(id);
            if (causeImage == null)
            {
                return NotFound();
            }
            ViewData["CauseId"] = new SelectList(_context.Cause, "Id", "Id", causeImage.CauseId);
            return View(causeImage);
        }

        // POST: CauseImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CauseId,Image")] CauseImage causeImage)
        {
            if (id != causeImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(causeImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauseImageExists(causeImage.Id))
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
            ViewData["CauseId"] = new SelectList(_context.Cause, "Id", "Id", causeImage.CauseId);
            return View(causeImage);
        }

        // GET: CauseImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CauseImage == null)
            {
                return NotFound();
            }

            var causeImage = await _context.CauseImage
                .Include(c => c.Cause)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (causeImage == null)
            {
                return NotFound();
            }

            return View(causeImage);
        }

        // POST: CauseImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CauseImage == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CauseImage'  is null.");
            }
            var causeImage = await _context.CauseImage.FindAsync(id);
            if (causeImage != null)
            {
                _context.CauseImage.Remove(causeImage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CauseImageExists(int id)
        {
          return _context.CauseImage.Any(e => e.Id == id);
        }
    }
}
