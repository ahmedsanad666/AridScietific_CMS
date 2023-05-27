using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebOS.AuxiliaryClasses;
using WebOS.Data;
using WebOS.Models;

namespace WebOS.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _environment;
        public ServicesController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }

        // GET: Services
        [Authorize(Roles=RoleName.Admins)]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Service.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Service == null)
            {
                return NotFound();
            }

            var service = await _context.Service
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        [Authorize(Roles = RoleName.Admins)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = RoleName.Admins)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,EnTitle,BriefDescription,EnBriefDescription,Body,EnBody,Image,IsActive")] Service service,IFormFile myfile)
        {
            if (ModelState.IsValid)
            {
                service.Image = await UserFile.UploadeNewFileAsync(service.Image,
myfile, _environment.WebRootPath, Properties.Resources.Pictures);

                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Services/Edit/5
        [Authorize(Roles = RoleName.Admins)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Service == null)
            {
                return NotFound();
            }

            var service = await _context.Service.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admins)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,EnTitle,BriefDescription,EnBriefDescription,Body,EnBody,Image,IsActive")] Service service,IFormFile myfile)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    service.Image = await UserFile.UploadeNewFileAsync(service.Image,
myfile, _environment.WebRootPath, Properties.Resources.Pictures);

                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            return View(service);
        }

        // GET: Services/Delete/5
        [Authorize(Roles = RoleName.Admins)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Service == null)
            {
                return NotFound();
            }

            var service = await _context.Service
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admins)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Service == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Service'  is null.");
            }
            var service = await _context.Service.FindAsync(id);
            if (service != null)
            {
                _context.Service.Remove(service);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = RoleName.Admins)]
        private bool ServiceExists(int id)
        {
          return _context.Service.Any(e => e.Id == id);
        }
    }
}
