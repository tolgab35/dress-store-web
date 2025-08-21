using DressStore.Api.Data;
using DressStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DressStore.Api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var categ = await _context.Categories.FindAsync(id);
            if (categ != null)
            {
                _context.Categories.Remove(categ);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }
    }
}
