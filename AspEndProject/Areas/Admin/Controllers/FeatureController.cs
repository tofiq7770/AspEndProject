using AspEndProject.DAL;
using AspEndProject.Models;
using AspEndProject.ViewModels.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspEndProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController : Controller
    {
        private readonly AppDbContext _context;
        public FeatureController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Feature> features = await _context.Features.ToListAsync();
            return View(features);
        }
        [Authorize(Roles = "SuperAdmin, Admin")]

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Feature feature = await _context.Features.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (feature == null) return NotFound();
            FeatureDetailVM model = new()
            {
                Name = feature.Name,
                Icon = feature.Icon,
                Description = feature.Description
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
        public async Task<IActionResult> Create(FeatureCreateVM feature)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool existfeature = await _context.Features.AnyAsync(m => m.Name == feature.Name && m.Icon == feature.Icon && m.Description == feature.Description);
            if (existfeature)
            {
                ModelState.AddModelError("Name", "These inputs already exist");
            }

            await _context.Features.AddAsync(new Feature { Name = feature.Name, Icon = feature.Icon, Description = feature.Description });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            Feature feature = await _context.Features.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (feature == null) return NotFound();

            _context.Features.Remove(feature);
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
            Feature feature = await _context.Features.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (feature == null) return NotFound();

            return View(new FeatureUpdateVM
            {
                Name = feature.Name,
                Icon = feature.Icon,
                Description = feature.Description
            });

        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(FeatureUpdateVM update, int? id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null) return BadRequest();
            Feature feature = await _context.Features.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (update == null) return NotFound();

            feature.Description = update.Description;
            feature.Icon = update.Icon;
            feature.Name = update.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
