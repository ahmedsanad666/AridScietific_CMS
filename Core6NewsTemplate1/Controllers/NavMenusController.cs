using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebOS.Data;
using WebOS.Models;
using X.PagedList;

namespace WebOS.Controllers
{
    public class NavMenusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NavMenusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NavMenus
        public IActionResult Index(int? page)
        {
            int pagenumber = page ?? 1;
            var navs = _context.NavMenus;
            var onepageofnav = navs.ToPagedList(pagenumber, 50);

            ViewBag.onepageofnav = onepageofnav;

            NavViewModel navViewModel = new NavViewModel()
            {
                NavMenuItems = _context.NavMenuItems,
            };

            //ViewData["NavMenuId"] = new SelectList(_context.NavMenus, "Id", "Name");
            return View(navViewModel);
        }

        // GET: NavMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.navitems = _context.NavMenuItems.Where(n => n.NavMenuId == id);
            ViewBag.navitemsubs = _context.NavMenuItemSub;
            var navMenu = await _context.NavMenus
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewData["navmenuname"] = navMenu.Name;
            NavViewModel navVM = new NavViewModel()
            {
                NavMenuItemSubs = _context.NavMenuItemSub,
            };
            if (navMenu == null)
            {
                return NotFound();
            }

            return View(navVM);
        }

        // GET: NavMenus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NavMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Url,indx,IsActive,Icon,NavMenuPosision,EnName")] NavMenu navMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(navMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(navMenu);
        }

        // GET: NavMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navMenu = await _context.NavMenus.FindAsync(id);
            if (navMenu == null)
            {
                return NotFound();
            }
            return View(navMenu);
        }

        // POST: NavMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NavMenuPosision,Name,Url,indx,IsActive,Icon,EnName")] NavMenu navMenu)
        {
            if (id != navMenu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(navMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NavMenuExists(navMenu.Id))
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
            return View(navMenu);
        }

        // GET: NavMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var navMenu = await _context.NavMenus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (navMenu == null)
            {
                return NotFound();
            }

            return View(navMenu);
        }

        // POST: NavMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var navMenu = await _context.NavMenus.FindAsync(id);
            _context.NavMenus.Remove(navMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NavMenuExists(int id)
        {
            return _context.NavMenus.Any(e => e.Id == id);
        }
    }
}
