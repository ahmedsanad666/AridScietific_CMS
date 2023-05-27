using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using WebOS.AuxiliaryClasses;
using WebOS.Data;
using WebOS.Models;

namespace WebOS.Controllers
{
    public class HomeCardsController : Controller
    {
        private IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public HomeCardsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }

        // GET: HomeCards
        public async Task<IActionResult> Index()
        {
            return View(await _context.HomeCard.ToListAsync());
        }

        // GET: HomeCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeCard = await _context.HomeCard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeCard == null)
            {
                return NotFound();
            }

            return View(homeCard);
        }

        // GET: HomeCards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HomeCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Image,indx,Url,UrlText,Status")] HomeCard homeCard,IFormFile myfile)
        {
            if (ModelState.IsValid)
            {
                homeCard.Image = await UserFile.UploadeNewFileAsync(homeCard.Image,
myfile, _environment.WebRootPath, "Pictures");

                _context.Add(homeCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homeCard);
        }

        // GET: HomeCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeCard = await _context.HomeCard.FindAsync(id);
            if (homeCard == null)
            {
                return NotFound();
            }
            return View(homeCard);
        }

        // POST: HomeCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Image,indx,Url,UrlText,Status")] HomeCard homeCard,IFormFile myfile)
        {
            if (id != homeCard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    homeCard.Image = await UserFile.UploadeNewFileAsync(homeCard.Image,
myfile, _environment.WebRootPath, "Pictures");


                    _context.Update(homeCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeCardExists(homeCard.Id))
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
            return View(homeCard);
        }

        // GET: HomeCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeCard = await _context.HomeCard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeCard == null)
            {
                return NotFound();
            }

            return View(homeCard);
        }

        // POST: HomeCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeCard = await _context.HomeCard.FindAsync(id);
            _context.HomeCard.Remove(homeCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeCardExists(int id)
        {
            return _context.HomeCard.Any(e => e.Id == id);
        }
    }
}
