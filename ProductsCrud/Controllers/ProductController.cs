using Business.Interfaces.Services;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsCrud.Authorize;

namespace ProductsCrud.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var products = await productService.GetProductsAsync();
            return View(products);
        }

        public async Task<ActionResult> Details(int id)
        {
            var product = await productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                await productService.AddProductAsync(newProduct);
                return RedirectToAction("Index");
            }

            return View(newProduct);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var product = await productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product updatedProduct)
        {
            if (ModelState.IsValid)
            {
                await productService.UpdateProductAsync(updatedProduct);
                return RedirectToAction("Index");
            }

            return View(updatedProduct);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var product = await productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await productService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }
    }
}
