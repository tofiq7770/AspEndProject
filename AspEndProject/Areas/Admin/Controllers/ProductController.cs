using AspEndProject.Areas.ViewModels.Products;
using AspEndProject.DAL;
using AspEndProject.Helpers.Extentions;
using AspEndProject.Models;
using AspEndProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspEndProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryService _categoryService;
        public ProductController(AppDbContext context, IProductService productService,
                                                       IWebHostEnvironment env,
                                                       ICategoryService categoryService)
        {
            _context = context;
            _productService = productService;
            _env = env;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {

            List<Product> products = await _productService.GetAllAsync();
            List<ProductVM> model = products.Select(m => new ProductVM { Id = m.Id, Name = m.Name, Image = m.ProductImages.FirstOrDefault(m => m.IsMain)?.Image, MinWeight = m.MinWeight, Weight = m.Weight, Price = m.Price, Origin = m.Origin, Check = m.Сheck, Quality = m.Quality, Description = m.Description, Category = m.Category.Name }).ToList();
            return View(model);

        }

        [HttpGet]

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            Product product = await _productService.GetByIdAsync((int)id);
            if (product == null) return NotFound();
            List<ProductImageVM> productImages = new();
            foreach (var item in product.ProductImages)
            {
                productImages.Add(new ProductImageVM
                {
                    Image = item.Image,
                    IsMain = item.IsMain
                });

            }
            ProductDetailVM model = new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category.Name,
                Images = productImages,
                Quality = product.Quality,
                Check = product.Сheck,
                MinWeight = product.MinWeight,
                Weight = product.Weight

            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllBySelectedAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ProductCreateVM request)
        {
            ViewBag.Categories = await _categoryService.GetAllBySelectedAsync();
            //if (!ModelState.IsValid) return View();

            foreach (var item in request.Images)
            {
                if (!item.CheckFileSize(200))
                {
                    ModelState.AddModelError("Images", "Image size must be max 500 kb");
                    return View();
                }

                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "Image format must be img");
                    return View();
                }
            }
            List<ProductImage> images = new();

            foreach (var item in request.Images)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;
                string path = Path.Combine(_env.WebRootPath, "img", fileName);
                await item.SaveFileToLocalAsync(path);


                images.Add(new ProductImage
                {
                    Image = fileName
                });
            }

            images.FirstOrDefault().IsMain = true;

            Product product = new()
            {
                Name = request.Name,
                Description = request.Description,
                Price = decimal.Parse(request.Price),
                CategoryId = request.CategoryId,
                ProductImages = images,
                Weight = request.Weight,
                MinWeight = request.MinWeight,
                Origin = request.Origin,
                Сheck = request.Check,
                Quality = request.Quality

            };

            await _productService.CreateAsync(product);
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = await _categoryService.GetAllBySelectedAsync();

            if (id == null) return BadRequest();
            Product product = await _productService.GetByIdAsync((int)id);
            if (product == null) return NotFound();


            List<ProductImageVM> productImage = new();

            foreach (var item in product.ProductImages)
            {
                productImage.Add(new ProductImageVM
                {
                    Image = item.Image,
                    IsMain = item.IsMain
                });
            }
            return View(new ProductUpdateVM { Name = product.Name, Description = product.Description, Images = productImage, Weight = product.Weight, Price = product.Price, Origin = product.Origin, Check = product.Сheck, Quality = product.Quality, MinWeight = product.MinWeight });

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductUpdateVM ProductUpdateVM, int? id)
        {
            if (!ModelState.IsValid) return View();
            if (id == null) return BadRequest();
            Product existProduct = await _productService.GetByIdAsync((int)id);
            if (existProduct == null) return NotFound();

            if (ProductUpdateVM.Photos != null)
            {
                foreach (var item in ProductUpdateVM.Photos)
                {
                    if (!item.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200 kb");
                        return View(ProductUpdateVM);
                    }
                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "Image format must be img");
                        return View(ProductUpdateVM);
                    }
                }

                foreach (var item in existProduct.ProductImages)
                {
                    _context.ProductImages.Remove(item);

                    FileExtentions.DeleteFileFromLocalAsync(Path.Combine(_env.WebRootPath, "img"), item.Image);
                }

                List<ProductImage> images = new();

                foreach (var item in ProductUpdateVM.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;
                    string path = Path.Combine(_env.WebRootPath, "img", fileName);
                    await item.SaveFileToLocalAsync(path);

                    images.Add(new ProductImage
                    {
                        Image = fileName
                    });
                }

                images.FirstOrDefault().IsMain = true;

                existProduct.ProductImages = images;
            }

            existProduct.Name = ProductUpdateVM.Name;
            existProduct.Description = ProductUpdateVM.Description;
            existProduct.Price = ProductUpdateVM.Price;
            existProduct.Weight = ProductUpdateVM.Weight;
            existProduct.MinWeight = ProductUpdateVM.MinWeight;
            existProduct.Origin = ProductUpdateVM.Origin;
            existProduct.Сheck = ProductUpdateVM.Check;
            existProduct.CategoryId = (int)ProductUpdateVM.CategoryId;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            Product product = await _context.Products.Where(m => m.Id == id).Include(m => m.ProductImages).FirstOrDefaultAsync();
            if (product == null) return NotFound();

            foreach (var item in product.ProductImages)
            {
                string path = Path.Combine(_env.WebRootPath, "img", item.Image);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
