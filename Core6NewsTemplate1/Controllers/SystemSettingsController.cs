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
    public class SystemSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _environment;
        public SystemSettingsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }

        // GET: SystemSettings
        public async Task<IActionResult> Index()
        {
            return View(await _context.SystemSettings.ToListAsync());
        }

        // GET: SystemSettings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SystemSettings == null)
            {
                return NotFound();
            }

            var systemSettings = await _context.SystemSettings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemSettings == null)
            {
                return NotFound();
            }

            return View(systemSettings);
        }

        // GET: SystemSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AraName,EngName,SmallLogo,LongAbout,EnLongAbout,LargeLogo,FavIcon,Footer,AdminEmail,LaunchDate,FB,Twitter,Linkedin,Youtube,Instagram,LastUpate,EnVision,About,EnAbout,Vision,Address,EnAddress,Phone,ProjectsNumber,EmployeesNumber,VolunteersNumber,BeneficiariesNumber")] SystemSettings systemSettings, IFormFile SmallLogofile, IFormFile LargeLogofile, IFormFile FavIconfile, IFormFile Footerfile)
        {
            if (ModelState.IsValid)
            {
                systemSettings.SmallLogo = await UserFile.UploadeNewFileAsync(systemSettings.SmallLogo,
SmallLogofile, _environment.WebRootPath, Properties.Resources.Files);
                systemSettings.LargeLogo = await UserFile.UploadeNewFileAsync(systemSettings.LargeLogo,
LargeLogofile, _environment.WebRootPath, Properties.Resources.Files);
                systemSettings.FavIcon = await UserFile.UploadeNewFileAsync(systemSettings.FavIcon,
FavIconfile, _environment.WebRootPath, Properties.Resources.Files);
                systemSettings.Footer = await UserFile.UploadeNewFileAsync(systemSettings.Footer,
Footerfile, _environment.WebRootPath, Properties.Resources.Files);

                _context.Add(systemSettings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemSettings);
        }

        // GET: SystemSettings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SystemSettings == null)
            {
                return NotFound();
            }

            var systemSettings = await _context.SystemSettings.FindAsync(id);
            if (systemSettings == null)
            {
                return NotFound();
            }
            return View(systemSettings);
        }

        // POST: SystemSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AraName,EngName,LongAbout,EnLongAbout,SmallLogo,LargeLogo,FavIcon,Footer,AdminEmail,LaunchDate,FB,Twitter,Linkedin,Youtube,Instagram,LastUpate,EnVision,About,EnAbout,Vision,Address,EnAddress,Phone,ProjectsNumber,EmployeesNumber,VolunteersNumber,BeneficiariesNumber")] SystemSettings systemSettings, IFormFile SmallLogofile, IFormFile LargeLogofile, IFormFile FavIconfile, IFormFile Footerfile)
        {
            systemSettings.SmallLogo = await UserFile.UploadeNewFileAsync(systemSettings.SmallLogo,
SmallLogofile, _environment.WebRootPath, Properties.Resources.Files);

            systemSettings.LargeLogo = await UserFile.UploadeNewFileAsync(systemSettings.LargeLogo,
LargeLogofile, _environment.WebRootPath, Properties.Resources.Files);


            systemSettings.FavIcon = await UserFile.UploadeNewFileAsync(systemSettings.FavIcon,
FavIconfile, _environment.WebRootPath, Properties.Resources.Files);

            systemSettings.Footer = await UserFile.UploadeNewFileAsync(systemSettings.Footer,
Footerfile, _environment.WebRootPath, Properties.Resources.Files);
            if (id != systemSettings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    systemSettings.LastUpate = DateTime.Now;
                    _context.Update(systemSettings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemSettingsExists(systemSettings.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit),id=1);
            }
            return View(systemSettings);
        }

        // GET: SystemSettings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SystemSettings == null)
            {
                return NotFound();
            }

            var systemSettings = await _context.SystemSettings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemSettings == null)
            {
                return NotFound();
            }

            return View(systemSettings);
        }

        // POST: SystemSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SystemSettings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SystemSettings'  is null.");
            }
            var systemSettings = await _context.SystemSettings.FindAsync(id);
            if (systemSettings != null)
            {
                _context.SystemSettings.Remove(systemSettings);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemSettingsExists(int id)
        {
            return _context.SystemSettings.Any(e => e.Id == id);
        }
    }
}
