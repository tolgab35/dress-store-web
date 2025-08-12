using dress_store_web.Models;
using Microsoft.AspNetCore.Mvc;

namespace dress_store_web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly Category category;
        public IActionResult Index()
        {
            return View();
        }
    }
}
