using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Models;
using Business.Services;
using Moq;
using NUnit.Framework;

namespace Tests.Business
{
    [TestFixture]
    public class ProductServiceTests
    {
        [Test]
        public async Task GetProductsAsync_Should_Return_Product_List()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(repo => repo.GetProductsAsync()).ReturnsAsync(new List<Product>());

            IProductService productService = new ProductService(mockRepository.Object);

            // Act
            var products = await productService.GetProductsAsync();

            // Assert
            Assert.IsNotNull(products);
            Assert.IsInstanceOf<IEnumerable<Product>>(products);
        }

        [Test]
        public void GetProductByIdAsync_Should_Throw_Exception_When_ProductId_Is_Zero()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            IProductService productService = new ProductService(mockRepository.Object);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await productService.GetProductByIdAsync(0));
        }

        [Test]
        public async Task AddProductAsync_Should_Call_Repository_Method()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            IProductService productService = new ProductService(mockRepository.Object);

            var newProduct = new Product
            {
            };

            // Act
            await productService.AddProductAsync(newProduct);

            // Assert
            mockRepository.Verify(repo => repo.AddProductAsync(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public void UpdateProductAsync_Should_Throw_Exception_When_UpdatedProduct_Is_Null()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            IProductService productService = new ProductService(mockRepository.Object);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await productService.UpdateProductAsync(null));
        }

        [Test]
        public async Task UpdateProductAsync_Should_Call_Repository_Method()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            IProductService productService = new ProductService(mockRepository.Object);

            var updatedProduct = new Product
            {
            };

            // Act
            await productService.UpdateProductAsync(updatedProduct);

            // Assert
            mockRepository.Verify(repo => repo.UpdateProductAsync(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public void DeleteProductAsync_Should_Throw_Exception_When_ProductId_Is_Zero()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            IProductService productService = new ProductService(mockRepository.Object);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await productService.DeleteProductAsync(0));
        }

        [Test]
        public async Task DeleteProductAsync_Should_Call_Repository_Method()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            IProductService productService = new ProductService(mockRepository.Object);

            // Act
            await productService.DeleteProductAsync(1);

            // Assert
            mockRepository.Verify(repo => repo.DeleteProductAsync(It.IsAny<int>()), Times.Once);
        }
    }
}

