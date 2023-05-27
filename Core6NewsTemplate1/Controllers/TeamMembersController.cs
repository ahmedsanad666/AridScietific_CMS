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
    public class TeamMembersController : Controller
    {
        private IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public TeamMembersController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: TeamMembers
        public async Task<IActionResult> Index()
        {
              return View(await _context.TeamMember.ToListAsync());
        }

        // GET: TeamMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TeamMember == null)
            {
                return NotFound();
            }

            var teamMember = await _context.TeamMember
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamMember == null)
            {
                return NotFound();
            }

            return View(teamMember);
        }

        // GET: TeamMembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeamMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,EnName,BriefDescription,EnBriefDescription,Job,EnJob,Image,IsActive,FB,Twitter,Linkedin,Youtube,Instagram")] TeamMember teamMember,IFormFile myfile)
        {
            if (ModelState.IsValid)
            {
                teamMember.Image = await UserFile.UploadeNewFileAsync(teamMember.Image,
myfile, _environment.WebRootPath, Properties.Resources.Pictures);

                _context.Add(teamMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teamMember);
        }

        // GET: TeamMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TeamMember == null)
            {
                return NotFound();
            }

            var teamMember = await _context.TeamMember.FindAsync(id);
            if (teamMember == null)
            {
                return NotFound();
            }
            return View(teamMember);
        }

        // POST: TeamMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,EnName,BriefDescription,EnBriefDescription,Job,EnJob,Image,IsActive,FB,Twitter,Linkedin,Youtube,Instagram")] TeamMember teamMember,IFormFile myfile)
        {
            if (id != teamMember.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    teamMember.Image = await UserFile.UploadeNewFileAsync(teamMember.Image,
myfile, _environment.WebRootPath, Properties.Resources.Pictures);

                    _context.Update(teamMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamMemberExists(teamMember.Id))
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
            return View(teamMember);
        }

        // GET: TeamMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TeamMember == null)
            {
                return NotFound();
            }

            var teamMember = await _context.TeamMember
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamMember == null)
            {
                return NotFound();
            }

            return View(teamMember);
        }

        // POST: TeamMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TeamMember == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TeamMember'  is null.");
            }
            var teamMember = await _context.TeamMember.FindAsync(id);
            if (teamMember != null)
            {
                _context.TeamMember.Remove(teamMember);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamMemberExists(int id)
        {
          return _context.TeamMember.Any(e => e.Id == id);
        }
    }
}
