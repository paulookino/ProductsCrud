using Business.Models;

namespace Business.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product updatedProduct);
        Task<bool> DeleteProductAsync(int productId);
    }
}
