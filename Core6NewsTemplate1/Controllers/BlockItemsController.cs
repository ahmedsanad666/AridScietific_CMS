using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebOS.AuxiliaryClasses;
using WebOS.Data;
using WebOS.Models;
using Microsoft.AspNetCore.Hosting;

namespace WebOS.Controllers
{
    public class BlockItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _environment;

        public BlockItemsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: BlockItems
        public async Task<IActionResult> Index(int id)
        {
            var applicationDbContext = _context.BlockItem.Include(b => b.Block).Where(b=>b.BlockId==id);
            ViewBag.block = _context.Block.SingleOrDefault(b => b.Id == id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BlockItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blockItem = await _context.BlockItem
                .Include(b => b.Block)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blockItem == null)
            {
                return NotFound();
            }

            return View(blockItem);
        }

        // GET: BlockItems/Create
        public IActionResult Create(int bid)
        {
            ViewData["BlockId"] = new SelectList(_context.Block.Where(b=>b.Id==bid), "Id", "Name");
            return View();
        }

        // POST: BlockItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BlockId,Name,link")] BlockItem blockItem,IFormFile myfile)
        {
            if (ModelState.IsValid)
            {
                blockItem.Image = await UserFile.UploadeNewFileAsync(blockItem.Image,
                myfile, _environment.WebRootPath, Properties.Resources.Pictures);

                _context.Add(blockItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),new { id=blockItem.BlockId});
            }
            ViewData["BlockId"] = new SelectList(_context.Block, "Id", "Name", blockItem.BlockId);
            return View(blockItem);
        }

        // GET: BlockItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blockItem = await _context.BlockItem.FindAsync(id);
            if (blockItem == null)
            {
                return NotFound();
            }
            ViewData["BlockId"] = new SelectList(_context.Block, "Id", "Name", blockItem.BlockId);
            return View(blockItem);
        }

        // POST: BlockItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BlockId,Name,link")] BlockItem blockItem, IFormFile myfile)
        {
            if (id != blockItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    blockItem.Image = await UserFile.UploadeNewFileAsync(blockItem.Image,
            myfile, _environment.WebRootPath, Properties.Resources.Pictures);
                    _context.Update(blockItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlockItemExists(blockItem.Id))
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
            ViewData["BlockId"] = new SelectList(_context.Block, "Id", "Name", blockItem.BlockId);
            return View(blockItem);
        }

        // GET: BlockItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blockItem = await _context.BlockItem
                .Include(b => b.Block)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blockItem == null)
            {
                return NotFound();
            }

            return View(blockItem);
        }

        // POST: BlockItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blockItem = await _context.BlockItem.FindAsync(id);
            _context.BlockItem.Remove(blockItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlockItemExists(int id)
        {
            return _context.BlockItem.Any(e => e.Id == id);
        }
    }
}
