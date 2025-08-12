namespace dress_store_web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } // Ürün açıklaması
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; } // Fotoğraf linki
        public string AvailableSizes { get; set; } // "S,M,L,XL" gibi
        public string AvailableColors { get; set; } // "Kırmızı,Mavi,Siyah" gibi
        public int TotalStock { get; set; }
    }
}
