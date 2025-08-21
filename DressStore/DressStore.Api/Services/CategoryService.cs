using DressStore.Api.Dtos;
using DressStore.Api.Resources;
using DressStore.Api.Data;
using DressStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DressStore.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<CategoryDTO>>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();
                var dtos = categories.Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

                return new Response<List<CategoryDTO>>
                {
                    data = dtos,
                    success = true,
                    message = Resource.OperationSuccessful
                };
            }
            catch (Exception ex)
            {
                return new Response<List<CategoryDTO>>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<CategoryDTO>> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return new Response<CategoryDTO>
                    {
                        data = null,
                        success = false,
                        message = Resource.CategoryNotFound
                    };
                }

                var dto = new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name
                };

                return new Response<CategoryDTO>
                {
                    data = dto,
                    success = true,
                    message = Resource.CategoryFound
                };
            }
            catch (Exception ex)
            {
                return new Response<CategoryDTO>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<CategoryDTO>> CreateCategoryAsync(CategoryDTO categoryDto)
        {
            try
            {
                var category = new Category
                {
                    Name = categoryDto.Name
                };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                var dto = new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name
                };

                return new Response<CategoryDTO>
                {
                    data = dto,
                    success = true,
                    message = Resource.CategoryCreated
                };
            }
            catch (Exception ex)
            {
                return new Response<CategoryDTO>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<CategoryDTO>> UpdateCategoryAsync(int id, CategoryDTO categoryDto)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return new Response<CategoryDTO>
                    {
                        data = null,
                        success = false,
                        message = Resource.CategoryNotFound
                    };
                }

                category.Name = categoryDto.Name;
                await _context.SaveChangesAsync();

                var dto = new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name
                };

                return new Response<CategoryDTO>
                {
                    data = dto,
                    success = true,
                    message = Resource.CategoryUpdated
                };
            }
            catch (Exception ex)
            {
                return new Response<CategoryDTO>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<bool>> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return new Response<bool>
                {
                    data = false,
                    success = false,
                    message = Resource.CategoryNotFound
                };
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return new Response<bool>
            {
                data = true,
                success = true,
                message = Resource.CategoryDeleted
            };
        }

        public async Task<Response<List<CategoryDTO>>> SearchCategoriesAsync(string searchTerm)
        {
            try
            {
                var categories = await _context.Categories
                    .Where(c => c.Name.Contains(searchTerm))
                    .ToListAsync();

                var dtos = categories.Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

                return new Response<List<CategoryDTO>>
                {
                    data = dtos,
                    success = true,
                    message = Resource.OperationSuccessful
                };
            }
            catch (Exception ex)
            {
                return new Response<List<CategoryDTO>>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<List<CategoryDTO>>> GetCategoriesByProductIdAsync(int productId)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.Id == productId);

                var dtos = product?.Category != null
                    ? new List<CategoryDTO> { new CategoryDTO { Id = product.Category.Id, Name = product.Category.Name } }
                    : new List<CategoryDTO>();

                return new Response<List<CategoryDTO>>
                {
                    data = dtos,
                    success = true,
                    message = Resource.OperationSuccessful
                };
            }
            catch (Exception ex)
            {
                return new Response<List<CategoryDTO>>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }
    }
}
