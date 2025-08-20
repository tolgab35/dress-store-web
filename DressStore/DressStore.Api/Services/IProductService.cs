using DressStore.Api.Dtos;

namespace DressStore.Api.Services
{
    public interface IProductService
    {
        Task<Response<List<ProductDTO>>> GetAllProductsAsync();
        Task<Response<ProductDTO>> GetProductByIdAsync(int id);
        Task<Response<ProductDTO>> CreateProductAsync(ProductDTO productDto);
        Task<Response<ProductDTO>> UpdateProductAsync(int id, ProductDTO productDto);
        Task<Response<bool>> DeleteProductAsync(int id);
        Task<Response<List<ProductDTO>>> GetProductsByCategoryIdAsync(int categoryId);
        Task<Response<List<ProductDTO>>> SearchProductsAsync(string searchTerm);
    }
}
