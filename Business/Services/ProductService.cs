using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.Models;

namespace Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productRepository.GetProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("ProductId must be greater than zero.", nameof(productId));

            return await _productRepository.GetProductByIdAsync(productId);
        }

        public async Task AddProductAsync(Product newProduct)
        {
            if (newProduct == null)
                throw new ArgumentNullException(nameof(newProduct), "Product cannot be null.");

            newProduct.CreatedAt = DateTime.Now;
            newProduct.Active = true;

            await _productRepository.AddProductAsync(newProduct);
        }

        public async Task<bool> UpdateProductAsync(Product updatedProduct)
        {
            if (updatedProduct == null)
                throw new ArgumentNullException(nameof(updatedProduct), "Updated product cannot be null.");

            updatedProduct.UpdatedAt = DateTime.Now;

            return await _productRepository.UpdateProductAsync(updatedProduct);
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("ProductId must be greater than zero.", nameof(productId));

            return await _productRepository.DeleteProductAsync(productId);
        }
    }
}
