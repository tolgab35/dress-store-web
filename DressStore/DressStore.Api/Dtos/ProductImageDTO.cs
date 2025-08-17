using dress_store_web.Models;

namespace DressStore.Api.Dtos
{
    public class ProductImageDTO
    {
        public int ProductId { get; set; }
        public string Url { get; set; } = null!;
        public bool IsPrimary { get; set; } = false;
        public int SortOrder { get; set; } = 0;
    }
}
