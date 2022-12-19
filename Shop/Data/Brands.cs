using System.ComponentModel.DataAnnotations;

namespace Shop.Data
{
    public class Brands
    {
        [Key]
        public int BrandId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
