using dress_store_web.Models;
using Microsoft.AspNetCore.Mvc;

namespace dress_store_web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly Category category;

        public CategoryController(Category category)
        {
            this.category = category;
        }
    }
}