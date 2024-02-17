using Business.Interfaces.Repositories;
using Business.Models;

namespace Tests.Data
{
    public class MockProductRepository : IProductRepository
    {
        private List<Product> products;

        public MockProductRepository(List<Product> products)
        {
            this.products = products;
        }

        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            return Task.FromResult<IEnumerable<Product>>(products);
        }

        public Task<Product> GetProductByIdAsync(int productId)
        {
            return Task.FromResult(products.FirstOrDefault(p => p.Id == productId));
        }

        public Task AddProductAsync(Product product)
        {
            products.Add(product);
            return Task.CompletedTask;
        }

        public Task<bool> UpdateProductAsync(Product updatedProduct)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == updatedProduct.Id);
            if (existingProduct != null)
            {
                // Update properties
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Stock = updatedProduct.Stock;
                existingProduct.UpdatedAt = updatedProduct.UpdatedAt;

                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task<bool> DeleteProductAsync(int productId)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == productId);
            if (existingProduct != null)
            {
                existingProduct.Active = false;
                existingProduct.DeletedAt = DateTime.Now;
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
