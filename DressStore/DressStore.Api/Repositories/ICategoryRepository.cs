using DressStore.Api.Models;

namespace DressStore.Api.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> AddCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
