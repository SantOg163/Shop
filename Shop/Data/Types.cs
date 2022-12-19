using System.ComponentModel.DataAnnotations;

namespace Shop.Data
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
