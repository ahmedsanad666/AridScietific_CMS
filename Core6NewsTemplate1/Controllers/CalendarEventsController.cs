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
    public class CalendarEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalendarEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CalendarEvents
        public async Task<IActionResult> Index()
        {
            return View(await _context.CalendarEvent.ToListAsync());
        }

        // GET: CalendarEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendarEvent = await _context.CalendarEvent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calendarEvent == null)
            {
                return NotFound();
            }

            return View(calendarEvent);
        }

        // GET: CalendarEvents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CalendarEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartingDate,EndingDate,CalendarEventCategoryId")] CalendarEvent calendarEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calendarEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calendarEvent);
        }

        // GET: CalendarEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendarEvent = await _context.CalendarEvent.FindAsync(id);
            if (calendarEvent == null)
            {
                return NotFound();
            }
            return View(calendarEvent);
        }

        // POST: CalendarEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartingDate,EndingDate,CalendarEventCategoryId")] CalendarEvent calendarEvent)
        {
            if (id != calendarEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calendarEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalendarEventExists(calendarEvent.Id))
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
            return View(calendarEvent);
        }

        // GET: CalendarEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendarEvent = await _context.CalendarEvent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calendarEvent == null)
            {
                return NotFound();
            }

            return View(calendarEvent);
        }

        // POST: CalendarEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calendarEvent = await _context.CalendarEvent.FindAsync(id);
            _context.CalendarEvent.Remove(calendarEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalendarEventExists(int id)
        {
            return _context.CalendarEvent.Any(e => e.Id == id);
        }
    }
}
