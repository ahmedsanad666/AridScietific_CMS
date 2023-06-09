﻿using System;
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
    public class BlogPostCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogPostCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BlogPostComments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BlogPostComment.Include(b => b.BlogPost);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BlogPostComments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPostComment = await _context.BlogPostComment
                .Include(b => b.BlogPost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPostComment == null)
            {
                return NotFound();
            }

            return View(blogPostComment);
        }

        // GET: BlogPostComments/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["BlogPostId"] = new SelectList(_context.BlogPost, "Id", "Body");
            return View();
        }

        // POST: BlogPostComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Comment,DateTime,IsHidden,IsFeatured,IsBestAnswer,IsDeleted,BlogPostId,ApplicationUserId,File")] BlogPostComment blogPostComment)
        {
            if (ModelState.IsValid)
            {
                blogPostComment.Id = Guid.NewGuid();
                _context.Add(blogPostComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogPostId"] = new SelectList(_context.BlogPost, "Id", "Body", blogPostComment.BlogPostId);
            return View(blogPostComment);
        }

        // GET: BlogPostComments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPostComment = await _context.BlogPostComment.FindAsync(id);
            if (blogPostComment == null)
            {
                return NotFound();
            }
            ViewData["BlogPostId"] = new SelectList(_context.BlogPost, "Id", "Body", blogPostComment.BlogPostId);
            return View(blogPostComment);
        }

        // POST: BlogPostComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Comment,DateTime,IsHidden,IsFeatured,IsBestAnswer,IsDeleted,BlogPostId,ApplicationUserId,File")] BlogPostComment blogPostComment)
        {
            if (id != blogPostComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPostComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostCommentExists(blogPostComment.Id))
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
            ViewData["BlogPostId"] = new SelectList(_context.BlogPost, "Id", "Body", blogPostComment.BlogPostId);
            return View(blogPostComment);
        }

        // GET: BlogPostComments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPostComment = await _context.BlogPostComment
                .Include(b => b.BlogPost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPostComment == null)
            {
                return NotFound();
            }

            return View(blogPostComment);
        }

        // POST: BlogPostComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var blogPostComment = await _context.BlogPostComment.FindAsync(id);
            _context.BlogPostComment.Remove(blogPostComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostCommentExists(Guid id)
        {
            return _context.BlogPostComment.Any(e => e.Id == id);
        }
    }
}
