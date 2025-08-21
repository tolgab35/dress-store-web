using DressStore.Api.Data;
using DressStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DressStore.Api.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly AppDbContext _context;

        public ProductImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductImage>> GetAllProductImagesAsync()
        {
            return await _context.ProductImages
                .Include(pi => pi.Product)
                .ToListAsync();
        }

        public async Task<ProductImage> GetProductImageByIdAsync(int id)
        {
            return await _context.ProductImages
                .Include(pi => pi.Product)
                .FirstOrDefaultAsync(pi => pi.Id == id);
        }

        public async Task<ProductImage> AddProductImageAsync(ProductImage productImage)
        {
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage;
        }

        public async Task<ProductImage> UpdateProductImageAsync(ProductImage productImage)
        {
            _context.ProductImages.Update(productImage);
            await _context.SaveChangesAsync();
            return productImage;
        }

        public async Task<bool> DeleteProductImageAsync(int id)
        {
            var img = await _context.ProductImages.FindAsync(id);
            if (img != null)
            {
                _context.ProductImages.Remove(img);
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }
    }
}
