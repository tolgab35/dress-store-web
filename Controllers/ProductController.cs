using Microsoft.AspNetCore.Mvc;
using dress_store_web.Models;

namespace dress_store_web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
      private readonly Product product;
    }
}
