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
    public class NavMenuItemSubsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NavMenuItemSubsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NavMenuItemSubs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.NavMenuItemSub.Include(n => n.NavMenuItem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: NavMenuItemSubs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navMenuItemSub = await _context.NavMenuItemSub
                .Include(n => n.NavMenuItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (navMenuItemSub == null)
            {
                return NotFound();
            }

            return View(navMenuItemSub);
        }

        // GET: NavMenuItemSubs/Create
        public IActionResult Create()
        {
            ViewData["NavMenuItemId"] = new SelectList(_context.NavMenuItems, "Id", "Url");
            return View();
        }

        // POST: NavMenuItemSubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NavMenuItemId,Icon,Url,Name,indx,IsActive")] NavMenuItemSub navMenuItemSub)
        {
            if (ModelState.IsValid)
            {
                _context.Add(navMenuItemSub);
                await _context.SaveChangesAsync();
                var navmenuitem = _context.NavMenuItems.Include(n => n.NavMenu).SingleOrDefault(n => n.Id == navMenuItemSub.NavMenuItemId);
                return RedirectToAction("Details","NavMenus",new { id= navmenuitem.NavMenuId});
            }
            ViewData["NavMenuItemId"] = new SelectList(_context.NavMenuItems, "Id", "Url", navMenuItemSub.NavMenuItemId);
            return View(navMenuItemSub);
        }

        // GET: NavMenuItemSubs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navMenuItemSub = await _context.NavMenuItemSub.FindAsync(id);
            if (navMenuItemSub == null)
            {
                return NotFound();
            }
            ViewData["NavMenuItemId"] = new SelectList(_context.NavMenuItems, "Id", "Name", navMenuItemSub.NavMenuItemId);
            return View(navMenuItemSub);
        }

        // POST: NavMenuItemSubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NavMenuItemId,Icon,Url,Name,indx,IsActive")] NavMenuItemSub navMenuItemSub)
        {
            if (id != navMenuItemSub.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(navMenuItemSub);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NavMenuItemSubExists(navMenuItemSub.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details","NavMenuItems",new { id=navMenuItemSub.NavMenuItemId});
            }
            ViewData["NavMenuItemId"] = new SelectList(_context.NavMenuItems, "Id", "Url", navMenuItemSub.NavMenuItemId);
            return View(navMenuItemSub);
        }

        // GET: NavMenuItemSubs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navMenuItemSub = await _context.NavMenuItemSub
                .Include(n => n.NavMenuItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (navMenuItemSub == null)
            {
                return NotFound();
            }

            return View(navMenuItemSub);
        }

        // POST: NavMenuItemSubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var navMenuItemSub = await _context.NavMenuItemSub.FindAsync(id);
            _context.NavMenuItemSub.Remove(navMenuItemSub);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "NavMenuItems", new { id = navMenuItemSub.NavMenuItemId });
        }

        private bool NavMenuItemSubExists(int id)
        {
            return _context.NavMenuItemSub.Any(e => e.Id == id);
        }
    }
}
