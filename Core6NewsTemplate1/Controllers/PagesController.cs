using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebOS.Data;
using WebOS.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace WebOS.Controllers
{
    [Authorize(Roles = RoleName.Admins)]
    public class PagesController : Controller
    {
        private IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public PagesController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Pages
        public async Task<IActionResult> Index()
        {
            return View(await _context.Page.ToListAsync());
        }

        // GET: Pages/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Page
                .SingleOrDefaultAsync(m => m.Id == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: Pages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Body,EnTitle,EnBody,PageName,DateOfRecord")] Page page)
        {
            if (ModelState.IsValid)
            {
                page.Body = (System.Text.RegularExpressions.Regex.Replace(page.Body, @"(?></?\w+)(?>(?:[^>'""]+|'[^']*'|""[^""]*"")*)>", String.Empty)).Replace("\n", "<br/>");
                page.DateOfRecord = DateTime.Now;
                _context.Add(page);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        // GET: Pages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Page.SingleOrDefaultAsync(m => m.Id == id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // POST: Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,EnTitle,EnBody,PageName,DateOfRecord")] Page page)
        {
            if (id != page.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(page);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit),new {id = id });
            }
            return View(page);
        }

        // GET: Pages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Page
                .SingleOrDefaultAsync(m => m.Id == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // POST: Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var page = await _context.Page.SingleOrDefaultAsync(m => m.Id == id);
            _context.Page.Remove(page);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> UploadAudioFile(IFormFile myfile)
        {
            var x = myfile.ContentType;
            /* 
             * the content types of Wav are many
             * audio/wave
             * audio/wav
             * audio/x-wav
             * audio/x-pn-wav
             * see "https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types"
            */
            if (myfile.ContentType != "audio/wav")
            {
                return BadRequest("Wrong file type");
            }
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");//uploads where you want to save data inside wwwroot
            var filePath = Path.Combine(uploads, myfile.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await myfile.CopyToAsync(fileStream);
            }
            return RedirectToAction(nameof(Details), new { id = 1 });
        }
        private bool PageExists(int id)
        {
            return _context.Page.Any(e => e.Id == id);
        }
    }
}
