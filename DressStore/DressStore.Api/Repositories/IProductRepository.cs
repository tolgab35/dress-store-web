using DressStore.Api.Models;

namespace DressStore.Api.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
