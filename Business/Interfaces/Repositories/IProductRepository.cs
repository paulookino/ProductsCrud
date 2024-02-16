using Business.Models;

namespace Business.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product updatedProduct);
        Task<bool> DeleteProductAsync(int productId);
    }
}
