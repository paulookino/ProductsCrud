using Business.Models;
using Business.Services;
using Data.Repositories;
using Moq;
using NUnit.Framework;
using System.Data.SqlClient;

namespace Tests.Data
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        [Test]
        public async Task GetProductsAsync_ReturnsProducts()
        {
            var mockRepository = new MockProductRepository(new List<Product> { });
            var result = await mockRepository.GetProductsAsync();

            // Assert on the expected result
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetProductById_ReturnsProduct()
        {
            // Arrange
            var mockRepository = new MockProductRepository(new List<Product>
        {
            new Product { Id = 1, Name = "Product1", Price = 10.0m, Stock = 5, Active = true, CreatedAt = DateTime.Now }
        });

            // Act
            var result = mockRepository.GetProductByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public void AddProduct_AddsProduct()
        {
            // Arrange
            var mockRepository = new MockProductRepository(new List<Product> { });
            var productToAdd = new Product { };

            // Act
            mockRepository.AddProductAsync(productToAdd);

            // Assert
            var addedProduct = mockRepository.GetProductsAsync().Result.FirstOrDefault(p => p.Id == productToAdd.Id);
            Assert.IsNotNull(addedProduct);
        }

        [Test]
        public void UpdateProduct_UpdatesProduct()
        {
            // Arrange
            var mockRepository = new MockProductRepository(new List<Product> { });
            var productToUpdate = new Product { };
            mockRepository.AddProductAsync(productToUpdate);

            // Act
            var updatedProduct = new Product { };
            bool result = mockRepository.UpdateProductAsync(updatedProduct).Result;

            // Assert
            Assert.IsTrue(result);
            var retrievedProduct = mockRepository.GetProductsAsync().Result.FirstOrDefault(p => p.Id == updatedProduct.Id);
            Assert.IsNotNull(retrievedProduct);
        }

        [Test]
        public void DeleteProduct_DeactivatesProduct()
        {
            // Arrange
            var mockRepository = new MockProductRepository(new List<Product> { });
            var productToDelete = new Product {  };
            mockRepository.AddProductAsync(productToDelete);

            // Act
            bool result = mockRepository.DeleteProductAsync(productToDelete.Id).Result;

            // Assert
            Assert.IsTrue(result);
            var deletedProduct = mockRepository.GetProductsAsync().Result.FirstOrDefault(p => p.Id == productToDelete.Id);
            Assert.IsNotNull(deletedProduct);
            Assert.IsFalse(deletedProduct.Active);
        }
    }
}
