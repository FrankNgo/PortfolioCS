using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Blogs.ToListAsync());
        //}

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Details(int? id)
        {
            var blog = await _context.Blogs .Include(c => c.Comments).SingleOrDefaultAsync(m => m.Id == id);
            Comment comment = new Comment();
            comment.Blog = blog;
            comment.BlogId = (int)id;
            ViewBag.Comments = _context.Comments.Where(c => c.BlogId == id);
            return View(comment);
        }

        public IActionResult Create()
        {
            return View();
        }


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

        public async Task<IActionResult> Edit(int? id)
        {
            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.Id == id);
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Copy")] Blog blog)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(blog);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            var blog = await _context.Blogs .SingleOrDefaultAsync(m => m.Id == id);
            return View(blog);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BlogIsThere(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ShowComments(int? id)
        {
            var blog = await _context.Blogs .Include(c => c.Comments).Include(u => u.User).SingleOrDefaultAsync(m => m.Id == id);
            List<Comment> comments = blog.Comments;
            return View(comments);
        }
    }
}