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
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comment comment)
        {
            comment.Blog = await _context.Blogs
                .Include(c => c.Comments)
                .FirstOrDefaultAsync(p => p.Id == comment.BlogId);
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            comment.User = currentUser;
            comment.UserId = userId;
            if (ModelState.IsValid)
            {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Blogs", new { id = comment.BlogId });
            }
            return View(comment);
        }
    }
}