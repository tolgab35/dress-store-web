using DressStore.Api.Dtos;

namespace DressStore.Api.Services
{
    public interface IProductImageService
    {
        Task<Response<List<ProductImageDTO>>> GetAllProductImagesAsync();
        Task<Response<ProductImageDTO>> GetProductImageByIdAsync(int id);
        Task<Response<ProductImageDTO>> CreateProductImageAsync(ProductImageDTO productImageDto);
        Task<Response<ProductImageDTO>> UpdateProductImageAsync(int id, ProductImageDTO productImageDto);
        Task<Response<bool>> DeleteProductImageAsync(int id);
    }
}
