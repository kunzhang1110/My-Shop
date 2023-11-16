using System.ComponentModel.DataAnnotations;

namespace MyShop.DTOs
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        [Range(100, Double.PositiveInfinity)]
        public long Price { get; set; }

        public IFormFile Image { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        [Range(0, 200)]
        public int QuantityInStock { get; set; }
    }
}