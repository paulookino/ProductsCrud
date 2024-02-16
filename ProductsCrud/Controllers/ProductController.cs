using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductsCrud.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ProductService productService;

        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }

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
        public async Task<ActionResult> Create([Bind("Name")] Product newProduct)
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
        public async Task<ActionResult> Edit([Bind("Id,Name")] Product updatedProduct)
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
