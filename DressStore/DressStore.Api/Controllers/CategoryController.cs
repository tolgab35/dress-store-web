using dress_store_web.Models;
using DressStore.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dress_store_web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext; // Dependency injection ile AppDbContext'i alıyoruz 
            // bu sayede veritabanı işlemlerini gerçekleştirebiliyoruz
            // Dependency injection, uygulamanın bağımlılıklarını yönetmeyi kolaylaştırır ve test edilebilirliği artırır.

            // dependency injection nedir? 
            // Dependency injection, bir sınıfın bağımlılıklarını dışarıdan almasını sağlayan bir tasarım desenidir.
            // Bu sayede sınıflar arasındaki bağımlılıkları azaltır ve test edilebilirliği artırır.
            // constructor kullanarak bağımlılıkları enjekte ederiz. böylece tamamen bağımlı olmaz.

        }

        [HttpGet]
        public async Task<List<Category>> GetCategories()
        {
            return await _dbContext.Categories.ToListAsync(); // Veritabanındaki tüm kategorileri listeler
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("Category cannot be null.");
            }
            _dbContext.Categories.Add(category); // memory'e ekleniyor, henüz veritabanına kaydedilmedi
            await _dbContext.SaveChangesAsync(); // veritabanına kaydediliyor
            return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, category); // CreatedAtAction, yeni oluşturulan kaynağa erişim için URL döner
        }
    }
}