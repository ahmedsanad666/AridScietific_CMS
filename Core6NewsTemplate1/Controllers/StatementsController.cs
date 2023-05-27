using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebOS.Data;
using WebOS.Models;
using X.PagedList;

namespace WebOS.Controllers
{
    [Authorize(Roles =RoleName.Admins)]
    public class StatementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Statements
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Statement.Include(s => s.ApplicationUser);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        // GET: Statements/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var statement = await _context.Statement
        //        .Include(s => s.ApplicationUser)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (statement == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(statement);
        //}

        // GET: Statements/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Statements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RecordDate,Details,Amount,Destination,ApplicationUserId,BalanceType")] Statement statement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", statement.ApplicationUserId);
            return View(statement);
        }

        // GET: Statements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statement = await _context.Statement.FindAsync(id);
            if (statement == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers.Where(c=>c.Id==statement.ApplicationUserId), "Id", "Id", statement.ApplicationUserId);
            return View(statement);
        }

        // POST: Statements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RecordDate,Details,Amount,Destination,ApplicationUserId,BalanceType")] Statement statement)
        {
            if (id != statement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatementExists(statement.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", statement.ApplicationUserId);
            return View(statement);
        }

        // GET: Statements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statement = await _context.Statement
                .Include(s => s.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statement == null)
            {
                return NotFound();
            }

            return View(statement);
        }

        // POST: Statements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statement = await _context.Statement.FindAsync(id);
            _context.Statement.Remove(statement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admins")]
        public IActionResult Index(string ss, int ? page)
        {
            var numberofpage = page ?? 1;
            //var result = new PagedResult<Statements>();

            IQueryable<Statement> applicationDbContext;
            if (ss == null)
            {
                applicationDbContext = _context.Statement.Include(s => s.ApplicationUser).OrderByDescending(a => a.RecordDate);

                //result = new PagedResult<Statements>
                //{
                //    Data = applicationDbContext.AsNoTracking().ToList(),
                //    TotalItems = _context.Statements.Count(),
                //    PageNumber = pageNumber,
                //    PageSize = pageSize,
                //};
                var onepageofstatements = applicationDbContext.ToPagedList(numberofpage, 50);
                ViewBag.onepageofstatements = onepageofstatements;
            }
            else
            {
                applicationDbContext = _context.Statement.Where(a => a.Details.Contains(ss) || a.ApplicationUser.ArName.Contains(ss) || a.ApplicationUser.EnName.Contains(ss)).Include(s => s.ApplicationUser).OrderByDescending(a => a.RecordDate);
                //result = new PagedResult<Statements>
                //{
                //    Data = applicationDbContext.AsNoTracking().ToList(),
                //    TotalItems = _context.Statements.Where(a => a.Details.Contains(ss) || a.ApplicationUser.ArName.Contains(ss) || a.ApplicationUser.EnName.Contains(ss)).Count(),
                //    PageNumber = pageNumber,
                //    PageSize = pageSize,
                //};
                var onepageofstatements = applicationDbContext.ToPagedList(numberofpage, 50);
                ViewBag.onepageofstatements = onepageofstatements;

            }
            ViewData["ss"] = ss;
            //ViewData["Balance"] = 
            return View(applicationDbContext);
        }


        private bool StatementExists(int id)
        {
            return _context.Statement.Any(e => e.Id == id);
        }
    }
}
