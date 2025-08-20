using DressStore.Api.Dtos;

namespace DressStore.Api.Services
{
    public interface IProductVariantService
    {
        Task<Response<List<ProductVariantDTO>>> GetAllProductVariantsAsync();
        Task<Response<ProductVariantDTO>> GetProductVariantByIdAsync(int id);
        Task<Response<ProductVariantDTO>> CreateProductVariantAsync(ProductVariantDTO productVariantDto);
        Task<Response<ProductVariantDTO>> UpdateProductVariantAsync(int id, ProductVariantDTO productVariantDto);
        Task<Response<bool>> DeleteProductVariantAsync(int id);
    }
}
