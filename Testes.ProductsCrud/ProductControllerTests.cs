using Business.Interfaces.Services;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProductsCrud.Controllers;

namespace Testes.ProductsCrud
{
    [TestFixture]
    public class ProductControllerTests
    {
        private ProductController productController;
        private Mock<IProductService> productServiceMock;

        [SetUp]
        public void Setup()
        {
            productServiceMock = new Mock<IProductService>();
            productController = new ProductController(productServiceMock.Object);
        }

        [Test]
        public async Task Index_ReturnsViewWithProducts()
        {
            // Arrange
            var expectedProducts = new List<Product> { new Product { Id = 1, Name = "Product1" }, new Product { Id = 2, Name = "Product2" } };
            productServiceMock.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(expectedProducts);

            // Act
            var result = await productController.Index();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOf<IEnumerable<Product>>(viewResult.Model);
            var model = (IEnumerable<Product>)viewResult.Model;
            Assert.AreEqual(expectedProducts.Count, model.Count());
        }


        [Test]
        public async Task Details_ProductExists_ReturnsViewWithProduct()
        {
            // Arrange
            int productId = 1;
            var expectedProduct = new Product { Id = productId, Name = "Product1" };
            productServiceMock.Setup(repo => repo.GetProductByIdAsync(productId)).ReturnsAsync(expectedProduct);

            // Act
            var result = await productController.Details(productId);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOf<Product>(viewResult.Model);
            var model = (Product)viewResult.Model;
            Assert.AreEqual(expectedProduct, model);
        }

        [Test]
        public async Task Details_ProductDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int productId = 1;
            productServiceMock.Setup(repo => repo.GetProductByIdAsync(productId)).ReturnsAsync((Product)null);

            // Act
            var result = await productController.Details(productId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Create_ReturnsView()
        {
            // Act
            var result = productController.Create();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task Create_ValidModelState_RedirectsToIndex()
        {
            // Arrange
            var newProduct = new Product { Name = "New Product" };

            // Act
            var result = await productController.Create(newProduct);

            // Assert
            
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [Test]
        public async Task Create_InvalidModelState_ReturnsView()
        {
            // Arrange
            var newProduct = new Product { /* ModelState is invalid */ };
            productController.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await productController.Create(newProduct);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOf<Product>(viewResult.Model);
        }

        [Test]
        public async Task Edit_ProductExists_ReturnsViewWithProduct()
        {
            // Arrange
            var existingProductId = 1;
            var expectedProduct = new Product { Id = existingProductId, Name = "ExistingProduct" };
            productServiceMock.Setup(repo => repo.GetProductByIdAsync(existingProductId)).ReturnsAsync(expectedProduct);

            // Act
            var result = await productController.Edit(existingProductId);

            // Assert

            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOf<Product>(viewResult.Model);
            var model = (Product)viewResult.Model;
            Assert.AreEqual(expectedProduct, model);
        }

        [Test]
        public async Task Edit_ProductDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var nonExistingProductId = 999;
            productServiceMock.Setup(repo => repo.GetProductByIdAsync(nonExistingProductId)).ReturnsAsync((Product)null);

            // Act
            var result = await productController.Edit(nonExistingProductId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Edit_ValidModelState_RedirectsToIndex()
        {
            // Arrange
            var updatedProduct = new Product { Id = 1, Name = "Updated Product" };

            // Act
            var result = await productController.Edit(updatedProduct);

            // Assert

            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [Test]
        public async Task Delete_ProductExists_ReturnsViewWithProduct()
        {
            // Arrange
            var existingProductId = 1;
            var expectedProduct = new Product { Id = existingProductId, Name = "ExistingProduct" };
            productServiceMock.Setup(repo => repo.GetProductByIdAsync(existingProductId)).ReturnsAsync(expectedProduct);

            // Act
            var result = await productController.Delete(existingProductId);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var ViewResult = (ViewResult)result;
            Assert.AreEqual(expectedProduct, ViewResult.Model);
        }

        [Test]
        public async Task Delete_ProductDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var nonExistingProductId = 999;
            productServiceMock.Setup(repo => repo.GetProductByIdAsync(nonExistingProductId)).ReturnsAsync((Product)null);

            // Act
            var result = await productController.Delete(nonExistingProductId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task DeleteConfirmed_ProductExists_RedirectsToIndex()
        {
            // Arrange
            var existingProductId = 1;

            // Act
            var result = await productController.DeleteConfirmed(existingProductId);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }
    }
}
