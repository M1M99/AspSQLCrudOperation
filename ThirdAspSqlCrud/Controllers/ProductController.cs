using Microsoft.AspNetCore.Mvc;
using ThirdAspSqlCrud.Entities;
using ThirdAspSqlCrud.Models;
using ThirdAspSqlCrud.Services;

namespace ThirdAspSqlCrud.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(string key = "", int id = 0)
        {
            var resp = await _productService.GetByKeyAndIdAsync(key, id);
            return View(resp);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(ProductAddViewModel vm, IFormFile imgFile)
        {
            if (ModelState.IsValid)
            {
                if (imgFile == null || imgFile.Length == 0)
                {
                    ModelState.AddModelError("ImgFile", "No file selected.");
                    return View(vm); //file secilmese meesage
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, imgFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imgFile.CopyToAsync(stream);
                }

                vm.Product.ImgLink = "/images/" + imgFile.FileName;

                await _productService.AddAsync(vm.Product);
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {

            var product = await _productService.GetWithId(id);
            if (product == null)
            {
                return NotFound();
            }
            var vm = new ProductEditViewModel
            {
                Product = product
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> Update(int id, ProductEditViewModel vm, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                var prod = await _productService.GetWithId(id);
                if (prod == null) {
                    return NotFound();
                }
                if(img != null && img.Length > 0)
                {
                    var fileUp = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                    if (!Directory.Exists(fileUp))
                    {
                        Directory.CreateDirectory(fileUp);
                    }

                    var filePath = Path.Combine(fileUp, img.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await img.CopyToAsync(stream);
                    }

                    prod.ImgLink = "/images/" + img.FileName;
                    vm.Product.ImgLink = prod.ImgLink;
                }
                await _productService.Update(id, vm);
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        public async Task<IActionResult> GetWithId(int id)
        {
            var prod = await _productService.GetWithId(id);
            return View(prod);
        }
    }
}
