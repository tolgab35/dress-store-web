using DressStore.Api.Models;

namespace DressStore.Api.Repositories
{
    public interface IProductImageRepository
    {
        Task<List<ProductImage>> GetAllProductImagesAsync();
        Task<ProductImage> GetProductImageByIdAsync(int id);
        Task<ProductImage> AddProductImageAsync(ProductImage productImage);
        Task<ProductImage> UpdateProductImageAsync(ProductImage productImage);
        Task<bool> DeleteProductImageAsync(int id);
    }
}
