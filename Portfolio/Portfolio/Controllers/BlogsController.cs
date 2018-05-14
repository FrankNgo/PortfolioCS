using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portfolio.Models;
using System.Security.Claims;

namespace Portfolio.Controllers
{
    [Authorize]
    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Blogs.ToListAsync());
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .Include(c => c.Comments)
                .SingleOrDefaultAsync(m => m.Id == id);
            Comment comment = new Comment();
            comment.Blog = blog;
            comment.BlogId = (int)id;
            ViewBag.Comments = _context.Comments.Where(c => c.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overbloging attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Copy")] Blog blog)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            blog.User = currentUser;
            blog.UserId = userId;
            if (ModelState.IsValid)
            {
                _context.Blogs.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overbloging attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Copy")] Blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DisplayComments(int? id)
        {
            var blog = await _context.Blogs
                .Include(c => c.Comments)
                .Include(u => u.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            List<Comment> comments = blog.Comments;
            return View(comments);
        }
    }
}