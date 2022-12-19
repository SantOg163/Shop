using System.ComponentModel.DataAnnotations;

namespace Shop.Data
{
    public class Sizes
    {
        [Key]
        public int SizeId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}