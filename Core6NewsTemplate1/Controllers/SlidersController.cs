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
    public class SlidersController : Controller
    {
        private IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public SlidersController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Sliders
        public async Task<IActionResult> Index()
        {
              return View(await _context.Slider.ToListAsync());
        }

        // GET: Sliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Slider == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: Sliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,EnTitle,SubTitle,EnSubTitle,Image,indx,AddDate,ContentAlignment,Url,UrlText,EnUrlText,Status")] Slider slider,IFormFile myfile)
        {
            if (ModelState.IsValid)
            {
                slider.Image = await UserFile.UploadeNewFileAsync(slider.Image,
myfile, _environment.WebRootPath, Properties.Resources.Pictures);

                _context.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        // GET: Sliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Slider == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,EnTitle,SubTitle,EnSubTitle,Image,indx,AddDate,ContentAlignment,Url,UrlText,EnUrlText,Status")] Slider slider,IFormFile myfile)
        {
            if (id != slider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    slider.Image = await UserFile.UploadeNewFileAsync(slider.Image,
myfile, _environment.WebRootPath, Properties.Resources.Pictures);

                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.Id))
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
            return View(slider);
        }

        // GET: Sliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Slider == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Slider == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Slider'  is null.");
            }
            var slider = await _context.Slider.FindAsync(id);
            if (slider != null)
            {
                _context.Slider.Remove(slider);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id)
        {
          return _context.Slider.Any(e => e.Id == id);
        }
    }
}
