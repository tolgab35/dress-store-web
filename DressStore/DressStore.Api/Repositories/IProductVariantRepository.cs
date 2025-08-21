using DressStore.Api.Models;

namespace DressStore.Api.Repositories
{
    public interface IProductVariantRepository
    {
        Task<List<ProductVariant>> GetAllProductVariantsAsync();
        Task<ProductVariant> GetProductVariantByIdAsync(int id);
        Task<ProductVariant> AddProductVariantAsync(ProductVariant productVariant);
        Task<ProductVariant> UpdateProductVariantAsync(ProductVariant productVariant);
        Task<bool> DeleteProductVariantAsync(int id);
    }
}
