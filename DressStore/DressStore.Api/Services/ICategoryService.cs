using dress_store_web.Models;
using DressStore.Api.Dtos;

namespace DressStore.Api.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDTO>>> GetAllCategoriesAsync();
        Task<Response<CategoryDTO>> GetCategoryByIdAsync(int id);
        Task<Response<CategoryDTO>> CreateCategoryAsync(CategoryDTO categoryDto);
        Task<Response<CategoryDTO>> UpdateCategoryAsync(int id, CategoryDTO categoryDto);
        Task<Response<bool>> DeleteCategoryAsync(int id);
        Task<Response<List<CategoryDTO>>> GetCategoriesByProductIdAsync(int productId);
        Task<Response<List<CategoryDTO>>> SearchCategoriesAsync(string searchTerm);
    }
}
