using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebOS.Data;
using WebOS.Models;

namespace WebOS.Controllers
{
    public class TicketCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TicketCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.TicketCategory.ToListAsync());
        }

        // GET: TicketCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketCategory = await _context.TicketCategory
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ticketCategory == null)
            {
                return NotFound();
            }

            return View(ticketCategory);
        }

        // GET: TicketCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TicketCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NotifyEmail,Status")] TicketCategory ticketCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("List","Tickets");
            }
            return View(ticketCategory);
        }

        // GET: TicketCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketCategory = await _context.TicketCategory.SingleOrDefaultAsync(m => m.Id == id);
            if (ticketCategory == null)
            {
                return NotFound();
            }
            return View(ticketCategory);
        }

        // POST: TicketCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NotifyEmail,Status")] TicketCategory ticketCategory)
        {
            if (id != ticketCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketCategoryExists(ticketCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("List","Tickets");
            }
            return View(ticketCategory);
        }

        // GET: TicketCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketCategory = await _context.TicketCategory
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ticketCategory == null)
            {
                return NotFound();
            }

            return View(ticketCategory);
        }

        // POST: TicketCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketCategory = await _context.TicketCategory.SingleOrDefaultAsync(m => m.Id == id);
            _context.TicketCategory.Remove(ticketCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("List", "Tickets");
        }

        private bool TicketCategoryExists(int id)
        {
            return _context.TicketCategory.Any(e => e.Id == id);
        }
    }
}
