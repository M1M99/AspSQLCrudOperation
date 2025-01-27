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
        public async Task<ActionResult>Add(ProductAddViewModel vm) {
            await _productService.AddAsync(vm.Product);
            return RedirectToAction("Index");
        }
    }
}
