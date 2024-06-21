using AspEndProject.DAL;
using AspEndProject.Models;
using AspEndProject.Services.Interface;
using AspEndProject.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspEndProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICategoryService _categoryService;
        public CategoryController(AppDbContext context, ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllCategories());
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
        public async Task<IActionResult> Create(CategoryCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            Category category = new()
            {
                Name = request.Name,

            };

            bool existCategory = await _context.Categories.AnyAsync(m => m.Name.Trim() == request.Name.Trim());
            if (existCategory)
            {
                ModelState.AddModelError("Name", "Category already  exist");
                return View();
            }

            await _context.Categories.AddAsync(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));


        }

        [Authorize(Roles = "SuperAdmin, Admin")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            Category category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            Category category = await _context.Categories.FirstOrDefaultAsync(_ => _.Id == id);
            if (category == null) return NotFound();

            return View(new CategoryUpdateVM { Id = category.Id, Name = category.Name });
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoryUpdateVM request, int? id)
        {
            Category existCategory = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (!ModelState.IsValid)
            {
                request.Name = existCategory.Name;
                return View(request);
            }
            if (existCategory == null) { return NotFound(); }


            existCategory.Name = request.Name;



            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Category category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (category == null) return NotFound();

            CategoryDetailVM model = new()
            {
                Name = category.Name,

            };

            return View(model);
        }
    }
}