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
    public class TestimonialsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _environment;
        public TestimonialsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Testimonials
        public async Task<IActionResult> Index()
        {
              return View(await _context.Testimonial.ToListAsync());
        }

        // GET: Testimonials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Testimonial == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonial
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // GET: Testimonials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Testimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,EnName,BriefDescription,EnBriefDescription,Job,EnJob,Image,IsActive")] Testimonial testimonial,IFormFile myfile)
        {
            if (ModelState.IsValid)
            {
                testimonial.Image = await UserFile.UploadeNewFileAsync(testimonial.Image,
myfile, _environment.WebRootPath, Properties.Resources.Pictures);

                _context.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testimonial);
        }

        // GET: Testimonials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Testimonial == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonial.FindAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }
            return View(testimonial);
        }

        // POST: Testimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,EnName,BriefDescription,EnBriefDescription,Job,EnJob,Image,IsActive")] Testimonial testimonial,IFormFile myfile)
        {
            if (id != testimonial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                testimonial.Image = await UserFile.UploadeNewFileAsync(testimonial.Image,
myfile, _environment.WebRootPath, Properties.Resources.Pictures);


                try
                {
                    _context.Update(testimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialExists(testimonial.Id))
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
            return View(testimonial);
        }

        // GET: Testimonials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Testimonial == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonial
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // POST: Testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Testimonial == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Testimonial'  is null.");
            }
            var testimonial = await _context.Testimonial.FindAsync(id);
            if (testimonial != null)
            {
                _context.Testimonial.Remove(testimonial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestimonialExists(int id)
        {
          return _context.Testimonial.Any(e => e.Id == id);
        }
    }
}
