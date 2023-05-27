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
    public class CausesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _environment;

        public CausesController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }

        // GET: Causes
        public async Task<IActionResult> Index()
        {
            ViewBag.CauseCategory = _context.CauseCategory;
              return View(await _context.Cause.ToListAsync());
        }

        // GET: Causes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cause == null)
            {
                return NotFound();
            }

            var cause = await _context.Cause
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cause == null)
            {
                return NotFound();
            }

            return View(cause);
        }

        // GET: Causes/Create
        public IActionResult Create()
        {
            ViewData["CauseCategoryId"] = new SelectList(_context.Set<CauseCategory>(), "Id", "Title");
            return View();
        }

        // POST: Causes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CauseCategoryId,Title,EnTitle,Field,EnField,BriefDescription,EnBriefDescription,Body,EnBody,Image,IsActive")] Cause cause,IFormFile myfile)
        {
            if (ModelState.IsValid)
            {
                cause.PostDateTime = DateTime.Now;
                cause.Image = await UserFile.UploadeNewFileAsync(cause.Image,
myfile, _environment.WebRootPath, Properties.Resources.Pictures);

                _context.Add(cause);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cause);
        }

        // GET: Causes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cause == null)
            {
                return NotFound();
            }

            var cause = await _context.Cause.FindAsync(id);
            if (cause == null)
            {
                return NotFound();
            }
            ViewData["CauseCategoryId"] = new SelectList(_context.Set<CauseCategory>(), "Id", "Title");
            return View(cause);
        }

        // POST: Causes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostDateTime,Title,CauseCategoryId,EnTitle,Field,EnField,BriefDescription,EnBriefDescription,Body,EnBody,Image,IsActive")] Cause cause,IFormFile myfile)
        {
            if (id != cause.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cause.Image = await UserFile.UploadeNewFileAsync(cause.Image,
myfile, _environment.WebRootPath, Properties.Resources.Pictures);

                    _context.Update(cause);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauseExists(cause.Id))
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
            return View(cause);
        }

        // GET: Causes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cause == null)
            {
                return NotFound();
            }

            var cause = await _context.Cause
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cause == null)
            {
                return NotFound();
            }

            return View(cause);
        }

        // POST: Causes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cause == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cause'  is null.");
            }
            var cause = await _context.Cause.FindAsync(id);
            if (cause != null)
            {
                _context.Cause.Remove(cause);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Causes");
        }

        private bool CauseExists(int id)
        {
          return _context.Cause.Any(e => e.Id == id);
        }
    }
}
