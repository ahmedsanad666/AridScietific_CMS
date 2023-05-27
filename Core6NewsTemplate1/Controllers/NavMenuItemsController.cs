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
    public class NavMenuItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NavMenuItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NavMenuItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.NavMenuItems.Include(n => n.NavMenu);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: NavMenuItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.navitemsubs = _context.NavMenuItemSub.Where(n=>n.NavMenuItemId==id);
            var navMenuItem = await _context.NavMenuItems
                .Include(n => n.NavMenu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (navMenuItem == null)
            {
                return NotFound();
            }

            return View(navMenuItem);
        }

        // GET: NavMenuItems/Create
        public IActionResult Create()
        {
            ViewData["NavMenuId"] = new SelectList(_context.NavMenus, "Id", "Name");
            return View();
        }

        // POST: NavMenuItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NavMenuId,Url,Name,indx,IsActive,Icon,EnName")] NavMenuItem navMenuItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(navMenuItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","NavMenus");
            }
            ViewData["NavMenuId"] = new SelectList(_context.NavMenus, "Id", "Name", navMenuItem.NavMenuId);
            return View(navMenuItem);
        }

        // GET: NavMenuItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navMenuItem = await _context.NavMenuItems.FindAsync(id);
            if (navMenuItem == null)
            {
                return NotFound();
            }
            ViewData["NavMenuId"] = new SelectList(_context.NavMenus, "Id", "Name", navMenuItem.NavMenuId);
            return View(navMenuItem);
        }

        // POST: NavMenuItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NavMenuId,Url,Name,indx,IsActive,Icon,EnName")] NavMenuItem navMenuItem)
        {
            if (id != navMenuItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(navMenuItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NavMenuItemExists(navMenuItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details","NavMenus",new { id=navMenuItem.NavMenuId});
            }
            ViewData["NavMenuId"] = new SelectList(_context.NavMenus, "Id", "Name", navMenuItem.NavMenuId);
            return View(navMenuItem);
        }

        // GET: NavMenuItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navMenuItem = await _context.NavMenuItems
                .Include(n => n.NavMenu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (navMenuItem == null)
            {
                return NotFound();
            }

            return View(navMenuItem);
        }

        // POST: NavMenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var navMenuItem = await _context.NavMenuItems.FindAsync(id);
            _context.NavMenuItems.Remove(navMenuItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "NavMenus", new { id = navMenuItem.NavMenuId });
        }

        private bool NavMenuItemExists(int id)
        {
            return _context.NavMenuItems.Any(e => e.Id == id);
        }
    }
}
