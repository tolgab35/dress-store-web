using dress_store_web.Models;
using DressStore.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DressStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductVariantController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public ProductVariantController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<ProductVariant>> GetProducts()
        {
            return await _dbContext.ProductVariants.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductVariant([FromBody] ProductVariant productVariant)
        {
            if (productVariant == null)
            {
                return BadRequest("Product variant cannot be null.");
            }
            _dbContext.ProductVariants.Add(productVariant);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProducts), new { id = productVariant.Id }, productVariant);
        }
    }
}
