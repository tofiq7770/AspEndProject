using AspEndProject.DAL;
using AspEndProject.Models;
using AspEndProject.ViewModels.FactContents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspEndProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FactContentController : Controller
    {
        private readonly AppDbContext _context;
        public FactContentController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<FactContent> factContent = await _context.FactContents.ToListAsync();
            return View(factContent);
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            FactContent factContent = await _context.FactContents.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (factContent == null) return NotFound();
            FactContentDetailVM model = new()
            {
                Title = factContent.Title,
                NumberInfo = factContent.NumberInfo,
                Icon = factContent.Icon

            };
            return View(model);
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FactContentCreateVM fact)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool existfact = await _context.FactContents.AnyAsync(m => m.Title == fact.Title
                                                           && m.Icon == fact.Icon
                                                           && m.NumberInfo == fact.NumberInfo);
            if (existfact)
            {
                ModelState.AddModelError("Title", "These inputs already exist");
            }

            await _context.FactContents.AddAsync(new FactContent
            {
                Title = fact.Title,
                NumberInfo = fact.NumberInfo,
                Icon = fact.Icon

            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "SuperAdmin, Admin")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            FactContent factContent = await _context.FactContents.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (factContent == null) return NotFound();

            _context.FactContents.Remove(factContent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null) return BadRequest();
            FactContent factContent = await _context.FactContents.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (factContent == null) return NotFound();

            return View(new FactContentUpdateVM
            {
                Title = factContent.Title,
                Icon = factContent.Icon,
                NumberInfo = factContent.NumberInfo
            });

        }
        [Authorize(Roles = "SuperAdmin, Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(FactContentUpdateVM update, int? id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null) return BadRequest();
            FactContent factContent = await _context.FactContents.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (update == null) return NotFound();

            factContent.Title = update.Title;
            factContent.Icon = update.Icon;
            factContent.NumberInfo = update.NumberInfo;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
