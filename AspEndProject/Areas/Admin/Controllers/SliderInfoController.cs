using AspEndProject.DAL;
using AspEndProject.Models;
using AspEndProject.ViewModels.SliderInfos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspEndProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderInfoController : Controller
    {
        private readonly AppDbContext _context;
        public SliderInfoController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SliderInfo> sliderInfos = await _context.SliderInfos.ToListAsync();
            return View(sliderInfos);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            SliderInfo sliderInfo = await _context.SliderInfos.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (sliderInfo == null) return NotFound();
            SliderInfoDetailVM model = new()
            {
                Id = sliderInfo.Id,
                Title = sliderInfo.Title,
                SubTitle = sliderInfo.SubTitle
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderInfoCreateVM sliderInfo)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool existSliderInfo = await _context.SliderInfos.AnyAsync(m => m.Title == sliderInfo.Title && m.SubTitle == sliderInfo.SubTitle);
            if (existSliderInfo)
            {
                ModelState.AddModelError("Title", "These inputs already exist");
            }

            await _context.SliderInfos.AddAsync(new SliderInfo { Title = sliderInfo.Title, SubTitle = sliderInfo.SubTitle });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            SliderInfo sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync(m => m.Id == id);

            if (sliderInfo == null) return NotFound();

            _context.SliderInfos.Remove(sliderInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null) return BadRequest();
            SliderInfo sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync(m => m.Id == id);

            if (sliderInfo == null) return NotFound();

            return View(new SliderInfoUpdateVM
            {
                Title = sliderInfo.Title,
                SubTitle = sliderInfo.SubTitle
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SliderInfoUpdateVM sliderInfo, int? id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null) return BadRequest();
            SliderInfo existSliderInfo = await _context.SliderInfos.FirstOrDefaultAsync(m => m.Id == id);

            if (sliderInfo == null) return NotFound();

            existSliderInfo.Title = sliderInfo.Title;
            existSliderInfo.SubTitle = sliderInfo.SubTitle;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
