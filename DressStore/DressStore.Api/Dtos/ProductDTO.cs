using DressStore.Api.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DressStore.Api.Dtos
{
    public class ProductDTO
    {
        [BindNever]
        public int Id { get; set; } // Sadece response için, request'te ignore edilir
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Description { get; set; }

        [Precision(10, 2)]
        public decimal Price { get; set; }                 // Temel liste fiyatı

        [Precision(10, 2)]
        public decimal? CompareAtPrice { get; set; }       // Eski/etiket fiyatı (ops.)

        public int CategoryId { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
