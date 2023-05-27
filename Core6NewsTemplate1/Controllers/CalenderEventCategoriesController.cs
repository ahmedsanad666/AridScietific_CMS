using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ARID.Models;
using WebOS.Data;

namespace WebOS.Controllers
{
    public class CalenderEventCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalenderEventCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CalenderEventCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.CalenderEventCategory.ToListAsync());
        }

        // GET: CalenderEventCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calenderEventCategory = await _context.CalenderEventCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calenderEventCategory == null)
            {
                return NotFound();
            }

            return View(calenderEventCategory);
        }

        // GET: CalenderEventCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CalenderEventCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,color")] CalenderEventCategory calenderEventCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calenderEventCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calenderEventCategory);
        }

        // GET: CalenderEventCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calenderEventCategory = await _context.CalenderEventCategory.FindAsync(id);
            if (calenderEventCategory == null)
            {
                return NotFound();
            }
            return View(calenderEventCategory);
        }

        // POST: CalenderEventCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,color")] CalenderEventCategory calenderEventCategory)
        {
            if (id != calenderEventCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calenderEventCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalenderEventCategoryExists(calenderEventCategory.Id))
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
            return View(calenderEventCategory);
        }

        // GET: CalenderEventCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calenderEventCategory = await _context.CalenderEventCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calenderEventCategory == null)
            {
                return NotFound();
            }

            return View(calenderEventCategory);
        }

        // POST: CalenderEventCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calenderEventCategory = await _context.CalenderEventCategory.FindAsync(id);
            _context.CalenderEventCategory.Remove(calenderEventCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalenderEventCategoryExists(int id)
        {
            return _context.CalenderEventCategory.Any(e => e.Id == id);
        }
    }
}
